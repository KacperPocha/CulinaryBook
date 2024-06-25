using CulinaryBook.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.ViewModels
{
    internal class CreateRecipeViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        public CreateRecipeViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
    }
}
