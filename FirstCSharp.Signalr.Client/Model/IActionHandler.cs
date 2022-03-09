

namespace FirstCSharp.Signalr.Client.Model
{
    using FirstCSharp.Domain.KeepAliveConn;

    public interface IActionHandler
    {
        bool Execute(ActionModule actionModule);
    }
}
