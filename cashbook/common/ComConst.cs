namespace Cashbook.common
{
    public class ComConst
    {
        /// <summary>接続文字列</summary>
        public static readonly string connStr = "server=127.0.0.1;user id=root;password=p@ssword;database=cashbook";

        /// <summary>画面起動モード</summary>
        public class Mode
        {
            /// <summary>画面起動モード：編集</summary>
            public static readonly int edit;
            /// <summary>画面起動モード：選択</summary>
            public static readonly int select = 1;
        }

        public class FormPurchaseDetail
        {
            public enum DataSumColumns
            {
                receivableSum = 0,
                payableSum
            }
            public enum DetailListColumns
            {
                blanchId = 0,
                description,
                receivable,
                payable,
                useforfood
            }

        }
    }
}
