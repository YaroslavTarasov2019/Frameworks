using Frameworks.Model;
using Frameworks.Model.Services;
using Frameworks.Model.Subject;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Shapes;
using Prism.Commands;
using System.Windows.Input;
using Google.Protobuf.WellKnownTypes;

namespace Frameworks.ViewModel
{
    public class Food_ViewModel : INotifyPropertyChanged
    {
        public ICommand MyCommand { get; set; }
        public ICommand WriteComment { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand OneCommand { get; set; }
        public ICommand TwoCommand { get; set; }
        public ICommand ThreeCommand { get; set; }
        public ICommand FourCommand { get; set; }
        public ICommand FiveCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public ICommand DislikeCommand { get; set; }
        public int food_id { get; set; }
        public int person_id { get; set; }
        public int restaurant_id { get; set; }
        public string FoodName { get; set; }
        public BitmapSource FoodImage { get; set; }
        public string FoodDescription { get; set; }
        public string FoodPrice { get; set; } = "";
        public ObservableCollection<DockPanel> StackPanelItems { get; set; }
        private Visibility gridVisibility = Visibility.Collapsed;
        public Visibility GridVisibility
        {
            get => gridVisibility;
            set => SetProperty(ref gridVisibility, value);
        }
        public string Comment { get; set; } = string.Empty;
        private SolidColorBrush oneColor = new SolidColorBrush(Colors.Gold);
        public SolidColorBrush OneColor
        {
            get => oneColor;
            set => SetProperty(ref oneColor, value);
        }
        private SolidColorBrush twoColor = new SolidColorBrush(Colors.LightGray);
        public SolidColorBrush TwoColor
        {
            get => twoColor;
            set => SetProperty(ref twoColor, value);
        }
        private SolidColorBrush threeColor = new SolidColorBrush(Colors.LightGray);
        public SolidColorBrush ThreeColor
        {
            get => threeColor;
            set => SetProperty(ref threeColor, value);
        }
        private SolidColorBrush fourColor = new SolidColorBrush(Colors.LightGray);
        public SolidColorBrush FourColor
        {
            get => fourColor;
            set => SetProperty(ref fourColor, value);
        }
        private SolidColorBrush fiveColor = new SolidColorBrush(Colors.LightGray);
        public SolidColorBrush FiveColor
        {
            get => fiveColor;
            set => SetProperty(ref fiveColor, value);
        }
        public int Points { get; set; } = 1;
        public int count = 1;
        public Food_ViewModel(int id_food, int id_person)
        {
            food_id = id_food;
            person_id = id_person;
            StackPanelItems = new ObservableCollection<DockPanel>();
            LikeCommand = new RelayCommand(LikeCommandCommandExecute, LikeCommandCommandCanExecute);
            DislikeCommand = new RelayCommand(DislikeCommandExecute, DislikeCommandCanExecute);

            IGetS getS = new Get_Something_Service();
            Food food = getS.Get_Food_Info_By_ID(id_food);
            DataTable restaurant_food = getS.Get_Restaurant_Food_By_Food_ID(id_food);
            FoodName = food.Name;
            BitmapImage image = new BitmapImage(new Uri(@"C:\Users\taras\source\repos\Frameworks\Frameworks\Photo\" + food.Picture_path + ".jpeg"));
            FoodImage = image;
            FoodDescription = food.Description;

            int min_price = 99999999;
            foreach (DataRow dataRow in restaurant_food.Rows)
            {
                Restaurant restaurant = getS.Get_Restaurant_Info_By_ID(Convert.ToInt32(dataRow[0].ToString()));
                if (Convert.ToInt32(dataRow[2].ToString()) < min_price)
                {
                    FoodPrice = restaurant.Name + ":    " + dataRow[2].ToString() + " грн";
                    min_price = Convert.ToInt32(dataRow[2].ToString());
                    restaurant_id = Convert.ToInt32(dataRow[0].ToString());
                }
            }
            MyCommand = new RelayCommand(ExecuteMyCommand, CanExecuteMyCommand);
            WriteComment = new DelegateCommand(WriteCommentExecute);
            View_ScrollViewer();

            SaveCommand = new DelegateCommand(SaveCommandExecute);
            OneCommand = new DelegateCommand(OneCommandExecute);
            TwoCommand = new DelegateCommand(TwoCommandExecute);
            ThreeCommand = new DelegateCommand(ThreeCommandExecute);
            FourCommand = new DelegateCommand(FourCommandExecute);
            FiveCommand = new DelegateCommand(FiveCommandExecute);
        }
        

        private void WriteCommentExecute()
        {
            if (GridVisibility == Visibility.Collapsed) GridVisibility = Visibility.Visible;
            else { GridVisibility = Visibility.Collapsed; }
        }       
        private void SaveCommandExecute()
        {
            new Comments_Service().Add_Comment(food_id, person_id, Comment, Points);
            Comment = "";
            GridVisibility = Visibility.Collapsed;
            View_ScrollViewer();
        }
        private void Set_Color_and_Points(int point, Color color1, Color color2, Color color3, Color color4, Color color5)
        {
            Points = point;
            OneColor = new SolidColorBrush(color1);
            TwoColor = new SolidColorBrush(color2);
            ThreeColor = new SolidColorBrush(color3);
            FourColor = new SolidColorBrush(color4);
            FiveColor = new SolidColorBrush(color5);
        }
        private void OneCommandExecute()
        {
            Set_Color_and_Points(1, Colors.Gold, Colors.LightGray, Colors.LightGray, Colors.LightGray, Colors.LightGray);
        }
        private void TwoCommandExecute()
        {
            Set_Color_and_Points(2, Colors.LightGray, Colors.Gold, Colors.LightGray, Colors.LightGray, Colors.LightGray);
        }
        private void ThreeCommandExecute()
        {
            Set_Color_and_Points(3, Colors.LightGray, Colors.LightGray, Colors.Gold, Colors.LightGray, Colors.LightGray);
        }
        private void FourCommandExecute()
        {
            Set_Color_and_Points(4, Colors.LightGray, Colors.LightGray, Colors.LightGray, Colors.Gold, Colors.LightGray);
        }
        private void FiveCommandExecute()
        {
            Set_Color_and_Points(5, Colors.LightGray, Colors.LightGray, Colors.LightGray, Colors.LightGray, Colors.Gold);
        }        
        private void ExecuteMyCommand(object parameter)
        {
            if (Count < 1)
            {
                MessageBox.Show("Кількість повинна бути більше нуля");
                return;
            }
            new Add_Service().Add_Basket(new Basket(food_id.ToString(), person_id.ToString(), Count.ToString(), restaurant_id.ToString()));
            if (parameter is Window window)
            {
                window.Close();
            }
        }
        private bool CanExecuteMyCommand(object parameter)  {   return true;    }
        private void LikeCommandCommandExecute(object parameter)
        {
            if (parameter is int comment_id)
            {
                new Comments_Service().Give_Like(comment_id);
                View_ScrollViewer();
            }
        }
        private bool LikeCommandCommandCanExecute(object parameter)     {   return true;    }
        private void DislikeCommandExecute(object parameter)
        {
            if (parameter is int comment_id)
            {
                new Comments_Service().Give_Dislike(comment_id);
                View_ScrollViewer();
            }
        }
        private bool DislikeCommandCanExecute(object parameter)     {    return true;    }
        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
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
        private void View_ScrollViewer()
        {
            StackPanelItems.Clear();
            DataTable dt = new Comments_Service().Show_Comments(food_id);
            foreach (DataRow row in dt.Rows)
            {
                Grid mainGrid = new Grid();
                Border commentBorder = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1), Margin = new Thickness(10), Padding = new Thickness(5), Width = 500, CornerRadius = new CornerRadius(5) };

                StackPanel stackPanel = new StackPanel();

                Person p = new Person_Service().Get_Person_Data_By_ID(Convert.ToInt32(row[2].ToString()));

                TextBlock authorBlock = new TextBlock { Text = p.sur_name + " " + p.first_name, Foreground = Brushes.Gray };
                stackPanel.Children.Add(authorBlock);

                TextBlock dateBlock = new TextBlock { Text = row[4].ToString(), Foreground = Brushes.Gray };
                stackPanel.Children.Add(dateBlock);

                double circleRadius = 10;
                var grid = new Grid();
                stackPanel.Children.Add(grid);
                for (int i = 0; i < Convert.ToInt32(row[5].ToString()); i++)
                {
                    var circle = new Ellipse { Width = circleRadius * 2, Height = circleRadius * 2, Fill = Brushes.Gold };
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(circle, i);
                    grid.Children.Add(circle);
                }

                var textBlock = new TextBlock
                {
                    Text = row[3].ToString(),
                    FontWeight = FontWeights.Bold
                };
                stackPanel.Children.Add(textBlock);

                var ratingPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                var likesBlock = new TextBlock
                {
                    Text = "Лайки: "
                };
                ratingPanel.Children.Add(likesBlock);

                var likesCountBlock = new TextBlock
                {
                    Text = row[6].ToString(),
                    Foreground = Brushes.Green
                };
                ratingPanel.Children.Add(likesCountBlock);

                var separatorBlock1 = new TextBlock
                {
                    Text = " | "
                };
                ratingPanel.Children.Add(separatorBlock1);

                var dislikesBlock = new TextBlock
                {
                    Text = "Дизлайки: "
                };
                ratingPanel.Children.Add(dislikesBlock);

                var dislikesCountBlock = new TextBlock
                {
                    Text = row[7].ToString(),
                    Foreground = Brushes.Red
                };
                ratingPanel.Children.Add(dislikesCountBlock);

                stackPanel.Children.Add(ratingPanel);

                var buttonPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                buttonPanel.Children.Add(new Button { Content = "Like", Width = 50, Height = 25, Margin = new Thickness(0, 0, 10, 0), Command = LikeCommand, CommandParameter = Convert.ToInt32(row[0].ToString()) });
                buttonPanel.Children.Add(new Button { Content = "Dislike", Width = 50, Height = 25, Command = DislikeCommand, CommandParameter = Convert.ToInt32(row[0].ToString()) });
                stackPanel.Children.Add(buttonPanel);
                commentBorder.Child = stackPanel;
                mainGrid.Children.Add(commentBorder);
                DockPanel dockPanel = new DockPanel();
                dockPanel.Children.Add(mainGrid);
                StackPanelItems.Add(dockPanel);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
