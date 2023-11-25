using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Frameworks.Model
{
    public partial class Operations: IOperations
    {
        private DataBase dataBase = new DataBase();

        public DataTable? Get_operation(MySqlCommand command)
        {
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                dataBase.openConnection();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                dataBase.closeConnection();
                return dt;
            }
            catch (MySqlException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void Set_operation(MySqlCommand command)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            dataBase.openConnection();
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
            dataBase.closeConnection();
        }

        public DataTable Get_List_from_table(string table, string[] param_name, string[] param_value)    // restaurant or food or order or order_food or features or food_features
        {
            int len;
            if (param_name != null && param_value != null && param_name.Length != param_value.Length) return new DataTable();
            string com = "";
            if (param_name == null || param_value == null || param_name.Length == 0)
            {
                com += "SELECT * FROM " + table;
                len = 0;
            }
            else
            {
                len = param_name.Length;
                com += "SELECT * FROM " + table + " WHERE ";
                for (int i = 0; i < param_name.Length - 1; i++) com += param_name[i] + " = @param" + (i + 1) + " AND ";
                com += param_name[param_name.Length - 1] + " = @param" + param_name.Length;
            }
            MySqlCommand command = new MySqlCommand(com, dataBase.GetConnection());
            for (int i = 1; i <= len; i++)           command.Parameters.AddWithValue("@param" + i.ToString(), param_value[i - 1]);
            DataTable? get = Get_operation(command);
            if (get != null) return get;
            return new DataTable();
        }

        public int Insert_To_Table(string table, string[] param_name, string[] param_value)
        {
            if (param_name.Length != param_value.Length)    return Constants.ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH;
            string com = "INSERT INTO " + table + " (";            
            for (int i = 0; i < param_name.Length-1; i++)       com += "`" + param_name[i] + "`, ";
            com += "`" + param_name[param_name.Length-1] + "`) VALUES (";
            for (int i = 1; i <= param_value.Length-1; i++)     com += "@param" + i + ", ";
            com += "@param" + (param_value.Length).ToString() + ")";
            MySqlCommand command = new MySqlCommand(com, dataBase.GetConnection());
            for (int i = 1; i <= param_value.Length; i++)           command.Parameters.AddWithValue("@param" + i.ToString(), param_value[i - 1]);
            Set_operation(command);
            return Constants.ERROR_ALL_OK;
        }

        public int Update_From_Table(string table, string[] param_name, string[] param_value, string[] cond_param_name, string[] cond_param_value)
        {
            if (param_name.Length != param_value.Length || cond_param_name.Length != cond_param_value.Length) 
                return Constants.ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH;
            string com = "UPDATE " + table + " SET ";
            for (int i = 0; i < param_name.Length - 1; i++)         com += param_name[i] + " = @param" + (i+1) + ", ";            
            com += param_name[param_name.Length - 1] + " = @param" + param_name.Length + " WHERE ";            
            for (int i = 0; i < cond_param_name.Length - 1; i++)    com += cond_param_name[i] + " = @param" + (param_name.Length + i + 1).ToString() + " and ";            
            com += cond_param_name[cond_param_name.Length - 1] + " = @param" + (cond_param_name.Length + param_name.Length);
            MySqlCommand command = new MySqlCommand(com, dataBase.GetConnection());
            for (int i = 1; i <= param_value.Length; i++)           command.Parameters.AddWithValue("@param" + i.ToString(), param_value[i - 1]);
            for (int i = 1; i <= cond_param_value.Length; i++)      command.Parameters.AddWithValue("@param" + (i + param_value.Length).ToString(), cond_param_value[i - 1]);
            Set_operation(command);
            return Constants.ERROR_ALL_OK;
        }

        public int Delete_From_Table(string table, string[] param_name, string[] param_value)  
        {
            if (param_name.Length != param_value.Length) return 111;
            if (param_name.Length == 0) return 112;
            string com = "DELETE FROM " + table + " WHERE ";
            for (int i = 0; i < param_name.Length - 1; i++)         com += param_name[i] + " = @param" + (i + 1) + " AND ";
            com += param_name[param_name.Length - 1] + " = @param" + param_name.Length;
            MySqlCommand command = new MySqlCommand(com, dataBase.GetConnection());
            for (int i = 1; i <= param_value.Length; i++)           command.Parameters.AddWithValue("@param" + i.ToString(), param_value[i - 1]);
            Set_operation(command);
            return 0;
        }

        public DataTable Get_Like_From_Table_With_One_Param(string table, string param_name, string param_value)
        {
            if (param_name == null || param_value == null) return new DataTable();
            string com = "SELECT * FROM " + table + " WHERE " + param_name + " LIKE '" + param_value + "%'";
            MySqlCommand command = new MySqlCommand(com, dataBase.GetConnection());
            DataTable? get = Get_operation(command);
            if (get != null) return get;
            return new DataTable();
        }
    }
}