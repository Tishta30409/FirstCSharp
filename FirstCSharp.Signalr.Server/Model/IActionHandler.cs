
namespace FirstCSharp.Signalr.Server.Model
{
    using System;
    using FirstCSharp.Domain.KeepAliveConn;

    public interface IActionHandler
    {
        /// <summary>
        /// 處理Action事務
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        (Exception exception, ActionBase actionBase) ExecuteAction(ActionModule action);
    }
}
