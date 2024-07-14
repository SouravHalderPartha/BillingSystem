namespace BillingSystem.Models.Repositories
{
    public interface IOpeningInvoicesRepository
    {
        IEnumerable<OpeningInvoice> GetAllOpeningInvoices();
        Task<IEnumerable<OpeningInvoice>> GetAllOpeningInvoicesAsync();

        Task<int> UpdateOpeningInvoicesAsync(OpeningInvoice openingInvoice);
    }
}
