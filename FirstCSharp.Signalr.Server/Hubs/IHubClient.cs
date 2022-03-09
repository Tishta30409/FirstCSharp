namespace FirstCSharp.Signalr.Server.Hubs
{
    using FirstCSharp.Domain.KeepAliveConn;

    public interface IHubClient
    {
        /// <summary>
        /// 廣撥用
        /// </summary>
        void BroadCastAction<A>(A act)
            where A : ActionBase;
    }
}
