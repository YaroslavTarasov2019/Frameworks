using Frameworks.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Restaurant_Window.xaml
    /// </summary>
    public partial class Restaurant_Window : Window
    {
        public Restaurant_Window(int id_restaurant)
        {
            InitializeComponent();

            Restaurant_ViewModel viewModel1 = new Restaurant_ViewModel(id_restaurant);
            DataContext = viewModel1;
        }
    }
}
