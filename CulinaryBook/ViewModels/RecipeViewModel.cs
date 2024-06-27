using CulinaryBook.Models;
using CulinaryBook.Persistance;
using ReactiveUI;
using System.Reactive;

namespace CulinaryBook.ViewModels
{
    internal class RecipeViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private Recipe _recipe;

        public Recipe Recipe
        {
            get => _recipe;
            set => this.RaiseAndSetIfChanged(ref _recipe, value);
        }

        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public RecipeViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Recipe recipe)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _recipe = recipe;

            BackCommand = ReactiveCommand.Create(Back);
        }

        private void Back()
        {
            _mainWindowViewModel.ShowMenu();
        }
    }
}
