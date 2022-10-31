using MISA.ASP.ClientApp.Utils.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;

namespace MISA.ASP.ClientApp.Utils.UriHandler
{
    public class UriHandler : MarshalByRefObject, IUriHandler
    {
        const string IPC_CHANNEL_NAME = "ASPSingleInstanceChannel";

        public static bool Register()
        {
            try
            {
                IpcServerChannel channel = new IpcServerChannel(IPC_CHANNEL_NAME);
                ChannelServices.RegisterChannel(channel, true);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(UriHandler), "UriHandler", WellKnownObjectMode.SingleCall);
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex);
            }

            return false;
        }

        public static IUriHandler GetHandler()
        {
            try
            {
                IpcClientChannel channel = new IpcClientChannel();
                ChannelServices.RegisterChannel(channel, true);
                string address = String.Format("ipc://{0}/UriHandler", IPC_CHANNEL_NAME);
                IUriHandler handler = (IUriHandler)RemotingServices.Connect(typeof(IUriHandler), address);

                TextWriter.Null.WriteLine(handler.ToString());

                return handler;
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex);
            }

            return null;
        }

        public void HandleUri(Uri uri)
        {
            _ = Program.MainForm.HandleUri(uri);
        }
    }
}
