using Frameworks.ViewModel;
using System.Windows;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Courier_Window.xaml
    /// </summary>
    public partial class Courier_Window : Window
    {
        public Courier_Window(int id_courier)
        {
            InitializeComponent();
            Courier_ViewModel viewModel1 = new Courier_ViewModel(id_courier);
            DataContext = viewModel1;
        }
    }
}
