namespace cashbook
{
    internal class FormPurchaseListDao
    {
        public static string GetSelectPurchase(DateTime payDateFrom, DateTime payDateTo)
        {
            return $"""
                SELECT
                    tp.id,
                    tp.payDate,
                    tp.destination AS destinationId,
                    mo.name AS destination,
                    tp.manager AS managerId,
                    mm.name AS manager,
                    tp.slipNumber
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
                    tp.payDate BETWEEN '{payDateFrom:d}' AND '{payDateTo:d}'
                ORDER BY
                    tp.payDate
                ;
                """;
        }
    }
}
