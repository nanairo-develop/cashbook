namespace cashbook.dto
{
    internal class TPurchaseDetailDto
    {
        public int PurchaseId { get; set; }
        public int BranchId { get; set; }
        public string Description { get; set; }
        public int Receivable { get; set; }
        public int Payable { get; set; }
        public bool UseForFood { get; set; }

        public TPurchaseDetailDto(int purchaseId, int branchId, string description, int receivable, int payable, bool useForFood)
        {
            PurchaseId = purchaseId;
            BranchId = branchId;
            Description = description;
            Receivable = receivable;
            Payable = payable;
            UseForFood = useForFood;
        }
    }
}
