using MISA.ASP.ClientApp.BL;
using MISA.ASP.ClientApp.Models.ActionInput.SynTaxDec;
using MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using MISA.ASP.ClientApp.Utils.Clients;
using MISA.ASP.ClientApp.Utils.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using MISA.ASP.ClientApp.UI.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.Exceptions;
using MISA.ASP.ClientApp.Models;
using MISA.ASP.ClientApp.Utils.FileHandler;

namespace MISA.ASP.ClientApp.UI.TaxDeclaration
{
    public partial class frmTaxDecSubmitted : Form
    {
        private AspClient _aspClient { get; set; }

        public frmTaxDecSubmitted()
        {
            InitializeComponent();
            Program.InProcessing = true;
            this.TopMost = true;
            _aspClient = new AspClient();
        }

        public void ShowForm(string actionID, string data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string>(ShowForm), actionID, data);
            }
            else
            {
                this.Show();
                UpdateProgress(5);
                Task.Factory.StartNew(() =>
                {
                    _  = SyncTaxDecSubmitted(actionID, data);
                });
            }
        }

        private void frmTaxDecSubmitted_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.InProcessing = false;
        }

        public async Task SyncTaxDecSubmitted(string actionID, string data)
        {
            try
            {
                var output = new SyncTaxDecSubmittedOutput() { ActionID = actionID };
                var syncInput = JsonConvert.DeserializeObject<SyncTaxDecSubmittedInput>(data);
                UpdateProgress(10);
                if (syncInput != null && syncInput.Details != null && syncInput.Details.Count > 0)
                {

                    var isLocalMode = bool.Parse(ConfigurationManager.AppSettings["IsLocalMode"]);
                    foreach (var detail in syncInput.Details)
                    {
                        var iOutputDetail = new SyncTaxDecSubmittedOutputDetail() { CustomerID = detail.CustomerID };
                        try
                        {
                            if (!isLocalMode)
                            {
                                var crawler = new ETaxCrawler(syncInput.ProfileID, detail.CustomerID, detail.TaxCode, detail.Username, detail.Password, CancellationToken.None);
                                var taxDecSubmitted = await crawler.GetTaxDecSubmitted(syncInput.FromDate, syncInput.ToDate);
                                iOutputDetail.TaxDeclarationSubmitteds = taxDecSubmitted;
                            }
                            else
                            {
                                using (var r = new StreamReader($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "")}"))
                                {
                                    string json = r.ReadToEnd();
                                    List<TaxDeclarationSubmitted> items = JsonConvert.DeserializeObject<List<TaxDeclarationSubmitted>>(json);
                                    iOutputDetail.TaxDeclarationSubmitteds = items;
                                }
                            }
                        }
                        catch (InvalidCaptchaException ex)
                        {
                            iOutputDetail.CrawlErrorType = CrawlErrorType.InvalidCaptcha;
                            LogUtil.LogError(ex);
                        }
                        catch (InvalidIdentityException ex)
                        {
                            iOutputDetail.CrawlErrorType = CrawlErrorType.InvalidIdentity;
                            LogUtil.LogError(ex);
                        }
                        catch (PasswordExpiredException ex)
                        {
                            iOutputDetail.CrawlErrorType = CrawlErrorType.PasswordExpired;
                            LogUtil.LogError(ex);
                        }
                        catch (InvalidTaxCodeException ex)
                        {
                            iOutputDetail.CrawlErrorType = CrawlErrorType.InvalidTaxCode;
                            LogUtil.LogError(ex);
                        }
                        catch (Exception ex)
                        {
                            iOutputDetail.CrawlErrorType = CrawlErrorType.Other;
                            LogUtil.LogError(ex);
                        }
                        finally
                        {
                            AppendProgress(70 / syncInput.Details.Count);
                            output.Details.Add(iOutputDetail);
                        }
                    }
                }

                if(output.Details.Count > 0)
                {
                    _aspClient.SetAuthentication(syncInput.AccessToken);
                    await _aspClient.InsertTaxDecSubmitted(syncInput.ProfileID, output);
                    UpdateProgress(80);
                    await UploadFileToServer(syncInput.ProfileID, output.Details);
                }
                
                UpdateProgress(100);
                ShowSuccessForm();
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex);
                ShowErrorForm();
            }
            finally
            {
                Program.InProcessing = false;
            }
        }

        private async Task UploadFileToServer(int profileID, List<SyncTaxDecSubmittedOutputDetail> details)
        {
            if (details != null && details.Count > 0)
            {
                foreach (var iOutput in details)
                {
                    try
                    {
                        if (iOutput.TaxDeclarationSubmitteds != null && iOutput.TaxDeclarationSubmitteds.Count > 0)
                        {
                            foreach (var iTaxDec in iOutput.TaxDeclarationSubmitteds)
                            {
                                try
                                {
                                    var localFolderPath = $"{FileUtil.BASE_PATH}/OutputFiles/{iTaxDec.TaxCode}/{iTaxDec.TransactionID}";
                                    if (!String.IsNullOrEmpty(iTaxDec.FileName))
                                    {
                                        await _aspClient.UploadFileToServer(
                                            profileID,
                                            FileTypeEnum.TaxDeclaration,
                                            localFolderPath,
                                            iTaxDec.FileName,
                                            iOutput.CustomerID,
                                            iTaxDec.DisplayTransactionID
                                        );
                                        await Task.Delay(200);
                                    }

                                    if (iTaxDec.Notifications != null && iTaxDec.Notifications.Count > 0)
                                    {
                                        foreach (var iTaxNoti in iTaxDec.Notifications)
                                        {
                                            try
                                            {
                                                if (!String.IsNullOrEmpty(iTaxNoti.FileName))
                                                {
                                                    await _aspClient.UploadFileToServer(
                                                        profileID,
                                                        FileTypeEnum.TaxNotification,
                                                        localFolderPath,
                                                        iTaxNoti.FileName,
                                                        iOutput.CustomerID,
                                                        iTaxDec.DisplayTransactionID
                                                    );
                                                    await Task.Delay(200);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LogUtil.LogError(ex);
                                                // Không throw ex khi 1 thông báo upload lỗi
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogUtil.LogError(ex);
                                    // Không throw ex khi 1 tờ khai upload lỗi
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError(ex);
                        // Không throw ex khi 1 khách hàng lỗi
                    }
                }
            }
        }

        #region Thao tác với Form Control
        private void UpdateProgress(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(UpdateProgress), value);
            }
            else
            {
                progressBar1.Value = value;
            }
        }

        private void AppendProgress(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(AppendProgress), value);
            }
            else
            {
                progressBar1.Value = Math.Min(100, progressBar1.Value + value);
            }
        }

        private void ShowSuccessForm()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ShowSuccessForm));
            }
            else
            {
                var frmSuccess = new frmSuccess("Đã hoàn thành đồng bộ tờ khai");
                frmSuccess.ShowDialog();
            }
        }

        private void ShowErrorForm()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ShowErrorForm));
            }
            else
            {
                var frmError = new frmError("Đã có lỗi xảy ra, vui lòng thực hiện đồng bộ lại");
                frmError.ShowDialog();
            }
        }
        #endregion
    }
}
