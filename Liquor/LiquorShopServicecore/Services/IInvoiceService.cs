using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> FindInvoiceByIDAsync(int id);
        Task<Invoice> FindInvoiceByInvoiceNumberAsync(int id);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        int SaveInvoice(Invoice Invoice);
        Task<int> DeletePurchaseAsync(Invoice Invoice);
    }
}
