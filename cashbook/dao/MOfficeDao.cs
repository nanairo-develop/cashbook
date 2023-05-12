namespace cashbook.dao
{
    internal class MOfficeDao
    {
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
    }
}
