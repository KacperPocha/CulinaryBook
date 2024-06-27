using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CulinaryBook.ViewModels;

namespace CulinaryBook.Views
{
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is CategoriesViewModel viewModel)
            {
                viewModel.SelectedCategory = (CategoryCount)((ListBox)sender).SelectedItem;
            }
        }
    }
}
