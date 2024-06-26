using CulinaryBook.Models;
using CulinaryBook.Persistance;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.ViewModels
{
    public class CreateRecipeViewModel : ViewModelBase
    {
        private string _recipeName;
        private string _recipeCategory;
        private string _recipeIngredients;
        private string _recipeDirections;
        private string _recipeTaste;
        private string _recipeTime;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly RecipeService _recipeService;
        private readonly Recipe _editingRecipe;

        public string RecipeName
        {
            get => _recipeName;
            set => this.RaiseAndSetIfChanged(ref _recipeName, value);
        }

        public string RecipeCategory
        {
            get => _recipeCategory;
            set => this.RaiseAndSetIfChanged(ref _recipeCategory, value);
        }

        public string RecipeIngredients
        {
            get => _recipeIngredients;
            set => this.RaiseAndSetIfChanged(ref _recipeIngredients, value);
        }

        public string RecipeDirections
        {
            get => _recipeDirections;
            set => this.RaiseAndSetIfChanged(ref _recipeDirections, value);
        }

        public string RecipeTaste
        {
            get => _recipeTaste;
            set => this.RaiseAndSetIfChanged(ref _recipeTaste, value);
        }

        public string RecipeTime
        {
            get => _recipeTime;
            set => this.RaiseAndSetIfChanged(ref _recipeTime, value);
        }

        public ReactiveCommand<Unit, Unit> CreateRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public CreateRecipeViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
            : this(mainWindowViewModel, appDbContext, null) { }
        public CreateRecipeViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Recipe recipe)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _recipeService = new RecipeService(appDbContext);
            _editingRecipe = recipe;

            if (recipe != null)
            {
                RecipeName = recipe.Title;
                RecipeCategory = recipe.Category;
                RecipeIngredients = recipe.Ingredients;
                RecipeDirections = recipe.Directions;
                RecipeTaste = recipe.Taste;
                RecipeTime = recipe.Time;

            }

            CreateRecipeCommand = ReactiveCommand.CreateFromTask(HandleCreateCommand);
            BackCommand = ReactiveCommand.Create(NavigateBack);
        }

        private async Task HandleCreateCommand()
        {
            if (_editingRecipe == null)
            {
                await _recipeService.CreateRecipe(RecipeName, RecipeCategory, RecipeIngredients, RecipeDirections, RecipeTaste, RecipeTime);
            }
            else
            {
                await _recipeService.UpdateRecipe(_editingRecipe.Id, RecipeName, RecipeCategory, RecipeIngredients, RecipeDirections, RecipeTaste, RecipeTime);
            }
            _mainWindowViewModel.ShowRecipes();
        }

        private void NavigateBack()
        {
            _mainWindowViewModel.ShowRecipes();
        }
    }
}
