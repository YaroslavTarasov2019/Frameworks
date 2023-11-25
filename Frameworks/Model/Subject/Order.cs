using Frameworks.Model.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.Model.Subject
{
    public class Order : Operations
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public int Id_Courier { get; set; }
        public int Id_Status { get; set; }
        public int Id_Client { get; set; }
        public int Id_Manager { get; set; }
        
        public Order(string adress, int id_Courier, int id_Status, int id_Client)
        {
            Adress = adress;
            Id_Courier = id_Courier;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = 0;
        }
        public Order(int id, string adress, int id_Courier, int id_Status, int id_Client, int id_Manager)
        {
            Id = id;
            Adress = adress;
            Id_Courier = id_Courier;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = id_Manager;
        }
        public Order(int id, string adress, int id_Status, int id_Client)
        {
            Id = id;
            Adress = adress;
            Id_Courier = 0;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = 0;
        }
        public Order(int id, string adress, int id_Courier, int id_Status, int id_Client)
        {
            Id = id;
            Adress = adress;
            Id_Courier = id_Courier;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = 0;
        }
        public Order(int id, int id_Status, int id_Client, int id_Manager, string adress)
        {
            Id = id;
            Adress = adress;
            Id_Courier = 0;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = id_Manager;
        }
        public Order(int id, int id_Status, int id_Client, int id_Manager)
        {
            Id = id;
            Adress = "";
            Id_Courier = 0;
            Id_Status = id_Status;
            Id_Client = id_Client;
            Id_Manager = id_Manager;
        }
        public Order(int id, int id_Status)
        {
            Id = id;
            Adress = "";
            Id_Courier = 0;
            Id_Status = id_Status;
            Id_Client = 0;
        }

        public void Add_to_DataBase()
        {
            IAdd add = new Add_Service();
            add.Add_Order(this);
        }

    }
}
