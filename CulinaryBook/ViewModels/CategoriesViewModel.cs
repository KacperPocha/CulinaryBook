using CulinaryBook.Persistance;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace CulinaryBook.ViewModels
{
    internal class CategoriesViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        public CategoriesViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;


        }
    }
}
