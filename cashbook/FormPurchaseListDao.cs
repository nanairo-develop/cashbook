namespace Cashbook
{
    internal class FormPurchaseListDao
    {
        public static string GetWherePurchase(FormPurchaseListDto purchaseListDto)
        {
            string where = $"""
                tp.payDate BETWEEN '{purchaseListDto.PayDateFrom:d}' AND '{purchaseListDto.PayDateTo:d}'
                """;
            if (purchaseListDto.OfficeId != 0)
            {
                where = $"""
                    {where}
                    AND tp.destination = {purchaseListDto.OfficeId}
                    """;
            }
            if (purchaseListDto.ManagerId != 0)
            {
                where = $"""
                    {where}
                    AND tp.manager = {purchaseListDto.ManagerId}
                    """;
            }
            return where;
        }
        public static string GetSelectPurchase(FormPurchaseListDto purchaseListDto)
        {
            return $"""
                SELECT
                    tp.id,
                    tp.payDate,
                    tp.destination AS destinationId,
                    mo.name AS destination,
                    tp.manager AS managerId,
                    mm.name AS manager,
                    tp.slipNumber,
                    tp.memo
                FROM
                    t_purchase tp
                LEFT JOIN
                    m_office mo
                ON
                    tp.destination = mo.id
                LEFT JOIN
                    m_manager mm
                ON
                    tp.manager = mm.id
                WHERE
                    {GetWherePurchase(purchaseListDto)}
                ORDER BY
                    tp.payDate
                ;
                """;
        }
    }
}
