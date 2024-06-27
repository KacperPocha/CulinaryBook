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
    public class CategoriesViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryCount> _categories;
        private readonly IAppDbContext _appDbContext;
        private readonly RecipeService _recipeService;
        private MainWindowViewModel _mainWindowViewModel;
        private CategoryCount _selectedCategory;

        public ObservableCollection<CategoryCount> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public CategoryCount SelectedCategory
        {
            get => _selectedCategory;
            set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
        }

        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenCategoryCommand { get; }

        public CategoriesViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _appDbContext = appDbContext;
            _recipeService = new RecipeService(appDbContext);

            Categories = new ObservableCollection<CategoryCount>();
            RefreshCategories();

            BackCommand = ReactiveCommand.Create(Back);
            OpenCategoryCommand = ReactiveCommand.Create(OpenCategory);
        }

        private void RefreshCategories()
        {
            var recipes = _recipeService.GetRecipes();

            var categoryCounts = recipes.GroupBy(b => b.Category)
                                        .Select(g => new CategoryCount { Category = g.Key, Count = g.Count() })
                                        .OrderBy(c => c.Category);

            Categories.Clear();
            foreach (var categoryCount in categoryCounts)
            {
                Categories.Add(categoryCount);
            }
        }

        private void Back()
        {
            _mainWindowViewModel.ShowMenu();
        }

        private void OpenCategory()
        {
            if (SelectedCategory != null)
            {
                _mainWindowViewModel.ShowRecipesByCategory(SelectedCategory.Category);
            }
        }
    }

    public class CategoryCount
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }
}
