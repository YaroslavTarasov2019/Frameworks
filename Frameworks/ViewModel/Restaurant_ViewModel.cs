using Frameworks.Model.Services;
using Frameworks.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Media.Imaging;
using Frameworks.Model.Subject;

namespace Frameworks.ViewModel
{
    public class Restaurant_ViewModel: INotifyPropertyChanged
    {
        public Restaurant restaurant;
        public string RestaurantName { get; set; }
        public BitmapSource RestaurantImage { get; set; }
        public string RestaurantDescription { get; set; }
        public string FoodInRestaurantList { get; set; } = string.Empty;
        public Restaurant_ViewModel(int id_restaurant) 
        {
            IGetS getS = new Get_Something_Service();
            restaurant = getS.Get_Restaurant_Info_By_ID(id_restaurant);
            RestaurantName = restaurant.Name;
            BitmapImage image = new BitmapImage(new Uri(@"C:\Users\taras\source\repos\Frameworks\Frameworks\Photo\" + restaurant.Picture_Path + ".jpeg"));
            RestaurantImage = image;
            RestaurantDescription = restaurant.Description;
            DataTable dt = getS.Get_Restaurant_Food_By_Restaurant_ID(id_restaurant);
            foreach (DataRow dr in dt.Rows)     FoodInRestaurantList += getS.Get_Food_Info_By_ID(Convert.ToInt32(dr[1].ToString())).Name + ":    " + dr[2].ToString() + "\n";            
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}