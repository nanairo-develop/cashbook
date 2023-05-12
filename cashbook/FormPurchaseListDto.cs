namespace cashbook
{
    internal class FormPurchaseListDto
    {
        public int OfficeId { get; set; }
        public int ManagerId { get; set; }
        public DateTime PayDateFrom { get; set; }
        public DateTime PayDateTo { get; set; }
    }
}
