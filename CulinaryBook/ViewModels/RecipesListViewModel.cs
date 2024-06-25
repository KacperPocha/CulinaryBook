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
using static System.Reflection.Metadata.BlobBuilder;

namespace CulinaryBook.ViewModels
{
    internal class RecipesListViewModel : ViewModelBase
    {
        private ObservableCollection<Recipe> _recipes;
        private Recipe _selectedRecipe;
        private RecipeService _recipeService;
        private MainWindowViewModel _mainWindowViewModel;
        private bool _sortByCategory;

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

        public ReactiveCommand<Unit, Unit> CreateRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> EditRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public RecipesListViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _recipeService = new RecipeService(appDbContext);

            CreateRecipeCommand = ReactiveCommand.Create(CreateRecipe);
            RemoveRecipeCommand = ReactiveCommand.Create(RemoveRecipe);
            EditRecipeCommand = ReactiveCommand.Create(EditRecipe);
            BackCommand = ReactiveCommand.Create(Back);

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

            public void RefreshRecipes()
            {
                var recipes = _recipeService.GetRecipes();

                if (SortByCategory)
                {
                    Recipes= new ObservableCollection<Recipe>(recipes.OrderBy(b => b.Category));
                }
                else
                {
                    Recipes = new ObservableCollection<Recipe>(recipes);
                }
            }
        }
    }
