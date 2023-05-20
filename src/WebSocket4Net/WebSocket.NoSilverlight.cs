using SuperSocket.ClientEngine;
using System.Security.Authentication;

namespace WebSocket4Net
{
    public partial class WebSocket
    {
#if NETCORE || NET6_0_OR_GREATER
       private SslProtocols m_SecureProtocols = SslProtocols.Tls11 | SslProtocols.Tls12;
#else
        private SslProtocols m_SecureProtocols = SslProtocols.Default;
#endif

        private TcpClientSession CreateSecureTcpSession()
        {
            var client = new SslStreamTcpSession();
            var security = client.Security = new SecurityOption();
            security.EnabledSslProtocols = m_SecureProtocols;
            return client;
        }
    }
}
