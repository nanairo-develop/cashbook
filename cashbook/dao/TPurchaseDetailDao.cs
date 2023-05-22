﻿using cashbook.dto;

namespace cashbook.dao
{
    internal class TPurchaseDetailDao
    {
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
        public static string GetInsertPurchaseDetails(List<TPurchaseDetailDto> purchaseDetailDtos)
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
