namespace cashbook.dao
{
    internal class MManagerDao
    {
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
    }
}
