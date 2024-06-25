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
    internal class MenuViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        public ReactiveCommand<Unit, Unit> RecipesListCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCategoriesCommand { get; }
        public MenuViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            RecipesListCommand = ReactiveCommand.Create(RecipesList);
            ShowCategoriesCommand = ReactiveCommand.Create(ShowCategories);
        }

        private void RecipesList()
        {
            _mainWindowViewModel.ShowRecipes();
        }

        private void ShowCategories()
        {
            _mainWindowViewModel.ShowCategories();
        }
    }
}
