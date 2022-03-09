
namespace FirstCSharp.Signalr.Server.Hubs
{
    using System;
    using System.Threading;
    using Microsoft.AspNet.SignalR;
    using NLog;
    using FirstCSharp.Domain.KeepAliveConn;

    /// <summary>
    /// SignalR客戶端
    /// </summary>
    public class HubClient : IHubClient
    {
        private ILogger logger = LogManager.GetLogger("FirstCSharp.Signalr.Server");

        private IHubContext hubContext => GlobalHost.ConnectionManager.GetHubContext<FirstCSharpHub>();

        /// <summary>
        /// 廣撥用
        /// </summary>
        /// <param name="act"></param>
        public void BroadCastAction<A>(A act)
            where A : ActionBase
        {
            var sendAction = new ActionModule()
            {
                Action = act.Action(),
                Message = act.ToString()
            };

            try
            {
                this.logger.Trace($"{this.GetType().Name} BroadCastAction: {sendAction.ToString()}");

                this.hubContext.Clients.All.BroadCastAction(sendAction.ToString());
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} BroadCastAction Exception");
                bool runing = true;
                while (runing)
                {
                    SpinWait.SpinUntil(() => runing = false, 500);
                }

                this.hubContext.Clients.All.BroadCastAction(sendAction.ToString());
            }
        }
    }
}
