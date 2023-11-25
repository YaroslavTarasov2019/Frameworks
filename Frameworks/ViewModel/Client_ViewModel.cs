using System;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Frameworks.Model;
using Frameworks.Model.Services;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Frameworks.Model.Subject;
using Frameworks.View;
using System.Linq;
using System.Windows.Input;
using System.Windows.Data;

namespace Frameworks.ViewModel
{
    public class Client_ViewModel : INotifyPropertyChanged
    {
        SpecialFunctionsForViewModel specialFunctions = new SpecialFunctionsForViewModel();
        const int amount_on_page = 9;
        public Person old_person_inf { get; set; }
        public int ID_Person;
        public string objects = "";
        public int[] manager_id;
        public Visibility FirstElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SecondElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility ThirdElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility FourthElementsVisibility { get; set; } = Visibility.Collapsed;
        public Visibility FifthElementsVisibility { get; set; } = Visibility.Collapsed;


        public Visibility doneDeliveryVisibility = Visibility.Visible;
        public Visibility DoneDeliveryVisibility
        {
            get => doneDeliveryVisibility;
            set => SetProperty(ref doneDeliveryVisibility, value);
        }
        public ICommand ShowFirstElementsCommand { get; set; }
        public ICommand ShowSecondElementsCommand { get; set; }
        public ICommand ShowThirdElementsCommand { get; set; }
        public ICommand ShowFourthElementsCommand { get; set; }
        public ICommand ShowFifthElementsCommand { get; set; }
        public ICommand PrevOrderCommand { get; set; }
        public ICommand NextOrderCommand { get; set; }
        public ICommand DoneDelivery { get; set; }
        public ICommand ToOrder { get; set; }
        public ICommand YourCommand { get; set; }
        public ICommand YourCommand1 { get; set; }
        public ICommand Command1 { get; set; }
        public ICommand Command2 { get; set; }
        public ICommand Command4 { get; set; }
        public ICommand Command5 { get; set; }

        private ObservableCollection<string> items = new ObservableCollection<string>();
        public ObservableCollection<string> Items { get { return items; } }

        private bool IsFilter;

        public string orderStatus = "";
        public string OrderStatus
        {
            get => orderStatus;
            set => SetProperty(ref orderStatus, value);
        }

        public Client_ViewModel(int id_person)
        {
            ID_Person = id_person;
            InitializeCommands();
            InitializeCollections();

            manager_id = new Person_Service().Get_ID_By_Role("ADMIN");
            IGetS getS = new Get_Something_Service();
            DataTable dt = getS.Get_All_Features();
            foreach (DataRow row in dt.Rows)
            {
                items.Add(row[1].ToString());
            }
            DataTable d = getS.Get_Order_By_ID_Person(ID_Person);
            if (d.Rows.Count > 0)
            {
                DataRow dr = d.Rows[page_order - 1];
                if (dr[3].ToString() == "6" || Convert.ToInt32(dr[3].ToString()) < 3) DoneDeliveryVisibility = Visibility.Collapsed;
                OrderStatus = new Get_Something_Service().Get_OrderStatus_By_Id(Convert.ToInt32(dr[3].ToString())).Rows[0][1].ToString();
            }
        }

        private void InitializeCommands()
        {
            ShowFirstElementsCommand = new RelayCommand(ShowFirstElements);
            ShowSecondElementsCommand = new RelayCommand(ShowSecondElements);
            ShowThirdElementsCommand = new RelayCommand(ShowThirdElements);
            ShowFourthElementsCommand = new RelayCommand(ShowFourthElements);
            ShowFifthElementsCommand = new RelayCommand(ShowFifthElements);

            PrevOrderCommand = new RelayCommand(ShowPrevOrder);
            NextOrderCommand = new RelayCommand(ShowNextOrder);

            DoneDelivery = new RelayCommand(DoneDeliveryExecute);

            Command1 = new DelegateCommand(ExecuteCommand1);
            Command2 = new DelegateCommand(ExecuteCommand2);
            Command4 = new DelegateCommand(ExecuteCommand4);
            Command5 = new DelegateCommand(ExecuteCommand5);

            ToOrder = new DelegateCommand(ToOrderCommand);

            YourCommand = new RelayCommand(YourCommandExecute, YourCommandCanExecute);
            YourCommand1 = new RelayCommand(YourCommandExecute1, YourCommandCanExecute1);
        }

        private void InitializeCollections()
        {
            FirstStackPanelItems = new ObservableCollection<DockPanel>();
            SecondStackPanelItems = new ObservableCollection<DockPanel>();
            ThirdStackPanelItems = new ObservableCollection<DockPanel>();
            FourthStackPanelItems = new ObservableCollection<DockPanel>();
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

        private void ShowElements(Visibility firstVisibility, Visibility secondVisibility, Visibility thirdVisibility, Visibility fourthVisibility, Visibility fifthVisibility)
        {
            FirstElementsVisibility = firstVisibility;
            SecondElementsVisibility = secondVisibility;
            ThirdElementsVisibility = thirdVisibility;
            FourthElementsVisibility = fourthVisibility;
            FifthElementsVisibility = fifthVisibility;
            OnPropertyChanged(nameof(FirstElementsVisibility));
            OnPropertyChanged(nameof(SecondElementsVisibility));
            OnPropertyChanged(nameof(ThirdElementsVisibility));
            OnPropertyChanged(nameof(FourthElementsVisibility));
            OnPropertyChanged(nameof(FifthElementsVisibility));
            CurrentPage = 1;
            first_name = second_name = sur_name = phone_number = null;
        }
        private void ShowFirstElements(object parameter)
        {
            objects = "Food";
            SelectedValue = "";
            MyRule = "";
            ShowElements(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);
            View_ScrollViewer(CurrentPage);
        }
        private void ShowSecondElements(object parameter)
        {
            objects = "Restaurant";
            ShowElements(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);
            View_ScrollViewer(CurrentPage);
        }
        private void ShowThirdElements(object parameter)
        {
            objects = "Basket";
            ShowElements(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);
            View2_ScrollViewer();
        }
        private void ShowFourthElements(object parameter)
        {
            objects = "Order";
            ShowElements(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
            View3_ScrollViewer();
        }
        private void ShowFifthElements(object parameter)
        {
            ShowElements(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);
            Information_About_Person();
        }
        private void DoneDeliveryExecute(object parameter)
        {
            DataTable dt = new Get_Something_Service().Get_Order_By_ID_Person(ID_Person);
            DataRow dar = dt.Rows[page_order - 1];
            new OrderOperation().ChangeOrderStatus(new Order(Convert.ToInt32(dar[0].ToString()), 6));
            DoneDeliveryVisibility = Visibility.Collapsed;

            DataTable d = new Get_Something_Service().Get_Order_By_ID_Person(ID_Person);

            DataRow dr = d.Rows[page_order - 1];
            if (dr[3].ToString() == "6" || Convert.ToInt32(dr[3].ToString()) < 3) DoneDeliveryVisibility = Visibility.Collapsed;
            OrderStatus = new Get_Something_Service().Get_OrderStatus_By_Id(Convert.ToInt32(dr[3].ToString())).Rows[0][1].ToString();

            int to_be_free_courier = Convert.ToInt32(d.Rows[page_order - 1][2].ToString());

            new Person_Service().Change_Courier_Status(to_be_free_courier, "FREE");

        }
        private void ToOrderCommand()
        {
            if (AdressOrder != "")
            {
                new OrderOperation().CreateOrder(ID_Person, AdressOrder, manager_id[specialFunctions.GenerateRandomNumber(0, manager_id.Length - 1)]);
                ThirdStackPanelItems.Clear();
                AdressOrder = "";
            }
        }
        private void ShowPrevOrder(object parameter)
        {
            if (page_order > 1)
            {
                page_order--;
                DataTable dt = new Get_Something_Service().Get_Order_By_ID_Person(ID_Person);
                DataRow dr = dt.Rows[page_order - 1];
                if (dr[3].ToString() == "6" || Convert.ToInt32(dr[3].ToString()) < 3) DoneDeliveryVisibility = Visibility.Collapsed;
                else DoneDeliveryVisibility = Visibility.Visible;
                OrderStatus = new Get_Something_Service().Get_OrderStatus_By_Id(Convert.ToInt32(dr[3].ToString())).Rows[0][1].ToString();
                View3_ScrollViewer();
            }
        }
        private void ShowNextOrder(object parameter)
        {
            if (page_order < page_count) 
            {
                page_order++;
                DataTable dt = new Get_Something_Service().Get_Order_By_ID_Person(ID_Person);
                DataRow dr = dt.Rows[page_order - 1];
                if (dr[3].ToString() == "6" || Convert.ToInt32(dr[3].ToString()) < 3) DoneDeliveryVisibility = Visibility.Collapsed;
                else DoneDeliveryVisibility = Visibility.Visible;
                OrderStatus = new Get_Something_Service().Get_OrderStatus_By_Id(Convert.ToInt32(dr[3].ToString())).Rows[0][1].ToString();
                View3_ScrollViewer();
            }
        }
        private int page_order { get; set; } = 1;
        private int page_count { get; set; } = 0;
        private string role = "";
        public string Role
        { 
            get => role; 
            set => SetProperty(ref role, value); 
        }
        public string AdressOrder { get; set; } = string.Empty;
        private string? first_name;
        public string? FirstName
        {
            get { return first_name; }
            set
            {
                if (first_name == null)     SetProperty(ref first_name, value);
                else
                {
                    SetProperty(ref first_name, value);
                    IsChange();
                }
            }
        }
        private string? second_name;
        public string? SecondName
        {
            get { return second_name; }
            set
            {
                if (second_name == null)        SetProperty(ref second_name, value);               
                else
                {
                    SetProperty(ref second_name, value);                    
                    IsChange();
                }
            }
        }
        private string? sur_name;
        public string? SurName
        {
            get { return sur_name; }
            set
            {
                if (sur_name == null)       SetProperty(ref sur_name, value);
                else
                {
                    SetProperty(ref sur_name, value);
                    IsChange();
                }
            }
        }
        private string? phone_number;
        public string? PhoneNumber
        {
            get { return phone_number; }
            set
            {
                if (phone_number == null)       SetProperty(ref phone_number, value);
                else
                {
                    SetProperty(ref phone_number, value);
                    IsChange();
                }
            }
        }
        private void IsChange()
        {
            if (old_person_inf.first_name != FirstName || old_person_inf.second_name != SecondName || old_person_inf.sur_name != SurName || old_person_inf.phone_number != PhoneNumber)
            {
                new Person_Service().Change_Person_Info(new Person(ID_Person, FirstName, SecondName, SurName, old_person_inf.photo_path, PhoneNumber));
                MessageBox.Show("Data Change");
                old_person_inf.first_name = FirstName;
                old_person_inf.second_name = SecondName;
                old_person_inf.sur_name = SurName;
                old_person_inf.phone_number = PhoneNumber;
            }
        }
        private void Information_About_Person()
        {
            IPerson iperson = new Person_Service();
            MyImageSource = new BitmapImage(new Uri(specialFunctions.Find_Image_str(iperson.Get_Photo_Path_By_ID(ID_Person))));
            old_person_inf = iperson.Get_Person_Data_By_ID(ID_Person);
            Role = iperson.Get_Role_By_ID(ID_Person);
            FirstName = old_person_inf.first_name;
            SecondName = old_person_inf.second_name;
            SurName = old_person_inf.sur_name;
            PhoneNumber = old_person_inf.phone_number;
        }
        private BitmapImage myImageSource;
        public BitmapImage MyImageSource
        {
            get => myImageSource;
            set => SetProperty(ref myImageSource, value);
        }

        public ObservableCollection<DockPanel> FirstStackPanelItems { get; set; }
        public ObservableCollection<DockPanel> SecondStackPanelItems { get; set; }
        public ObservableCollection<DockPanel> ThirdStackPanelItems { get; set; }
        public ObservableCollection<DockPanel> FourthStackPanelItems { get; set; }

        private string myrule = "";
        public string MyRule
        {
            get => myrule;
            set
            {
                SetProperty(ref myrule, value);
                FirstStackPanelItems.Clear();
                SecondStackPanelItems.Clear();
                CurrentPage = 1;
                if (myrule != "") IsFilter = false;
                else IsFilter = true;
                View_ScrollViewer(1);
            }
        }
        private int currentPage = 1;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }
       
        private void ExecuteCommand1()
        {
            CurrentPage = 1;
            View_ScrollViewer(CurrentPage);
        }
        private void ExecuteCommand2()
        {
            CurrentPage --;
            if (CurrentPage > 0)    View_ScrollViewer(CurrentPage);
            else
            {
                CurrentPage++;
            }
        }
        private void ExecuteCommand4()
        {
            if (CurrentPage + 1 <= (int)Math.Ceiling(Convert.ToDecimal(specialFunctions.RuleOrNo(objects, MyRule, IsFilter, SelectedValue, ID_Person).Rows.Count) / amount_on_page))
            {
                CurrentPage += 1;
                View_ScrollViewer(CurrentPage);
            }
        }
        private string selectedValue = "";
        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                if (selectedValue != value)
                {
                    selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                    IsFilter = true;
                    View_ScrollViewer(1);
                }
            }
        }
        
        private void ExecuteCommand5()
        {
            CurrentPage = (int)Math.Ceiling(Convert.ToDecimal(specialFunctions.RuleOrNo(objects, MyRule, IsFilter, SelectedValue, ID_Person).Rows.Count) / amount_on_page);
            View_ScrollViewer(CurrentPage);
        }

        private void YourCommandExecute(object parameter)
        {
            if (parameter is string str)
            {
                if (objects == "Food")  new Food_Window(Convert.ToInt32(str), ID_Person).Show();
                if (objects == "Restaurant")    new Restaurant_Window(Convert.ToInt32(str)).Show();
            }
        }
        private bool YourCommandCanExecute(object parameter) { return true; }
        private void YourCommandExecute1(object parameter)
        {
            if (parameter is string str)
            {
                new Get_Something_Service().Delete_Basket_By_ID(Convert.ToInt32(str));
                View2_ScrollViewer();
            }
        }
        private bool YourCommandCanExecute1(object parameter) { return true; }
        
        private Button Image_Button_settings(DataRow row, Thickness thickness)
        {
            return new Button { Tag = row, Height = 279, Width = 496, HorizontalAlignment = HorizontalAlignment.Left, 
                Content = new Image { Source = new BitmapImage(new Uri(specialFunctions.Find_Image_str(row[3].ToString()), UriKind.Absolute)) }, Margin = thickness,
                Command = YourCommand, CommandParameter = row[0].ToString()
            };
        }
        private Button Button_settings(DataRow row, Thickness thickness)
        {
            return new Button { Tag = row, Content = row[1].ToString(), Height = 30, Width = 496, Background = System.Windows.Media.Brushes.White, 
                HorizontalAlignment = HorizontalAlignment.Left, BorderThickness = new Thickness(0, 0, 0, 0), FontSize = 18, Margin = thickness,
                Command = YourCommand, CommandParameter = row[0].ToString()
            };
        }

        private void View_ScrollViewer(int pageNumber)
        {
            int from = (pageNumber - 1) * amount_on_page + 1;
            int to = pageNumber * amount_on_page;
            FirstStackPanelItems.Clear();
            SecondStackPanelItems.Clear();
            DataTable dt = specialFunctions.RuleOrNo(objects, MyRule, IsFilter, SelectedValue, ID_Person);
            DockPanel dockPanel = new DockPanel();
            DockPanel dockPanel2 = new DockPanel();
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                if (i < from)       continue;
                if (i > to)         break;

                DataRow row = dt.Rows[i - 1];
                int columnRemainder = (i - 1) % 3;

                if (columnRemainder == 0)
                {
                    dockPanel = new DockPanel();
                    dockPanel.Children.Add(Image_Button_settings(row, new Thickness(30, 10, 0, 0)));
                    if (objects == "Food") FirstStackPanelItems.Add(dockPanel);
                    else if (objects == "Restaurant") SecondStackPanelItems.Add(dockPanel);

                    dockPanel2 = new DockPanel();
                    dockPanel2.Children.Add(Button_settings(row, new Thickness(30, 0, 0, 50)));
                    if (objects == "Food") FirstStackPanelItems.Add(dockPanel2);
                    else if (objects == "Restaurant") SecondStackPanelItems.Add(dockPanel2);
                }
                else if (columnRemainder == 1)
                {
                    dockPanel.Children.Add(Image_Button_settings(row, new Thickness(30, 10, 30, 0)));
                    dockPanel2.Children.Add(Button_settings(row, new Thickness(30, 0, 30, 50)));
                }
                else if (columnRemainder == 2)
                {
                    dockPanel.Children.Add(Image_Button_settings(row, new Thickness(0, 10, 30, 0)));
                    dockPanel2.Children.Add(Button_settings(row, new Thickness(0, 0, 30, 50)));
                }
            }           
        }

        private string[] myArray;
        private string[] old_myArray;

        DataTable rest_food;

        private bool MyArrayNull()
        {
            foreach (var item in myArray)   if (item == null) return false;
            return true;
        }
        public string[] MyArray
        {
            get 
            {
                int new_data = -1;
                if (!myArray.SequenceEqual(old_myArray) && MyArrayNull())
                {
                    int differingIndex = -1;

                    for (int i = 0; i < Math.Min(myArray.Length, old_myArray.Length); i++)
                    {
                        int number;
                        if (int.TryParse(myArray[i], out number))
                        {
                            if (myArray[i] != old_myArray[i] && myArray[i] != "")
                            {
                                differingIndex = i;
                                new_data = Convert.ToInt32(myArray[i]);
                                break;
                            }
                        }
                        else if (myArray[i] == "")
                        {
                            myArray[i] = "0";
                        }
                        else
                        {
                            myArray[i] = old_myArray[i];
                        }
                    }
                    if (differingIndex != -1)
                    {
                        new Get_Something_Service().Update_Basket_By_ID(Convert.ToInt32(rest_food.Rows[differingIndex][0].ToString()), new_data);
                        View2_ScrollViewer();
                        MessageBox.Show("Змінено!");
                    }
                }
                return myArray;
            }            
        }
        protected void First_Collumn(DockPanel dockPanel, Food food_in_basket)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\taras\source\repos\Frameworks\Frameworks\Photo\" + food_in_basket.Picture_path + ".jpeg"));
            image.Height = 279;
            image.Width = 496;
            image.Margin = new Thickness(20, 0, 50, 50);
            DockPanel.SetDock(image, Dock.Left);
            dockPanel.Children.Add(image);
        }
        protected void Second_Collumn(DockPanel dockPanel, Food food_in_basket, Restaurant restaurant_in_basket, string basket_id, ICommand YourCommand1)
        {
            StackPanel textStackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 100, 0)};
            textStackPanel.Children.Add(new TextBlock { Text = food_in_basket.Name, FontWeight = FontWeights.Bold, FontSize = 18 });
            textStackPanel.Children.Add(new TextBlock { Text = restaurant_in_basket.Name, FontWeight = FontWeights.Bold, FontSize = 18, Margin = new Thickness(0, 50, 0, 0) });
            if (YourCommand1 != null)   textStackPanel.Children.Add(new Button { Content = "Удалить", FontSize = 18, Height = 50, Margin = new Thickness(0, 100, 0, 0), Command = YourCommand1, CommandParameter = basket_id });            
            DockPanel.SetDock(textStackPanel, Dock.Left);
            dockPanel.Children.Add(textStackPanel);
        }
        protected void Third_Collumn(DockPanel dockPanel, int food_price)
        {
            StackPanel priceStackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 100, 0) };
            TextBlock price = new TextBlock { Text = "Цена", FontSize = 18 };
            DockPanel.SetDock(price, Dock.Left);
            priceStackPanel.Children.Add(price);
            TextBlock price_a = new TextBlock { Text = food_price.ToString(), FontSize = 18, Margin = new Thickness(0, 50, 0, 0)};
            DockPanel.SetDock(price_a, Dock.Left);
            priceStackPanel.Children.Add(price_a);
            dockPanel.Children.Add(priceStackPanel);
        }
        protected void Fourth_Collumn(DockPanel dockPanel, int amount, int i, string[] MyArray, string[] old_myArray)
        {
            StackPanel quantityStackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 100, 0) };
            TextBlock quantity = new TextBlock { Text = "Количество", FontSize = 18, Margin = new Thickness(0, 0, 100, 0)};
            DockPanel.SetDock(quantity, Dock.Left);
            quantityStackPanel.Children.Add(quantity);
            if (MyArray != null && old_myArray != null)
            {
                TextBox textBox = new TextBox { Margin = new Thickness(0, 50, 0, 0), FontSize = 18, Text = old_myArray[i] = MyArray[i] = amount.ToString() };
                textBox.SetBinding(TextBox.TextProperty, new Binding($"MyArray[{i}]") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                DockPanel.SetDock(textBox, Dock.Left);
                quantityStackPanel.Children.Add(textBox);
            }
            else
            {
                Label label_a = new Label { Margin = new Thickness(0, 50, 0, 0), Content = amount.ToString(), FontSize = 18};
                DockPanel.SetDock(label_a, Dock.Left);
                quantityStackPanel.Children.Add(label_a);
            }
            dockPanel.Children.Add(quantityStackPanel);
        }
        protected void Fifth_Collumn(DockPanel dockPanel, int sum)
        {
            StackPanel sumStackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 100, 0) };
            TextBlock total = new TextBlock { Text = "Сумма", FontSize = 18 };
            DockPanel.SetDock(total, Dock.Left);
            sumStackPanel.Children.Add(total);
            TextBlock total_s = new TextBlock { Text = sum.ToString(), Margin = new Thickness(0, 50, 0, 0), FontSize = 18};
            DockPanel.SetDock(total_s, Dock.Left);
            sumStackPanel.Children.Add(total_s);
            dockPanel.Children.Add(sumStackPanel);
        }
        private void View2_ScrollViewer()
        {
            FirstStackPanelItems.Clear();
            SecondStackPanelItems.Clear();
            ThirdStackPanelItems.Clear();
            FourthStackPanelItems.Clear();
            rest_food = specialFunctions.RuleOrNo(objects, MyRule, IsFilter, SelectedValue, ID_Person);
            int i = 0;
            myArray = new string[rest_food.Rows.Count];
            old_myArray = new string[rest_food.Rows.Count];
            foreach (DataRow row in rest_food.Rows)
            {
                DockPanel dockPanel = new DockPanel { Margin = new Thickness(0, 50, 0, 0) };
                ThirdStackPanelItems.Add(dockPanel);

                IGetS getS = new Get_Something_Service();
                Food food_in_basket = getS.Get_Food_Info_By_ID(Convert.ToInt32(row[1].ToString()));
                Restaurant restaurant_in_basket = getS.Get_Restaurant_Info_By_ID(Convert.ToInt32(row[4].ToString()));
                int food_price = getS.Get_Price_By_Food_and_Restaurant(Convert.ToInt32(row[1].ToString()), Convert.ToInt32(row[4].ToString()));
                int food_amount = Convert.ToInt32(row[3].ToString());
                
                First_Collumn(dockPanel, food_in_basket);
                Second_Collumn(dockPanel, food_in_basket, restaurant_in_basket, row[0].ToString(), YourCommand1);
                Third_Collumn(dockPanel, food_price);                
                Fourth_Collumn(dockPanel, food_amount, i, MyArray, old_myArray);
                Fifth_Collumn(dockPanel, food_price * food_amount);

                i++;
            }
        }
        private void View3_ScrollViewer()
        {
            FirstStackPanelItems.Clear();
            SecondStackPanelItems.Clear();
            ThirdStackPanelItems.Clear();
            FourthStackPanelItems.Clear();
            rest_food = specialFunctions.RuleOrNo(objects, MyRule, IsFilter, SelectedValue, ID_Person);
            page_count = rest_food.Rows.Count;
            IGetS getS = new Get_Something_Service();
            if (rest_food.Rows.Count > 0)
            {
                DataTable dataTable = getS.Get_Order_Food_By_ID_Order(Convert.ToInt32(rest_food.Rows[page_order - 1][0].ToString()));
                foreach (DataRow row in dataTable.Rows)
                {
                    DockPanel dockPanel = new DockPanel { Margin = new Thickness(0, 50, 0, 0)};
                    FourthStackPanelItems.Add(dockPanel);
                    Food food_in_basket = getS.Get_Food_Info_By_ID(Convert.ToInt32(row[2].ToString()));
                    Restaurant restaurant_in_basket = getS.Get_Restaurant_Info_By_ID(Convert.ToInt32(row[5].ToString()));
                    int food_price = getS.Get_Price_By_Food_and_Restaurant(Convert.ToInt32(row[2].ToString()), Convert.ToInt32(row[5].ToString()));
                    int food_amount = Convert.ToInt32(row[4].ToString());

                    First_Collumn(dockPanel, food_in_basket);
                    Second_Collumn(dockPanel, food_in_basket, restaurant_in_basket, row[0].ToString(), null);
                    Third_Collumn(dockPanel, food_price);
                    Fourth_Collumn(dockPanel, food_amount, 0, null, null);
                    Fifth_Collumn(dockPanel, food_price * food_amount);
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}