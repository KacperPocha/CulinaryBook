using CulinaryBook.Models;
using CulinaryBook.Persistance;
using ReactiveUI;

namespace CulinaryBook.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        private readonly IAppDbContext _appDbContext;

        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            _appDbContext = new AppDbContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            CurrentView = new MenuViewModel(this, _appDbContext);
        }

        public void ShowMenu()
        {
            CurrentView = new MenuViewModel(this, _appDbContext);
        }

        public void ShowRecipes()
        {
            CurrentView = new RecipesListViewModel(this, _appDbContext);
        }

        public void ShowCreateRecipe()
        {
            CurrentView = new CreateRecipeViewModel(this, _appDbContext);
        }

        public void ShowCreateRecipe(Recipe recipe)
        {
            CurrentView = new CreateRecipeViewModel(this, _appDbContext, recipe);
        }

        public void ShowCategories()
        {
            CurrentView = new CategoriesViewModel(this, _appDbContext);
        }
    }
}
