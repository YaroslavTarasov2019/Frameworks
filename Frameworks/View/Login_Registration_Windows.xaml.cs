using Frameworks.ViewModel;
using System.Windows;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Login_Registration_Windows.xaml
    /// </summary>
    public partial class Login_Registration_Windows : Window
    {
        public Login_Registration_Windows()
        {
            InitializeComponent();

            Entry_ViewModel viewModel1 = new Entry_ViewModel();
            DataContext = viewModel1;
        }
    }
}