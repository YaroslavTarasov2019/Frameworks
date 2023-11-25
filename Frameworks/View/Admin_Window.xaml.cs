using Frameworks.ViewModel;
using System.Windows;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        public Admin_Window(int id_admin)
        {
            InitializeComponent();
            Admin_ViewModel viewModel1 = new Admin_ViewModel(id_admin);
            DataContext = viewModel1;
        }
    }
}
