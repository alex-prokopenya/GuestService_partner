namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;

    [Serializable]
    public class CompleteOperation
    {
        private const string SessionStateName = "completeOperation";

        public void Clear()
        {
            this.OperationId = null;
            this.OperationResultTime = null;
            this.OperationResultType = null;
            this.OperationResultData = null;
        }

        public static CompleteOperation CreateFromSession(HttpSessionStateBase session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            CompleteOperation operation = session["completeOperation"] as CompleteOperation;
            if (operation == null)
            {
                session["completeOperation"] = operation = new CompleteOperation();
            }
            return operation;
        }

        public bool IsFinished()
        {
            if (this.OperationId == null)
            {
                if (!this.OperationResultTime.HasValue)
                {
                    throw new Exception("opearion is not started");
                }
                return true;
            }
            if (CompleteOperationProvider.IsFinished(this.OperationId))
            {
                CompleteOperationResult result = CompleteOperationProvider.GetResult(this.OperationId);
                if (result != null)
                {
                    this.OperationId = null;
                    this.OperationResultTime = new DateTime?(result.ResultDate);
                    this.OperationResultType = result.DataType;
                    this.OperationResultData = result.Data;
                    return true;
                }
            }
            return false;
        }

        public void Start()
        {
            this.Clear();
            this.OperationId = CompleteOperationProvider.Start();
        }

        public bool HasResult
        {
            get
            {
                return this.OperationResultTime.HasValue;
            }
        }

        public bool IsProgress
        {
            get
            {
                return (this.OperationId != null);
            }
        }

        public string OperationId { get; set; }

        public string OperationResultData { get; set; }

        public DateTime? OperationResultTime { get; set; }

        public string OperationResultType { get; set; }
    }
}

