namespace cashbook.dto
{
    internal class MManagerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Exist { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public MManagerDto(int id, string name, bool exist, DateTime validFrom, DateTime validTo)
        {
            Id = id;
            Name = name;
            Exist = exist;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }
    }
}
