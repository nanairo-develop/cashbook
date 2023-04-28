namespace cashbook.dto
{
    internal class TPurchaseDto
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public int Destination { get; set; }
        public int Manager { get; set; }
        public string SlipNumber { get; set; }
        public TPurchaseDto(DateTime date,
                            object? destination,
                            object? manager,
                            string slipNumber)
        {
            Id = 0;
            PayDate = date;
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
            SlipNumber = slipNumber;
        }
    }
}
