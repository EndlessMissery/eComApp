using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Customer;

public partial class CustomersBase : ComponentBase
{
    [Inject] public required IDbContextFactory<AppDbContext> DbContextFactory { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    protected List<DB.Entities.Customer> Customers = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadCustomersAsync();
    }

    protected async Task LoadCustomersAsync()
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        Customers = await context.Customers.ToListAsync();
    }

    protected async Task DeleteCustomer(int customerId)
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var customer = await context.Customers.FindAsync(customerId);
        if (customer != null)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            await LoadCustomersAsync();
        }
    }

    protected void NavigateToAddCustomer()
    {
        NavigationManager.NavigateTo("/edit-customer/0");
    }
}