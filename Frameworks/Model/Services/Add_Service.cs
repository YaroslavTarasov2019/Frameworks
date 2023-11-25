using Frameworks.Model.Subject;
using System.Data;

namespace Frameworks.Model.Services
{
    public class Add_Service : IAdd
    {
        IOperations Ioperations = new Operations();

        public int Add_Order(Order order)
        {
            string[] arr = { "adress", "id_courier", "id_status", "id_client", "id_manager" };
            string[] arr2 = { order.Adress.ToString(), order.Id_Courier.ToString(), order.Id_Status.ToString(), order.Id_Client.ToString(), order.Id_Manager.ToString() };
            Ioperations.Insert_To_Table(Constants.ORDER_TABLENAME, arr, arr2);
            return 0;
        }
        public int Add_User(Person person)
        {
            string[] arr5 = { "phone_number" };
            string[] arr6 = { person.phone_number };
            DataTable? accounts = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr5, arr6);
            if (accounts.Rows.Count > 0) return Constants.ERROR_USERISALREADYEXIST;
            string[] arr = { "firstname", "secondname", "surname", "photo_path", "phone_number", "role", "status" };
            string[] arr2 = new string[7];
            if (person is Client client)
            {
                arr2[0] = client.first_name.ToString();
                arr2[1] = client.second_name.ToString();
                arr2[2] = client.sur_name.ToString();
                arr2[3] = client.photo_path.ToString();
                arr2[4] = client.phone_number.ToString();
                arr2[5] = client.role.ToString();
                arr2[6] = client.status.ToString();
            }
            else if (person is Courier courier)
            {
                arr2[0] = courier.first_name.ToString();
                arr2[1] = courier.second_name.ToString();
                arr2[2] = courier.sur_name.ToString();
                arr2[3] = courier.photo_path.ToString();
                arr2[4] = courier.phone_number.ToString();
                arr2[5] = courier.role.ToString();
                arr2[6] = courier.status.ToString();
            }
            else if (person is Admin admin)
            {
                arr2[0] = admin.first_name.ToString();
                arr2[1] = admin.second_name.ToString();
                arr2[2] = admin.sur_name.ToString();
                arr2[3] = admin.photo_path.ToString();
                arr2[4] = admin.phone_number.ToString();
                arr2[5] = admin.role.ToString();
                arr2[6] = admin.status.ToString();
            }
            int error_code = Ioperations.Insert_To_Table(Constants.PERSON_TABLENAME, arr, arr2);
            if (error_code != Constants.ERROR_ALL_OK) { return error_code; }
            string[] arr3 = { "phone_number", "password" };
            string[] arr4 = { person.phone_number.ToString(), person.password.ToString() };
            Ioperations.Insert_To_Table(Constants.ENTRY_TABLENAME, arr3, arr4);
            return Constants.ERROR_ALL_OK;
        }
        public int Add_Restaurant(Restaurant restaurant)
        {
            string[] arr = { "name", "description" };
            string[] arr2 = { restaurant.Name, restaurant.Description };
            DataTable? accounts = Ioperations.Get_List_from_table(Constants.RESTAURANT_TABLENAME, arr, arr2);
            if (accounts.Rows.Count != 0) return Constants.ERROR_RESTAURANTISALREADYEXIST;
            string[] arr3 = { "name", "description", "picture_path" };
            string[] arr4 = { restaurant.Name.ToString(), restaurant.Description.ToString(), restaurant.Picture_Path.ToString() };
            Ioperations.Insert_To_Table(Constants.RESTAURANT_TABLENAME, arr3, arr4);
            return Constants.ERROR_ALL_OK;
        }
        public int Add_Food(Food food)
        {
            string[] arr = { "name", "description" };
            string[] arr2 = { food.Name, food.Description };
            DataTable? accounts = Ioperations.Get_List_from_table(Constants.FOOD_TABLENAME, arr, arr2);
            if (accounts.Rows.Count != 0) return Constants.ERROR_FOODISALREADYEXIST;
            string[] arr3 = { "name", "description", "picture_path" };
            string[] arr4 = { food.Name.ToString(), food.Description.ToString(), food.Picture_path.ToString() };
            Ioperations.Insert_To_Table(Constants.FOOD_TABLENAME, arr3, arr4);
            return Constants.ERROR_ALL_OK;
        }
        public void Add_Basket(Basket basket)
        {
            string[] arr = { "id_food", "id_client", "amount", "id_restaurant" };
            string[] arr2 = { basket.Id_Food.ToString(), basket.Id_Client.ToString(), basket.Amount.ToString(), basket.Id_Restaurant.ToString() };
            Ioperations.Insert_To_Table(Constants.BASKET_TABLENAME, arr, arr2);
        }
    }
}