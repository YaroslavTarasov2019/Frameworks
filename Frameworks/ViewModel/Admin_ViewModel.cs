using Frameworks.Model.Services;
using Frameworks.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frameworks.Model.Subject;
using System.Data;

namespace Frameworks.ViewModel
{
    public class Admin_ViewModel: INotifyPropertyChanged
    {
        SpecialFunctionsForViewModel specialFunctions = new SpecialFunctionsForViewModel();
        public Admin_ViewModel(int id_admin)
        {
            Id_Admin = id_admin;
            ShowFirstElementsCommand = new RelayCommand(ShowFirstElements);
            ShowSecondElementsCommand = new RelayCommand(ShowSecondElements);
            ShowThirdElementsCommand = new RelayCommand(ShowThirdElements);
            ShowFourthElementsCommand = new RelayCommand(ShowFourthElements);
            ClearCommand = new RelayCommand(Clear);
            SaveRCommand = new RelayCommand(SaveR);
            SaveFCommand = new RelayCommand(SaveF);
            SavePriceCommand = new RelayCommand(SavePrice);
            FirstStackPanelItems = new ObservableCollection<DockPanel>();
            YourCommand1 = new RelayCommand(YourCommandExecute1, YourCommandCanExecute1);
            YourCommand2 = new RelayCommand(YourCommandExecute2, YourCommandCanExecute2);
         
            View_ScrollViewer();           
        }
        public int Id_Admin { get; set; }
        public ICommand ShowFirstElementsCommand { get; private set; }
        public ICommand ShowSecondElementsCommand { get; private set; }
        public ICommand ShowThirdElementsCommand { get; private set; }
        public ICommand ShowFourthElementsCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand SaveRCommand { get; private set; }
        public ICommand YourCommand1 { get; private set; }
        public ICommand YourCommand2 { get; private set; }
        public ICommand SaveFCommand { get; private set; }
        public ICommand SavePriceCommand { get; private set; }
        public Visibility FirstElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SecondElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility ThirdElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility FourthElementsVisibility { get; set; } = Visibility.Collapsed;
        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string path_to_photo;
        public string Path_to_Photo
        {
            get { return path_to_photo; }
            set
            {
                path_to_photo = value;
                OnPropertyChanged(nameof(Path_to_Photo));
            }
        }
        public ObservableCollection<DockPanel> FirstStackPanelItems { get; set; }
        private void ShowElements(Visibility firstVisibility, Visibility secondVisibility, Visibility thirdVisibility, Visibility fourthVisibility)
        {
            FirstElementsVisibility = firstVisibility;
            SecondElementsVisibility = secondVisibility;
            ThirdElementsVisibility = thirdVisibility;
            FourthElementsVisibility = fourthVisibility;
            OnPropertyChanged(nameof(FirstElementsVisibility));
            OnPropertyChanged(nameof(SecondElementsVisibility));
            OnPropertyChanged(nameof(ThirdElementsVisibility));
            OnPropertyChanged(nameof(FourthElementsVisibility));
        }
        private void ShowFirstElements(object parameter)
        {
            ShowElements(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);
            View_ScrollViewer();
        }
        private void ShowSecondElements(object parameter)
        {
            ShowElements(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);
        }
        private void ShowThirdElements(object parameter)
        {
            ShowElements(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
        }
        private void ShowFourthElements(object parameter)
        {
            ShowElements(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);            
            DataTable dtf = new Get_Something_Service().Get_All_Food();
            DataTable dtr = new Get_Something_Service().Get_All_Restaurant();
            foreach (DataRow row in dtf.Rows)   ListOfFood += row[0].ToString() + ": " + row[1].ToString() + "\n";            
            foreach (DataRow row in dtr.Rows)   ListOfRestaurant += row[0].ToString() + ": " + row[1].ToString() + "\n";            
        }
        private void Clear(object parameter)
        {
            Name = string.Empty;
            Description = string.Empty;
            Path_to_Photo = string.Empty;
        }
        private void SaveR(object parameter)
        {
            new Add_Service().Add_Restaurant(new Restaurant(Name, Description, Path_to_Photo));
            Name = string.Empty;
            Description = string.Empty;
            Path_to_Photo = string.Empty;
        }
        private void SaveF(object parameter)
        {
            new Add_Service().Add_Food(new Food(Name, Description, Path_to_Photo));
            Name = string.Empty;
            Description = string.Empty;
            Path_to_Photo = string.Empty;
        }
        private void SavePrice(object parameter)
        {
            new Attach_Service().Attach_Food_to_Restaurant(ChooseRestaurant, ChooseFood, Price);
            ChooseFood = 0;
            ChooseRestaurant = 0;
            Price = 0;
        }
        protected bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (!System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }        
        private void YourCommandExecute1(object parameter)
        {
            if (parameter is int order_id)
            {
                int[] free_couriers = new Person_Service().Get_ID_By_Role_and_Status("COURIER", "FREE");
                if (free_couriers.Length > 0)
                {
                    int id_courier = free_couriers[specialFunctions.GenerateRandomNumber(0, free_couriers.Length - 1)];
                    new Get_Something_Service().Update_Courier_By_ID(order_id, id_courier);
                    new OrderOperation().ChangeOrderStatus(new Order(order_id, 3));
                    new Person_Service().Change_Courier_Status(id_courier, "WORK");
                    View_ScrollViewer();
                }
            }
        }
        private bool YourCommandCanExecute1(object parameter) { return true; }
        private void YourCommandExecute2(object parameter)
        {
            if (parameter is int order_id)
            {
                new OrderOperation().ChangeOrderStatus(new Order(order_id, 2));
                View_ScrollViewer();
            }
        }
        private bool YourCommandCanExecute2(object parameter) { return true; }

        private string listOfRestaurant = "";
        public string ListOfRestaurant
        {
            get => listOfRestaurant;
            set => SetProperty(ref listOfRestaurant, value);
        }
        private string listOfFood = "";
        public string ListOfFood
        {
            get => listOfFood;
            set => SetProperty(ref listOfFood, value);
        }
        private int chooseRestaurant;
        public int ChooseRestaurant
        {
            get => chooseRestaurant;
            set => SetProperty(ref chooseRestaurant, value);
        }
        private int chooseFood;
        public int ChooseFood
        {
            get => chooseFood;
            set => SetProperty(ref chooseFood, value);
        }
        private int price;
        public int Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        Order[] order_for_me;

        private void View_ScrollViewer()
        {
            FirstStackPanelItems.Clear();
            IGetS getS = new Get_Something_Service();
            order_for_me = getS.Get_Order_By_Manager_and_Status(Id_Admin, 1);
            foreach (Order order in order_for_me)
            {
                DockPanel dockPanel = new DockPanel { Margin = new Thickness(0, 50, 0, 0) };
                FirstStackPanelItems.Add(dockPanel);
                StackPanel textStackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 100, 0) };
                textStackPanel.Children.Add(new TextBlock { Text = order.Id.ToString(), FontWeight = FontWeights.Bold, FontSize = 18 });
                textStackPanel.Children.Add(new TextBlock { Text = order.Adress.ToString(), FontWeight = FontWeights.Bold, FontSize = 18, Margin = new Thickness(100, 0, 0, 0) });
                textStackPanel.Children.Add(new TextBlock { Text = order.Id_Courier.ToString(), FontWeight = FontWeights.Bold, FontSize = 18, Margin = new Thickness(100, 0, 0, 0) });
                textStackPanel.Children.Add(new Button { Content = "Передати кур'єру", FontWeight = FontWeights.Bold, FontSize = 18, Margin = new Thickness(100, 0, 0, 0), Command = YourCommand1, CommandParameter = order.Id });
                textStackPanel.Children.Add(new Button { Content = "Відмовити", FontWeight = FontWeights.Bold, FontSize = 18, Margin = new Thickness(100, 0, 0, 0), Command = YourCommand2, CommandParameter = order.Id });
                DockPanel.SetDock(textStackPanel, Dock.Left);
                dockPanel.Children.Add(textStackPanel);
            }
        }                
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}