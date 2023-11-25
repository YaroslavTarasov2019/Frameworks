using Frameworks.ViewModel;
using System.Windows;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Client_Window.xaml
    /// </summary>
    public partial class Client_Window : Window
    {        
        public Client_Window(int id_person)
        {
            InitializeComponent();
            Client_ViewModel viewModel1 = new Client_ViewModel(id_person);
            DataContext = viewModel1;
        }
    }
}
