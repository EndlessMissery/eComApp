using eComApp.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace eComApp.Components.Pages.Category
{
    public class EditCategoryBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        private AppDbContext _context = null!;
        protected DB.Entities.Category Category = new();

        protected override async Task OnInitializedAsync()
        {
            _context = await DbContextFactory.CreateDbContextAsync();
            Category = await _context.Categories.FindAsync(Id) ?? new DB.Entities.Category();
        }

        protected async Task SaveCategory()
        {
            if (Category.Id == 0)
            {
                _context.Categories.Add(Category);
            }
            else
            {
                _context.Categories.Update(Category);
            }

            await _context.SaveChangesAsync();
            NavigationManager.NavigateTo("/categories");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}