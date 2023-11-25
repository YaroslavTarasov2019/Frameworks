using Frameworks.Model.Subject;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Numerics;

namespace Frameworks.Model
{
    public interface IOperations
    {
        DataTable? Get_operation(MySqlCommand command);
        DataTable Get_List_from_table(string table, string[] param_name, string[] param_value);
        int Delete_From_Table(string table, string[] param_name, string[] param_value);
        int Insert_To_Table(string table, string[] param_name, string[] param_value);
        int Update_From_Table(string table, string[] param_name, string[] param_value, string[] cond_param_name, string[] cond_param_value);
        DataTable Get_Like_From_Table_With_One_Param(string table, string param_name, string param_value);
    }
    public interface IGetS
    {
        DataTable Get_All_Food();
        DataTable Get_All_Restaurant();
        DataTable Get_OrderStatus_By_Id(int id);
        DataTable Get_All_Features();
        DataTable Get_Food_By_Features(string features);
        Food Get_Food_Info_By_ID(int id);
        DataTable Get_Restaurant_Food_By_Food_ID(int id_food);
        DataTable Get_Restaurant_Food_By_Restaurant_ID(int id_restaurant);
        Restaurant Get_Restaurant_Info_By_ID(int id);
        int Get_Price_By_Food_and_Restaurant(int id_food, int id_restaurant);
        void Update_Basket_By_ID(int id_basket, int amount);
        void Update_Courier_By_ID(int id_order, int id_courier);
        void Delete_Basket_By_ID(int id);
        DataTable Get_Order_Food_By_ID_Order(int id_order);
        DataTable Get_Order_By_ID_Person(int id_person);
        Order[] Get_Order_By_Manager_and_Status(int id_manager, int status);
        Order Get_One_Wait_Order();
        Order Get_Order_By_ID_Courier(int id_courier);
        DataTable Get_OrderFood_By_IdOrder(int id_order);
    }

    public interface IAdd
    {
        int Add_Order(Order order);
        int Add_User(Person person);
        int Add_Restaurant(Restaurant restaurant);
        int Add_Food(Food food);
        void Add_Basket(Basket basket);
    }
    public interface IAttach
    {
        void Attach_Features_to_Food(int id_features, int id_food);  
        void Attach_Food_to_Restaurant(int id_restaurant, int id_food, int cost);
    }
    public interface IOrder
    {
        int CreateOrder(int id_client, string adress, int id_manager);
        void ChangeOrderStatus(Order order);
    }
    public interface IPerson
    {
        int Register_Person(Person person);
        int Change_Courier_Status(int id_courier, string status);
        int Change_Person_Info(Person newperson);
        bool Check_Login_Password(Entry entry);
        int Get_Id_Person_By_Phone_Number(string phone_number);
        Person Get_Person_Data_By_ID(int id);
        string Get_Role_By_ID(int id);
        string Get_Role_By_Phone_Number(string phone_number);
        string Get_Photo_Path_By_ID(int id);
        int[] Get_ID_By_Role(string role);
        int[] Get_ID_By_Role_and_Status(string role, string status);
    }
    public interface IComment
    {
        int Add_Comment(int id_food, int id_client, string text, int points);
        int Give_Like(int id_comment);
        int Give_Dislike(int id_comment);
        DataTable Show_Comments(int id_food);
        bool Is_Comment(int id_food, int id_client);
    }
}