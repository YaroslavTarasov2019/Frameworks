using Frameworks.Model;
using Frameworks.Model.Services;
using Frameworks.Model.Subject;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace Frameworks.ViewModel
{
    public class Courier_ViewModel: INotifyPropertyChanged
    {
        Order order;
        public Visibility ButtonVisibility { get; set; }
        public Courier_ViewModel(int id_courier) 
        {
            StackPanelItems = new ObservableCollection<DockPanel>();
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            ID_Courier = id_courier;
            IGetS getS = new Get_Something_Service();
            order = getS.Get_Order_By_ID_Courier(ID_Courier);
            Status = order.Id_Status;
            Adress = order.Adress;
            IPerson person = new Person_Service();
            Person client = person.Get_Person_Data_By_ID(order.Id_Client);
            FullName = client.first_name + " " + client.second_name + " " + client.sur_name;
            PhoneNumber = client.phone_number;
            id_order = order.Id;
            if (Status < 5) ButtonVisibility = Visibility.Visible;
            else ButtonVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(ButtonVisibility));

            DataTable dataT = getS.Get_OrderFood_By_IdOrder(order.Id);
            int sum = 0;
            foreach(DataRow dataRow in dataT.Rows)
            {
                sum += (Convert.ToInt32(dataRow[3].ToString()) * Convert.ToInt32(dataRow[4]));
            }
            Amount = sum;
            View_ScrollViewer();
        }
        private void ChangeStatus(object parameter)
        {
            if (order.Id_Status >= 3 && order.Id_Status < 5)
            {
                order.Id_Status += 1;
                new OrderOperation().ChangeOrderStatus(new Order(order.Id, order.Id_Status));
                View_ScrollViewer();
                Status = order.Id_Status;

                if (Status < 5) ButtonVisibility = Visibility.Visible;
                else ButtonVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }
        public ICommand ChangeStatusCommand { get; private set; }

        private int id_order;

        private int id_courier;
        public int ID_Courier
        {
            get => id_courier;
            set => SetProperty(ref id_courier, value);
        }
        private int status;
        public int Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
        private string adress;
        public string Adress
        {
            get => adress;
            set => SetProperty(ref adress, value);
        }
        private int amount;
        public int Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
        private string full_name;
        public string FullName
        {
            get => full_name;
            set => SetProperty(ref full_name, value);
        }
        private string phone_number;
        public string PhoneNumber
        {
            get => phone_number;
            set => SetProperty(ref phone_number, value);
        }
        private ObservableCollection<DockPanel> stackPanelItems;
        public ObservableCollection<DockPanel> StackPanelItems
        {
            get => stackPanelItems;
            set => SetProperty(ref stackPanelItems, value);
        }
        private void View_ScrollViewer()
        {
            StackPanelItems.Clear();
            IGetS getS = new Get_Something_Service();
            DataTable food_order = getS.Get_OrderFood_By_IdOrder(id_order);
            int i = 1;
            foreach (DataRow row in food_order.Rows)
            {
                DockPanel dockPanel = new DockPanel { Margin = new Thickness(0, 50, 0, 0) };
                StackPanelItems.Add(dockPanel);
                dockPanel.Children.Add(new TextBlock { Text = i.ToString() + ") Страва: " + getS.Get_Food_Info_By_ID(Convert.ToInt32(row[2].ToString())).Name, FontWeight = FontWeights.Bold, FontSize = 20 });
                dockPanel.Children.Add(new TextBlock { Text = "Ціна за штуку: " + row[3].ToString(), FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(100, 0, 0, 0) });
                dockPanel.Children.Add(new TextBlock { Text = "Кількість: " + row[4].ToString(), FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(200, 0, 0, 0) });
                dockPanel.Children.Add(new TextBlock { Text = "Заклад харчування: " + getS.Get_Restaurant_Info_By_ID(Convert.ToInt32(row[5].ToString())).Name, FontWeight = FontWeights.Bold, FontSize = 20, Margin = new Thickness(250, 0, 0, 0) });
                i++;
            }
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
