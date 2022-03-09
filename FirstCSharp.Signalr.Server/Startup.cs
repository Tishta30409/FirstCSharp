[assembly: Microsoft.Owin.OwinStartup(typeof(FirstCSharp.Signalr.Server.Startup))]

namespace FirstCSharp.Signalr.Server
{
    using Microsoft.AspNet.SignalR;
    using Owin;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 解除限制WS傳輸量q
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;

            GlobalHost.Configuration.DefaultMessageBufferSize = 100; // 每個集線器緩存保留的消息，留存過多會造成緩存變高
            app.MapSignalR();
        }

    }
}
