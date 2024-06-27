using CulinaryBook.Models;
using CulinaryBook.Persistance;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.ViewModels
{
    internal class RecipesListViewModel : ViewModelBase
    {
        private ObservableCollection<Recipe> _recipes;
        private Recipe _selectedRecipe;
        private RecipeService _recipeService;
        private MainWindowViewModel _mainWindowViewModel;
        private bool _sortByCategory;
        private string _filterCategory;
        private string _searchQuery;

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set => this.RaiseAndSetIfChanged(ref _recipes, value);
        }

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
        }

        public bool SortByCategory
        {
            get => _sortByCategory;
            set
            {
                if (_sortByCategory != value)
                {
                    _sortByCategory = value;
                    RefreshRecipes();
                }
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuery, value);
                RefreshRecipes();
            }
        }

        public ReactiveCommand<Unit, Unit> CreateRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> EditRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenRecipeCommand { get; }

        public RecipesListViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, string filterCategory = null)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _recipeService = new RecipeService(appDbContext);
            _filterCategory = filterCategory;

            CreateRecipeCommand = ReactiveCommand.Create(CreateRecipe);
            RemoveRecipeCommand = ReactiveCommand.Create(RemoveRecipe);
            EditRecipeCommand = ReactiveCommand.Create(EditRecipe);
            BackCommand = ReactiveCommand.Create(Back);
            OpenRecipeCommand = ReactiveCommand.Create(OpenRecipe);

            Recipes = new ObservableCollection<Recipe>();
            RefreshRecipes();
        }

        private void CreateRecipe()
        {
            _mainWindowViewModel.ShowCreateRecipe();
        }

        private async void RemoveRecipe()
        {
            if (_selectedRecipe != null)
            {
                await _recipeService.DeleteRecipe(_selectedRecipe.Id);
                RefreshRecipes();
            }
        }

        private void EditRecipe()
        {
            if (_selectedRecipe != null)
            {
                _mainWindowViewModel.ShowCreateRecipe(_selectedRecipe);
            }
        }

        private void Back()
        {
            _mainWindowViewModel.ShowMenu();
        }

        private void OpenRecipe()
        {
            if (_selectedRecipe != null)
            {
                _mainWindowViewModel.ShowRecipeDetails(_selectedRecipe);
            }
        }

        public void RefreshRecipes()
        {
            var recipes = _recipeService.GetRecipes();

            if (!string.IsNullOrEmpty(_filterCategory))
            {
                recipes = recipes.Where(r => r.Category == _filterCategory).ToList();
            }

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                recipes = recipes.Where(r => r.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (SortByCategory)
            {
                Recipes = new ObservableCollection<Recipe>(recipes.OrderBy(b => b.Category));
            }
            else
            {
                Recipes = new ObservableCollection<Recipe>(recipes);
            }
        }
    }
}
