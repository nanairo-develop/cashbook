namespace Cashbook
{
    internal class FormPurchaseListDto
    {
        public enum PurchaseListColumns
        {
            id = 0,
            payDate,
            destinationId,
            destination,
            managerId,
            manager,
            slipNumber,
            memo
        }
        public int OfficeId { get; set; }
        public int ManagerId { get; set; }
        public DateTime PayDateFrom { get; set; }
        public DateTime PayDateTo { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
