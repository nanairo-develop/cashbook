using cashbook.dto;
using System.Text;

namespace cashbook.dao
{
    internal class MOfficeDao
    {
        #region SELECT
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

        public static string GetSelectOffice(string condition)
        {
            string nameCondition = "";
            if (condition != string.Empty)
            {
                nameCondition = $"""
                    AND name LIKE '%{condition}%'
                    """;
            }
            string query = $"""
                SELECT
                    id,
                    name,
                    displayOrder
                FROM
                    m_office
                WHERE
                    1 = 1
                {nameCondition}
                ORDER BY
                    displayOrder DESC
                ;
                """;
            return query;
        }

        public static string GetSelectOffice(MOfficeDto officeDto)
        {
            return $"""
                SELECT
                    id,
                    name,
                    displayOrder
                FROM
                    m_office
                WHERE
                    name = '{officeDto.Name}'
                """;
        }
        #endregion SELECT

        #region INSERT
        public static string GetInsertOffice(MOfficeDto officeDto)
        {
            StringBuilder ret = new();
            _ = ret.AppendLine("INSERT INTO m_office");
            _ = ret.AppendLine("(");
            _ = ret.AppendLine("    name,");
            _ = ret.AppendLine("    displayOrder");
            _ = ret.AppendLine(")");
            _ = ret.AppendLine("VALUES");
            _ = ret.AppendLine("(");
                ret.AppendFormat("    '{0}',", officeDto.Name);
            if (officeDto.DisplayOrder is null)
            {
                _ = ret.AppendLine("    null");
            }
            else
            {
                _ = ret.AppendFormat("    {0},", officeDto.DisplayOrder);
                _ = ret.AppendLine();

            }
            _ = ret.AppendLine(");");
            return ret.ToString();
        }
        #endregion INSERT

        #region UPDATE
        public static string GetUpdateOffice(MOfficeDto officeDto)
        {
            StringBuilder ret = new();
            _ = ret.AppendLine("UPDATE m_office");
            _ = ret.AppendLine("SET");
            if (officeDto.DisplayOrder is null)
            {
                _ = ret.AppendLine("    displayOrder = null");
            }
            else
            {
                _ = ret.AppendFormat("    displayOrder = {0}", officeDto.DisplayOrder);
                _ = ret.AppendLine();
            }
            _ = ret.AppendLine("WHERE");
            _ = ret.AppendFormat("    id = {0};", officeDto.Id);
            return ret.ToString();
        }
        #endregion UPDATE
    }
}
