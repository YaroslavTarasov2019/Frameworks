using System;
using System.Data;
using Frameworks.Model.Subject;

namespace Frameworks.Model.Services
{
    public class OrderOperation : IOrder
    {
        IOperations Ioperations = new Operations();

        public const int ErrorCodeNoBaskets = 108;
        public const int ErrorCodeNoClient = 109;


        public Basket[] MapBasketData(  // change on private
            DataTable baskets,
            int clientId)
        {
            if (baskets.Rows.Count == 0) return new Basket[0];
            DataRow firstRow = baskets.Rows[0];
            Basket firstBasket = new Basket(
                firstRow[0]?.ToString() ?? "Is null",
                firstRow[1]?.ToString() ?? "Is null",
                clientId.ToString(),
                firstRow[3]?.ToString() ?? "Is null",
                firstRow[4]?.ToString() ?? "Is null"
                );
            baskets.Rows.RemoveAt(0);
            Basket[] remainingBaskets = MapBasketData(baskets, clientId);
            Basket[] allBaskets = new Basket[remainingBaskets.Length + 1];
            allBaskets[0] = firstBasket;
            Array.Copy(remainingBaskets, 0, allBaskets, 1, remainingBaskets.Length);
            return allBaskets;
        }

        public void InsertOrderFoods(    // change on private
            Basket[] baskets,
            int new_id)
        {
            foreach (Basket b in baskets)
            {
                string[] arr3 = { "id_food", "id_restaurant" };
                string[] arr4 = { b.Id_Food.ToString(), b.Id_Restaurant.ToString() };
                int cost = Convert.ToInt32(Ioperations.Get_List_from_table(Constants.RESTAURANTFOOD_TABLENAME, arr3, arr4).Rows[0][2]);
                string[] arr = { "id_order", "id_food", "cost_for_one", "amount", "id_restaurant" };
                string[] arr2 = { new_id.ToString(), b.Id_Food.ToString(), cost.ToString(), b.Amount.ToString(), b.Id_Restaurant.ToString() };
                Ioperations.Insert_To_Table(Constants.ORDERFOOD_TABLENAME, arr, arr2);
            }
        }

        public bool IsClient(int id)
        {
            string[] arr = { "id", "role" };
            string[] arr2 = { id.ToString(), "CLIENT" };
            if (Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows.Count == 0)
                return false;
            return true;
        }

        public DataTable? SelectBasketsbyID(int id)
        {
            string[] arr = { "id_client" };
            string[] arr2 = { id.ToString() };
            DataTable count_baskets = Ioperations.Get_List_from_table(Constants.BASKET_TABLENAME, arr, arr2);
            if (count_baskets.Rows.Count == 0) return null;
            return count_baskets;
        }

        public int CreateOrder(int id_client, string adress, int id_manager)
        {
            if (IsClient(id_client) == false) return ErrorCodeNoClient;
            DataTable? count_baskets = SelectBasketsbyID(id_client);
            if (count_baskets == null) return ErrorCodeNoBaskets;
            Basket[] baskets = MapBasketData(count_baskets, id_client);
            string[] arr3 = { "id_client" };
            string[] arr4 = { id_client.ToString() };
            Ioperations.Delete_From_Table(Constants.BASKET_TABLENAME, arr3, arr4);
            string[] arr = new string[0];
            string[] arr2 = new string[0];
            int new_id = Ioperations.Get_List_from_table(Constants.ORDER_TABLENAME, arr, arr2).Rows.Count + 1;
            InsertOrderFoods(baskets, new_id);
            Order order = new Order(new_id, 1, id_client, id_manager, adress);
            order.Add_to_DataBase();
            return 0;
        }

        public void ChangeOrderStatus(Order order)  // for client (confirm delivery) and manager
        {
            string[] arr = { "id_status" };
            string[] arr2 = { order.Id_Status.ToString() };
            string[] arr3 = { "id" };
            string[] arr4 = { order.Id.ToString() };
            Ioperations.Update_From_Table(Constants.ORDER_TABLENAME, arr, arr2, arr3, arr4);
        }
    }
}