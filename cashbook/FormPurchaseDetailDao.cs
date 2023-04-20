namespace cashbook
{
    internal class FormPurchaseDetailDao
    {
        public static string GetPurchaseInsert(DateTime payDate, object? office, object? manager, string slipNumber)
        {
            return $"""
                INSERT INTO t_purchase
                (
                    payDate,
                    destination,
                    manager,
                    slipNumber
                )
                VALUES(
                    '{payDate:d}'.
                    {office},
                    {manager},
                    '{slipNumber}'
                );
                """;
        }
        public static string GetSelectPurchaseDetail(int purchaseId)
        {
            return $"""
                SELECT
                    branchId,
                    description,
                    receivable,
                    payable,
                    useforfood
                FROM
                    t_purchaseDetail
                WHERE
                    purchaseId = {purchaseId}
                ;
                """;
        }
        public static string GetSelectPurchaseId()
        {
            return $"""
                SELECT
                    MAX(id) AS id
                FROM
                    t_purchase
                ;
                """;
        }
        public static string GetSelectOffice()
        {
            return $"""
                SELECT
                    id,
                    name
                FROM
                    m_office
                ORDER BY
                    displayOrder DESC
                ;
                """;
        }
        public static string GetSelectManager(DateTime payDate)
        {
            return $"""
                SELECT
                    id,
                    name
                FROM
                    m_manager
                WHERE
                    '{payDate:d}' BETWEEN validFrom AND validTo
                ;
                """;
        }


    }
}
