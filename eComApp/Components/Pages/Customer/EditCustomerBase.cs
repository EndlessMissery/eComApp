using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Customer
{
    public class EditCustomerBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        private AppDbContext _context = null!;
        protected DB.Entities.Customer Customer = new();

        protected override async Task OnInitializedAsync()
        {
            _context = await DbContextFactory.CreateDbContextAsync();
            Customer = await _context.Customers.FindAsync(id) ?? new DB.Entities.Customer();
        }

        protected async Task SaveCustomer()
        {
            if (Customer.Id == 0)
            {
                _context.Customers.Add(Customer);
            }
            else
            {
                _context.Customers.Update(Customer);
            }

            await _context.SaveChangesAsync();
            NavigationManager.NavigateTo("/customers");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/customers");
        }
    }
}