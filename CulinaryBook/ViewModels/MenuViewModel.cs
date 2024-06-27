using CulinaryBook.Models;
using CulinaryBook.Persistance;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace CulinaryBook.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private RecipeService _recipeService;
        public ReactiveCommand<Unit, Unit> RecipesListCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCategoriesCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowRandomRecipeCommand { get; }

        public MenuViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _recipeService = new RecipeService(appDbContext);
            RecipesListCommand = ReactiveCommand.Create(RecipesList);
            ShowCategoriesCommand = ReactiveCommand.Create(ShowCategories);
            ShowRandomRecipeCommand = ReactiveCommand.CreateFromTask(ShowRandomRecipe);
        }

        private void RecipesList()
        {
            _mainWindowViewModel.ShowRecipes();
        }

        private void ShowCategories()
        {
            _mainWindowViewModel.ShowCategories();
        }

        private async Task ShowRandomRecipe()
        {
            var recipes = _recipeService.GetRecipes().ToList();
            if (recipes.Any())
            {
                var random = new Random();
                var randomRecipe = recipes[random.Next(recipes.Count)];
                _mainWindowViewModel.ShowRecipeDetails(randomRecipe);
            }
        }
    }
}
