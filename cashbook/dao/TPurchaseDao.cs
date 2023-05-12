using cashbook.dto;

namespace cashbook.dao
{
    internal class TPurchaseDao
    {
        public static string GetInsertPurchase(TPurchaseDto tPurchaseDto)
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
                    '{tPurchaseDto.PayDate:d}',
                    {tPurchaseDto.Destination},
                    {tPurchaseDto.Manager},
                    '{tPurchaseDto.SlipNumber}'
                );
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

    }
}
