using System;
using System.Data;
using Frameworks.Model.Subject;

namespace Frameworks.Model.Services
{
    public class Get_Something_Service : IGetS
    {
        IOperations Ioperations = new Operations();
        public DataTable Get_All_Food()
        {
            return Ioperations.Get_List_from_table(Constants.FOOD_TABLENAME, null, null);
        }
        public DataTable Get_All_Features()
        {
            return Ioperations.Get_List_from_table(Constants.FEATURES_TABLENAME, null, null);
        }
        public DataTable Get_OrderStatus_By_Id(int id)
        {
            string[] arr1 = { "id" };
            string[] arr2 = { id.ToString() };
            return Ioperations.Get_List_from_table(Constants.STATUSORDER_TABLENAME, arr1, arr2);
        }
        public DataTable Get_Food_By_Features(string features)
        {
            string[] arr5 = { "name" };
            string[] arr6 = { features };
            DataTable d = Ioperations.Get_List_from_table(Constants.FEATURES_TABLENAME, arr5, arr6);
            if (d.Rows.Count == 0) return new DataTable();
            DataRow row = d.Rows[0];
            string[] arr1 = { "id_features" };
            string[] arr2 = { row[0].ToString() ?? "0" };
            DataTable dt = Ioperations.Get_List_from_table(Constants.FOODFEATURES_TABLENAME, arr1, arr2);
            DataRow[] f = new DataRow[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string[] arr3 = { "id" };
                string[] arr4 = { dr[0].ToString() ?? "0" };
                
                f[i] = Ioperations.Get_List_from_table(Constants.FOOD_TABLENAME, arr3, arr4).Rows[0];
                i++;
            }
            DataTable dataTable = new DataTable();

            if (f.Length > 0)
            {
                foreach (DataColumn column in f[0].Table.Columns)
                {
                    dataTable.Columns.Add(column.ColumnName, column.DataType);
                }
            }

            foreach (DataRow dataRow in f)
            {
                dataTable.LoadDataRow(dataRow.ItemArray, true);
            }
            return dataTable;
        }
        public DataTable Get_Part_of_Food(string[] param_name, string[] param_value)
        {
            return Ioperations.Get_List_from_table(Constants.FOOD_TABLENAME, param_name, param_value);
        }

        public DataTable Get_All_Basket()
        {
            return Ioperations.Get_List_from_table(Constants.BASKET_TABLENAME, null, null);
        }
        public DataTable Get_Part_of_Basket(string[] param_name, string[] param_value)
        {
            return Ioperations.Get_List_from_table(Constants.BASKET_TABLENAME, param_name, param_value);
        }

        public DataTable Get_All_Restaurant()
        {
            return Ioperations.Get_List_from_table(Constants.RESTAURANT_TABLENAME, null, null);
        }
        public DataTable Get_Part_of_Restaurant(string[] param_name, string[] param_value)
        {
            return Ioperations.Get_List_from_table(Constants.RESTAURANT_TABLENAME, param_name, param_value);
        }

        public DataTable Get_Order_By_ID_Person(int id_person)
        {
            string[] arr1 = { "id_client" };
            string[] arr2 = { id_person.ToString() };
            return Ioperations.Get_List_from_table(Constants.ORDER_TABLENAME, arr1, arr2);
        }

        public Order Get_Order_By_ID_Courier(int id_courier)
        {
            string[] arr1 = { "id_courier" };
            string[] arr2 = { id_courier.ToString() };
            DataRow row = Ioperations.Get_List_from_table(Constants.ORDER_TABLENAME, arr1, arr2 ).Rows[0];
            return new Order(Convert.ToInt32(row[0].ToString()), row[1].ToString() ?? "", Convert.ToInt32(row[2].ToString()),
                Convert.ToInt32(row[3].ToString()), Convert.ToInt32(row[4].ToString()), Convert.ToInt32(row[5].ToString()));
        }

        public DataTable Get_Order_Food_By_ID_Order(int id_order)
        {
            string[] arr1 = { "id_order" };
            string[] arr2 = { id_order.ToString() };
            return Ioperations.Get_List_from_table(Constants.ORDERFOOD_TABLENAME, arr1, arr2);
        }
        public Food Get_Food_Info_By_ID(int id)
        {
            string[] arr1 = { "id" };
            string[] arr2 = { id.ToString() };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.FOOD_TABLENAME, arr1, arr2).Rows[0];
            return new Food(dataRow[1].ToString() ?? "", dataRow[2].ToString() ?? "", dataRow[3].ToString() ?? "");
        }
        public DataTable Get_Restaurant_Food_By_Food_ID(int id_food)
        {
            string[] arr1 = { "id_food" };
            string[] arr2 = { id_food.ToString() };
            return Ioperations.Get_List_from_table(Constants.RESTAURANTFOOD_TABLENAME, arr1, arr2);
        }
        public DataTable Get_Restaurant_Food_By_Restaurant_ID(int id_restaurant)
        {
            string[] arr1 = { "id_restaurant" };
            string[] arr2 = { id_restaurant.ToString() };
            return Ioperations.Get_List_from_table(Constants.RESTAURANTFOOD_TABLENAME, arr1, arr2);
        }
        public int Get_Price_By_Food_and_Restaurant(int id_food, int id_restaurant)
        {
            string[] arr1 = { "id_food", "id_restaurant" };
            string[] arr2 = { id_food.ToString(), id_restaurant.ToString() };
            return Convert.ToInt32(Ioperations.Get_List_from_table(Constants.RESTAURANTFOOD_TABLENAME, arr1, arr2).Rows[0][2].ToString());
        }
        public Restaurant Get_Restaurant_Info_By_ID(int id)
        {
            string[] arr1 = { "id" };
            string[] arr2 = { id.ToString() };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.RESTAURANT_TABLENAME, arr1, arr2).Rows[0];
            return new Restaurant(dataRow[1].ToString() ?? "", dataRow[2].ToString() ?? "", dataRow[3].ToString() ?? "");
        }
        public void Delete_Basket_By_ID(int id)
        {
            string[] arr1 = { "id" };
            string[] arr2 = { id.ToString() };
            Ioperations.Delete_From_Table(Constants.BASKET_TABLENAME, arr1, arr2);
        }

        public void Update_Basket_By_ID(int id_basket, int amount)
        {
            string[] arr1 = { "amount" };
            string[] arr2 = { amount.ToString() };
            string[] arr3 = { "id" };
            string[] arr4 = { id_basket.ToString() };
            Ioperations.Update_From_Table(Constants.BASKET_TABLENAME, arr1, arr2, arr3, arr4);
        }

        public void Update_Courier_By_ID(int id_order, int id_courier)
        {
            string[] arr1 = { "id_courier" };
            string[] arr2 = { id_courier.ToString() };
            string[] arr3 = { "id" };
            string[] arr4 = { id_order.ToString() };
            Ioperations.Update_From_Table(Constants.ORDER_TABLENAME, arr1, arr2, arr3, arr4);
        }

        public Order[] Get_Order_By_Manager_and_Status(int id_manager, int status)
        {
            string[] arr1 = { "id_manager", "id_status" };
            string[] arr2 = { id_manager.ToString(), status.ToString() };
            DataTable dt = Ioperations.Get_List_from_table(Constants.ORDER_TABLENAME, arr1, arr2);
            Order[] orders = new Order[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                orders[i] = new Order(Convert.ToInt32(row[0].ToString()), row[1].ToString(),
                   Convert.ToInt32(row[2].ToString()), Convert.ToInt32(row[3].ToString()), Convert.ToInt32(row[4].ToString()));
                i++;
            }
            return orders;
        }
        public DataTable Get_OrderFood_By_IdOrder(int id_order)
        {
            string[] arr1 = { "id_order" };
            string[] arr2 = { id_order.ToString() };
            return Ioperations.Get_List_from_table(Constants.ORDERFOOD_TABLENAME, arr1, arr2);
        }
        public Order? Get_One_Wait_Order()
        {
            string[] arr1 = { "id_status" };
            string[] arr2 = { "2" };
            DataTable dt = Ioperations.Get_List_from_table(Constants.ORDER_TABLENAME, arr1, arr2);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Order(Convert.ToInt32(row[0].ToString()), Convert.ToInt32(row[3].ToString()),
                    Convert.ToInt32(row[4].ToString()), Convert.ToInt32(row[5].ToString()), row[1].ToString());
            }
            return null;
        }
    }
}