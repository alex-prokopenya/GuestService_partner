namespace GuestService.Data
{
    using Sm.System.Database;
    using System;
    using System.Data;

    public static class CompleteOperationProvider
    {
        public static CompleteOperationResult GetResult(string id)
        {
            DataRowCollection rows = DatabaseOperationProvider.QueryProcedure("up_asyncOperationGetResult", "result", new { id = id }).Tables["result"].Rows;
            if (rows.Count > 0)
            {
                DataRow row = rows[0];
                return new CompleteOperationResult { Id = row.ReadNullableTrimmedString("id"), ResultDate = row.ReadUnspecifiedDateTime("rdate"), DataType = row.ReadNullableTrimmedString("dtype"), Data = row.ReadNullableString("data") };
            }
            return null;
        }

        public static bool IsFinished(string id)
        {
            DataRowCollection rows = DatabaseOperationProvider.QueryProcedure("up_asyncOperationFinished", "result", new { id = id }).Tables["result"].Rows;
            return ((rows.Count > 0) && rows[0].ReadBoolean("finished"));
        }

        public static void SetResult(string id, string dataType, string data)
        {
            DatabaseOperationProvider.ExecuteProcedure("up_asyncOperationSetResult", new { id = id, dtype = dataType, data = data });
        }

        public static string Start()
        {
            DataRowCollection rows = DatabaseOperationProvider.QueryProcedure("up_asyncOperationStart", "result", null).Tables["result"].Rows;
            if (rows.Count > 0)
            {
                return rows[0].ReadNullableTrimmedString("id");
            }
            return null;
        }
    }
}

