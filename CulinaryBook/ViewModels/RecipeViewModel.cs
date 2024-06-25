using CulinaryBook.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.ViewModels
{
    internal class RecipeViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        public RecipeViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;


        }
    }
}
