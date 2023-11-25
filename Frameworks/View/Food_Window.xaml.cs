using Frameworks.ViewModel;
using System.Windows;

namespace Frameworks.View
{
    /// <summary>
    /// Логика взаимодействия для Food_Window.xaml
    /// </summary>
    public partial class Food_Window : Window
    {
        public Food_Window(int id_food, int id_person)
        {
            InitializeComponent();

            Food_ViewModel viewModel1 = new Food_ViewModel(id_food, id_person);
            DataContext = viewModel1;
        }
    }
}