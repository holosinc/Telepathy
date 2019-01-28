using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Telepathy
{
    public abstract class Common
    {
        // nagle: disabled by default
        public bool NoDelay = true;

        // receive buffers
        protected BigBuffer ReceiveBigBuffers;
        protected const int OpsToAlloc = 2;

        protected abstract void ProcessReceive(SocketAsyncEventArgs e);

        protected void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;

                case SocketAsyncOperation.Send:
                    Logger.LogError("Client.IO_Completed: Send should never happen.");
                    break;

                default:
                    Logger.LogError("The last operation completed on the socket was not a receive or send");
                    break;
            }
        }
    }
}