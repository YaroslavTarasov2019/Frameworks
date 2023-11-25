using System;
using System.Data;
using System.Text.RegularExpressions;
using Frameworks.Model.Subject;

namespace Frameworks.Model.Services
{
    public class Person_Service : IPerson
    {
        IOperations Ioperations = new Operations();
        public int Register_Person(Person person)
        {
            IAdd Iadd = new Add_Service();
            if (person is Client client)
            {
                if (!Regex.IsMatch(client.phone_number, Constants.PATTERN_PHONENUMBER)) return Constants.ERROR_WRONGFORMAT_PHONENUMBER;
                int add_error = Iadd.Add_User(client);
                if (add_error != Constants.ERROR_ALL_OK) return add_error;
            }
            else if (person is Courier courier)
            {
                if (!Regex.IsMatch(courier.phone_number, Constants.PATTERN_PHONENUMBER)) return Constants.ERROR_WRONGFORMAT_PHONENUMBER;
                int add_error = Iadd.Add_User(courier);
                if (add_error != Constants.ERROR_ALL_OK) return add_error;
            }
            else if (person is Admin admin)
            {
                if (!Regex.IsMatch(admin.phone_number, Constants.PATTERN_PHONENUMBER)) return Constants.ERROR_WRONGFORMAT_PHONENUMBER;
                int add_error = Iadd.Add_User(admin);
                if (add_error != Constants.ERROR_ALL_OK) return add_error;
            }
            return Constants.ERROR_ALL_OK;
        }
        public int Change_Courier_Status(int id_courier, string status)
        {
            if (status != "FREE" && status != "WORK")   return 122;

            string[] arr = { "status" };
            string[] arr2 = { status };            
            string[] arr3 = { "id", "role" };
            string[] arr4 = { id_courier.ToString(), "COURIER" };
            return Ioperations.Update_From_Table(Constants.PERSON_TABLENAME, arr, arr2, arr3, arr4);
        }
        public int Change_Person_Info(Person newperson)
        {
            if (!Regex.IsMatch(newperson.phone_number, Constants.PATTERN_PHONENUMBER)) return Constants.ERROR_WRONGFORMAT_PHONENUMBER;

            string[] arr3 = { "id" };
            string[] arr4 = { newperson.id.ToString() };
            string tn = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr3, arr4)?.Rows[0][5].ToString() ?? "String is null";
            string[] arr = { "firstname", "secondname", "surname", "photo_path", "phone_number" };
            string[] arr2 = { newperson.first_name, newperson.second_name, newperson.sur_name, newperson.photo_path, newperson.phone_number, };
            Ioperations.Update_From_Table(Constants.PERSON_TABLENAME, arr, arr2, arr3, arr4);
            string[] arr5 = { "phone_number" };
            string[] arr6 = { newperson.phone_number.ToString() };
            string[] arr7 = { "phone_number" };
            string[] arr8 = { tn.ToString() };
            Ioperations.Update_From_Table(Constants.ENTRY_TABLENAME, arr5, arr6, arr7, arr8);
            return Constants.ERROR_ALL_OK;
        }
        public bool Check_Login_Password(Entry entry)
        {
            string[] arr = { "phone_number", "password" };
            string[] arr2 = { entry.phone_number, entry.password };
            DataTable? accounts = Ioperations.Get_List_from_table(Constants.ENTRY_TABLENAME, arr, arr2);
            if (accounts == null || accounts.Rows.Count != 1) return false;
            return true;
        }
        public int Get_Id_Person_By_Phone_Number(string phone_number)
        {
            string[] arr = { "phone_number" };
            string[] arr2 = { phone_number };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows[0];
            return Convert.ToInt32(dataRow[0].ToString());
        }
        public Person Get_Person_Data_By_ID(int id)
        {
            string[] arr = { "id" };
            string[] arr2 = { id.ToString() };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows[0];
            return new Person(id, dataRow[1].ToString(), dataRow[2].ToString(), dataRow[3].ToString(), dataRow[4].ToString(), dataRow[5].ToString());
        }

        public string Get_Role_By_ID(int id) 
        {
            string[] arr = { "id" };
            string[] arr2 = { id.ToString() };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows[0];
            return dataRow[6].ToString();
        }
        public string Get_Role_By_Phone_Number(string phone_number)
        {
            string[] arr = { "phone_number" };
            string[] arr2 = { phone_number };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows[0];
            return dataRow[6].ToString();
        }
        public string Get_Photo_Path_By_ID(int id)
        {
            string[] arr = { "id" };
            string[] arr2 = { id.ToString() };
            DataRow dataRow = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2).Rows[0];
            return dataRow[4].ToString();
        }

        public int[] Get_ID_By_Role(string role)
        {
            string[] arr = { "role" };
            string[] arr2 = { role };
            DataTable dt = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2);
            int[] id = new int[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                id[i] = Convert.ToInt32(row[0].ToString());
                i++;
            }
            return id;
        }
        public int[] Get_ID_By_Role_and_Status(string role, string status)
        {
            string[] arr = { "role", "status" };
            string[] arr2 = { role, status };
            DataTable dt = Ioperations.Get_List_from_table(Constants.PERSON_TABLENAME, arr, arr2);
            int[] id = new int[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                id[i] = Convert.ToInt32(row[0].ToString());
                i++;
            }
            return id;
        }
    }
}