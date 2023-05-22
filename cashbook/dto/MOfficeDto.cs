namespace cashbook.dto
{
    internal class MOfficeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public string Error { get; set; }

        public MOfficeDto(string name)
        {
            Name = name;
            try
            {
                if (Name == string.Empty)
                {
                    Name = string.Empty;
                    ArgumentNullException argumentNullException = new(nameof(Name), "Invalid name");
                    throw argumentNullException;
                }
            }
            catch (ArgumentNullException ex)
            {
                Error = ex.Message;
            }
            finally
            {
                Error ??= string.Empty;
            }
        }
    }
}
