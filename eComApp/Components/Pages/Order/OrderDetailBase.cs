using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Order
{
    public class OrderDetailBase(IDbContextFactory<AppDbContext> dbContextFactory) : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = dbContextFactory;

    }
}