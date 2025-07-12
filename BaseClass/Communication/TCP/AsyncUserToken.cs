using System;
using System.Net.Sockets;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class AsyncUserToken
    {
        private SocketAsyncEventArgs _receiveEventArgs;

        private SocketAsyncEventArgs _sendEventArgs;

        private byte[] _asyncReceiveBuffer;

        private byte[] _asyncSendBuffer;

        private DynamicBufferManager _receiveBuffer;

        private DynamicBufferManager _sendBuffer;

        private Socket _connectSocket;
        public SocketAsyncEventArgs ReceiveEventArgs
        {
            get
            {
                return this._receiveEventArgs;
            }
            set
            {
                this._receiveEventArgs = value;
            }
        }

        public SocketAsyncEventArgs SendEventArgs
        {
            get
            {
                return this._sendEventArgs;
            }
            set
            {
                this._sendEventArgs = value;
            }
        }

        public Socket ConnectSocket
        {
            get
            {
                return this._connectSocket;
            }
            set
            {
                this._connectSocket = value;
            }
        }

        public DynamicBufferManager ReceiveBuffer
        {
            get
            {
                return this._receiveBuffer;
            }
            set
            {
                this._receiveBuffer = value;
            }
        }

        public DynamicBufferManager SendBuffer
        {
            get
            {
                return this._sendBuffer;
            }
            set
            {
                this._sendBuffer = value;
            }
        }

        public void ClearAsyncSendBuffer()
        {
            bool flag = this._asyncSendBuffer != null;
            if (flag)
            {
                Array.Clear(this._asyncSendBuffer, 0, this._asyncSendBuffer.Length);
            }
        }

        public byte[] AsyncSendBuffer
        {
            get
            {
                return this._asyncSendBuffer;
            }
        }

        public AsyncUserToken(int ReceiveBufferSize)
        {
            this._connectSocket = null;
            this._receiveEventArgs = new SocketAsyncEventArgs();
            this._receiveEventArgs.UserToken = this;
            this._asyncReceiveBuffer = new byte[ReceiveBufferSize];
            this._receiveEventArgs.SetBuffer(this._asyncReceiveBuffer, 0, this._asyncReceiveBuffer.Length);
            this._sendEventArgs = new SocketAsyncEventArgs();
            this._sendEventArgs.UserToken = this;
            this._asyncSendBuffer = new byte[ReceiveBufferSize];
            this._sendEventArgs.SetBuffer(this._asyncSendBuffer, 0, this._asyncSendBuffer.Length);
            this._receiveBuffer = new DynamicBufferManager(ReceiveBufferSize);
            this._sendBuffer = new DynamicBufferManager(ReceiveBufferSize);
        }
    }
}
