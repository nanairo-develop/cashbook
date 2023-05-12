namespace cashbook.dto
{
    internal class TPurchaseDto
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public int Destination { get; set; }
        public int Manager { get; set; }
        public string SlipNumber { get; set; }
        public string Memo { get; set; }
        public TPurchaseDto(object? destination, object? manager)
        {
            SetDestination(destination);
            SetManager(manager);
            SlipNumber ??= string.Empty;
            Memo ??= string.Empty;
        }
        public TPurchaseDto(DateTime date,
                            object? destination,
                            object? manager,
                            string slipNumber,
                            string memo)
        {
            Id = 0;
            PayDate = date;
            SetDestination(destination);
            SetManager(manager);
            SlipNumber = slipNumber;
            Memo = memo;
        }
        public void SetDestination(object? destination)
        {
            if (destination != null)
            {
                if (int.TryParse(destination.ToString(), out int temp))
                {
                    Destination = temp;
                }
            }
            else
            {
                // 0の時はエラーとしたいが、ここに混入する前に防ぐべき
                Destination = 0;
            }
        }
        public void SetManager(object? manager)
        {
            if (manager != null)
            {
                if (int.TryParse(manager.ToString(), out int temp))
                {
                    Manager = temp;
                }
            }
            else
            {
                // 0の時はエラーとしたいが、ここに混入する前に防ぐべき
                Manager = 0;
            }
        }
    }
}
