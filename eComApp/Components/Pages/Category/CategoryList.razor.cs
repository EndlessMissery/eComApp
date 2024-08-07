using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Category
{
    public partial class CategoryListBase : ComponentBase
    {
        protected List<DB.Entities.Category> Categories { get; set; } = new();

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        private AppDbContext _context = null!;

        protected override async Task OnInitializedAsync()
        {
            _context = await DbContextFactory.CreateDbContextAsync();
            await LoadCategories();
        }

        public async Task LoadCategories()
        {
            Categories = await _context.Categories.ToListAsync();
        }

        public void EditCategory(int id)
        {
            NavigationManager.NavigateTo($"/edit-category/{id}");
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                await LoadCategories();
            }
        }
    }
}