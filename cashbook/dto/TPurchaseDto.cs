using Cashbook.common;
using System.Data;

namespace Cashbook.dto
{
    public class TPurchaseDto
    {
        public enum TPurchaseColumns
        {
            id = 0,
            payDate,
            destination,
            manager,
            slipNumber,
            memo
        }
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public int Destination { get; set; }
        public int Manager { get; set; }
        public string SlipNumber { get; set; }
        public string Memo { get; set; }
        public TPurchaseDto()
        {
            SlipNumber ??= string.Empty;
            Memo ??= string.Empty;
        }
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

        public static TPurchaseDto GetFormPurchaseDetailParam(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            TPurchaseDto purchaseDto = new()
            {
                Id = (int)ComDataTable.Data(dr, TPurchaseColumns.id),
                PayDate = (DateTime)ComDataTable.Data(dr, TPurchaseColumns.payDate),
                Destination = (int)ComDataTable.Data(dr, TPurchaseColumns.destination),
                Manager = (int)ComDataTable.Data(dr, TPurchaseColumns.manager),
                SlipNumber = ComDataTable.Data(dr, TPurchaseColumns.slipNumber).ToString() ?? string.Empty,
                Memo = ComDataTable.Data(dr, TPurchaseColumns.memo).ToString() ?? string.Empty
            };

            return purchaseDto;
        }


    }
}
