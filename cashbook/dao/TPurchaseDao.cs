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
        public static string GetSelectDuplicate(TPurchaseDto tPurchaseDto)
        {
            return $"""
                SELECT
                    COUNT(id) AS cnt
                FROM
                    t_purchase
                WHERE
                    payDate = '{tPurchaseDto.PayDate:d}'
                AND destination = {tPurchaseDto.Destination}
                AND slipNumber = '{tPurchaseDto.SlipNumber}'
                ;
                """;
        }
        public static string GetSelectPurchase(int purchaseId)
        {
            return $"""
                SELECT
                    id,
                    payDate,
                    destination,
                    manager,
                    slipNumber,
                    memo
                FROM
                    t_purchase
                WHERE
                    id = {purchaseId}
                ;
                """;
        }

        public static string GetUpdatePurchase(TPurchaseDto beforePurchaseDto, TPurchaseDto afterPurchaseDto)
        {
            string set = string.Empty;
            if (beforePurchaseDto.PayDate != afterPurchaseDto.PayDate) {
                set = $"""
                    payDate = {afterPurchaseDto.PayDate:d},
                    """;
            }
            if (beforePurchaseDto.Destination != afterPurchaseDto.Destination)
            {
                set = $"""
                    {set}
                    destination = {afterPurchaseDto.Destination},
                    """;
            }
            set = set[..^1];
            return $"""
                UPDATE t_purchase
                SET {set}
                WHERE
                    id = {beforePurchaseDto.Id}
                """;
        }
    }
}
