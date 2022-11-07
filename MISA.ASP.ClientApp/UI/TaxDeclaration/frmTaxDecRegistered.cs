using MISA.ASP.ClientApp.BL;
using MISA.ASP.ClientApp.Models.ActionInput.SynTaxDec;
using MISA.ASP.ClientApp.Models.ActionOutput.SyncTaxDec;
using MISA.ASP.ClientApp.Models.Enums;
using MISA.ASP.ClientApp.Models.EtaxCrawler;
using MISA.ASP.ClientApp.Models.Exceptions;
using MISA.ASP.ClientApp.UI.Common;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISA.ASP.ClientApp.UI.TaxDeclaration
{
    public partial class frmTaxDecRegistered : Form
    {
        public frmTaxDecRegistered()
        {
            InitializeComponent();
            Program.InProcessing = true;
            this.TopMost = true;
        }

        public void ShowForm(string actionID, string data)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new Action<string, string>(ShowForm), actionID, data);
            }
            else
            {
                this.Show();
                UpdateProgress(5);
                Task.Factory.StartNew(() =>
                {
                    _ = SyncTaxDecRegistered(actionID, data);
                });
            }
        }

        private void frmTaxDecRegistered_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.InProcessing = false;
        }

        public async Task SyncTaxDecRegistered(string actionID, string data)
        {
            try
            {
                var output = new SyncTaxDecRegisteredOutput() { ActionID = actionID };
                var syncInput = JsonConvert.DeserializeObject<SyncTaxDecRegisteredInput>(data);
                UpdateProgress(10);
                if(syncInput != null && syncInput.Details != null && syncInput.Details.Count > 0)
                {
                    var isLocalMode = bool.Parse(ConfigurationManager.AppSettings["IsLocalMode"]);
                    foreach (var detail in syncInput.Details)
                    {
                        var iOutputDetail = new SyncTaxDecRegisteredOutputDetail() { CustomerID = detail.CustomerID };
                        try
                        {
                            if(!isLocalMode)
                            {
                                var crawler = new ETaxCrawler(syncInput.ProfileID, detail.CustomerID, detail.TaxCode, detail.Username, detail.Password, CancellationToken.None);
                                var taxDecRegistered = await crawler.GetTaxDecRegistered();
                                iOutputDetail.TaxDeclarationRegistereds = taxDecRegistered;
                            }
                            else
                            {
                                using (var r = new StreamReader($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "")}"))
                                {
                                    string json = r.ReadToEnd();
                                    List<TaxDeclarationRegistered> items = JsonConvert.DeserializeObject<List<TaxDeclarationRegistered>>(json);
                                    iOutputDetail.TaxDeclarationRegistereds = items;
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

                if (output.Details.Count > 0)
                {
                    var aspClient = new ASPClient();
                    aspClient.SetAuthentication(syncInput.AccessToken);
                    await aspClient.InsertTaxDecRegistered(syncInput.ProfileID, output);
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

        #region Thao tác với Form Control
        private void UpdateProgress(int value)
        {
            if(InvokeRequired)
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
            if(InvokeRequired)
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
