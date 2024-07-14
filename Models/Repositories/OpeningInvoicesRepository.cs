
using BillingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.Models.Repositories
{
    public class OpeningInvoicesRepository : IOpeningInvoicesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OpeningInvoicesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public IEnumerable<OpeningInvoice> GetAllOpeningInvoices()
        {
            return _applicationDbContext.OpeningInvoices.OrderBy(p => p.Id);
        }

        public async Task<IEnumerable<OpeningInvoice>> GetAllOpeningInvoicesAsync()
        {
            return await _applicationDbContext.OpeningInvoices.OrderBy(c => c.Id).ToListAsync();

        }

        public async Task<int> UpdateOpeningInvoicesAsync(OpeningInvoice openingInvoice)
        {
            bool openingInvoiceWithSameRateExist = await _applicationDbContext.OpeningInvoices.AnyAsync(c => c.Rate == openingInvoice.Rate && c.Id != openingInvoice.Id);

            if (openingInvoiceWithSameRateExist)
            {
                throw new Exception("A OpeningInvoice with the same Rate already exists");
            }

            var openingInvoiceToUpdate = await _applicationDbContext.OpeningInvoices.FirstOrDefaultAsync(c => c.Id == openingInvoice.Id);

            if (openingInvoiceToUpdate != null)
            {

                openingInvoiceToUpdate.LinkServiceId = openingInvoice.LinkServiceId;
                openingInvoiceToUpdate.Rate = openingInvoice.Rate;

                _applicationDbContext.OpeningInvoices.Update(openingInvoiceToUpdate);
                return await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The category to update can't be found.");
            }
        }

    }
}
