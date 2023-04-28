using cashbook.dto;

namespace cashbook
{
    internal class FormPurchaseDetailDao
    {
        public static string GetPurchaseInsert(TPurchaseDto tPurchaseDto)
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
        public static string GetInsertPurchaseDetail(List<TPurchaseDetailDto> purchaseDetailDtos)
        {

            string values = string.Empty;
            foreach (TPurchaseDetailDto purchaseDetailDto in purchaseDetailDtos)
            
            {
                values = $"""
                (
                    {purchaseDetailDto.PurchaseId},
                    {purchaseDetailDto.BranchId},
                    '{purchaseDetailDto.Description}',
                    {purchaseDetailDto.Receivable},
                    {purchaseDetailDto.Payable},
                    {purchaseDetailDto.UseForFood}
                ),
                """;
            }
            values = values[..^1];


            return $"""
                INSERT INTO t_purchaseDetail
                (
                    purchaseId,
                    branchId,
                    description,
                    receivable,
                    payable,
                    useForFood
                )
                VALUES
                    {values}
                ;
                """;
        }
    }
}
