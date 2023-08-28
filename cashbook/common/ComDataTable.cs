using System.Data;

namespace cashbook.common
{
    internal static class ComDataTable
    {
        public static object Data(this DataRow self, Enum columnEnum)
        {
            return self[columnEnum.GetHashCode()];
        }

    }
}
