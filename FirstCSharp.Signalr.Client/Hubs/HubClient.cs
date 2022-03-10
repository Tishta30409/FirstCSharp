
namespace FirstCSharp.Signalr.Client.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Autofac.Features.Indexed;
    using Microsoft.AspNet.SignalR.Client;
    using FirstCSharp.Domain.KeepAliveConn;
    using FirstCSharp.Signalr.Client.Model;
    using System.Threading;

    public class HubClient : IHubClient
    {
        /// <summary>
        /// handler集合
        /// </summary>
        private IIndex<string, IActionHandler> handlerSets;

        private bool IsProcessing;

        private int ConnectTag = 0;

        public HubClient(string url, string hubName, IIndex<string, IActionHandler> handlerSets)
        {
            Url = url;
            HubName = hubName;
            this.handlerSets = handlerSets;
            // 處理中
            this.IsProcessing = false;
            // 當前連線號碼
            this.ConnectTag = -1;
        }

        public override async void BroadCastAction(string str)
        {
            try
            {
                var actionModule = ActionModule.FromString(str);

                if (this.handlerSets.TryGetValue(actionModule.Action.ToLower(), out var handler))
                {
                    //測試 3秒Delay
                    var second = 0;
                    while (!SpinWait.SpinUntil(() => false, 1000) && second < 3)
                    {
                        second += 1;
                    }

                    this.UnlockProcess();

                    handler.Execute(actionModule);
                }
            }
            catch (Exception)
            {
            }
        }

        public override async Task<ActionModule> GetAction<T>(T act)
        {
            var sendAction = new ActionModule()
            {
                Action = act.Action(),
                Message = act.ToString()
            };

            if (this.State != ConnectionState.Connected)
            {
                return null;
            }

            var str = await this.HubProxy?.Invoke<string>("GetAction", sendAction.ToString()).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return string.Empty;
                }
                else
                {
                    return task.Result;
                }
            });

            return string.IsNullOrEmpty(str) ? null : ActionModule.FromString(str);
        }

        public override void SendAction<T>(T act)
        {
            var sendAction = new ActionModule()
            {
                Action = act.Action(),
                Message = act.ToString()
            };

            //如果未連線 或是處理中就返回
            if (this.State != ConnectionState.Connected || IsProcessing)
            {
                return;
            }

            this.IsProcessing = true; 

            this.HubProxy?.Invoke<string>("SendAction", sendAction.ToString());

            //每次呼叫 超過時間就
            //測試 3秒Delay
        }

        public override async Task StartAsync()
        {
            this.HubConnection = new HubConnection(Url);
            this.HubConnection.TransportConnectTimeout = TimeSpan.FromSeconds(30);
            this.HubProxy = HubConnection.CreateHubProxy(HubName);
            this.HubProxy.On<string>("BroadCastAction", str => this.BroadCastAction(str));
            // 連線開啟
            await this.HubConnection.Start().ContinueWith(task =>
            {
            });
        }

        public int GetNextPackageNum()
        {
            this.ConnectTag += 1;
            return this.ConnectTag;
        }

        public override bool GetProcessState()
        {
            return this.IsProcessing;
        }

        public override void UnlockProcess()
        {
            this.IsProcessing = false; 
        }
    }
}
