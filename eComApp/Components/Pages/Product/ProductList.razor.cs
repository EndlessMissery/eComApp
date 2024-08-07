using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Product
{
    public partial class ProductListBase : ComponentBase
    {
        public List<DB.Entities.Product> Products { get; set; } = new();

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        private AppDbContext _context = null!;

        protected override async Task OnInitializedAsync()
        {
            _context = await DbContextFactory.CreateDbContextAsync();
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                await LoadProducts(); // Refresh the product list
            }
        }

        public void EditProduct(int id)
        {
            NavigationManager.NavigateTo($"/edit-product/{id}");
        }
    }
}