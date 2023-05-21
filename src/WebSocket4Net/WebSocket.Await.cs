namespace WebSocket4Net
{
    public partial class WebSocket
    {
#if AndreasReitberger
        private TaskCompletionSource<bool>? m_OpenTaskSrc;
        private TaskCompletionSource<bool>? m_CloseTaskSrc;
#else
        private TaskCompletionSource<bool> m_OpenTaskSrc;
        private TaskCompletionSource<bool> m_CloseTaskSrc;
#endif
         
        public async Task<bool> OpenAsync()
        {
            var openTaskSrc = m_OpenTaskSrc;

            if (openTaskSrc != null)
                return await openTaskSrc.Task;

            openTaskSrc = m_OpenTaskSrc = new TaskCompletionSource<bool>();
            Open();
            return await openTaskSrc.Task;
        }

        public async Task<bool> CloseAsync()
        {
            var closeTaskSrc = m_CloseTaskSrc;

            if (closeTaskSrc != null)
                return await closeTaskSrc.Task;

            closeTaskSrc = m_CloseTaskSrc = new TaskCompletionSource<bool>();
            Close();
            return await closeTaskSrc.Task;
        }

        private void FinishOpenTask()
        {
#if AndreasReitberger
            ///Reference: https://github.com/AndreasReitberger/KlipperRestApiSharp/issues/45
            bool? succeeded = m_OpenTaskSrc?.TrySetResult(StateCode == WebSocketStateConst.Open);
            if(succeeded is false) 
            {

            }
#else
            m_OpenTaskSrc?.SetResult(this.StateCode == WebSocketStateConst.Open);
#endif
            m_OpenTaskSrc = null;
        }

        private void FinishCloseTask()
        {
#if AndreasReitberger
            ///Reference: https://github.com/AndreasReitberger/KlipperRestApiSharp/issues/45
            bool? succeeded = m_CloseTaskSrc?.TrySetResult(StateCode == WebSocketStateConst.Closed);
            if(succeeded is false) 
            {

            }
#else
            m_CloseTaskSrc?.SetResult(this.StateCode == WebSocketStateConst.Closed);
#endif
            m_CloseTaskSrc = null;
        }

        partial void OnInternalOpened()
        {
            FinishOpenTask();
        }

        partial void OnInternalClosed()
        {
            FinishOpenTask();
            FinishCloseTask();
        }

        partial void OnInternalError()
        {
            FinishOpenTask();
            FinishCloseTask();
        }
    }
}
