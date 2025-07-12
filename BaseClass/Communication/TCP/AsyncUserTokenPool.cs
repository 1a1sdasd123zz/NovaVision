using System;
using System.Collections.Generic;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class AsyncUserTokenPool
    {
        private Stack<AsyncUserToken> m_pool;
        public AsyncUserTokenPool(int capacity)
        {
            this.m_pool = new Stack<AsyncUserToken>(capacity);
        }

        public void Push(AsyncUserToken item)
        {
            bool flag = item == null;
            if (flag)
            {
                throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null");
            }
            Stack<AsyncUserToken> pool = this.m_pool;
            lock (pool)
            {
                this.m_pool.Push(item);
            }
        }

        public AsyncUserToken Pop()
        {
            Stack<AsyncUserToken> pool = this.m_pool;
            AsyncUserToken result;
            lock (pool)
            {
                result = this.m_pool.Pop();
            }
            return result;
        }

        public int Count
        {
            get
            {
                return this.m_pool.Count;
            }
        }
    }
}
