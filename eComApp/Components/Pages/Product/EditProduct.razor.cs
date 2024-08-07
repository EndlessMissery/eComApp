using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Product
{
    public class EditProductBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        private AppDbContext _context = null!;
        protected DB.Entities.Product Product = new();
        protected List<DB.Entities.Category> Categories = new();

        protected override async Task OnInitializedAsync()
        {
            _context = await DbContextFactory.CreateDbContextAsync();
            Categories = await _context.Categories.ToListAsync();
            if (Id != 0)
            {
                Product = await _context.Products.FindAsync(Id) ?? new DB.Entities.Product();
            }
        }

        protected async Task SaveProduct()
        {
            if (Product.Id == 0)
            {
                _context.Products.Add(Product);
            }
            else
            {
                _context.Products.Update(Product);
            }

            await _context.SaveChangesAsync();
            NavigationManager.NavigateTo("/products");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/products");
        }
    }
}