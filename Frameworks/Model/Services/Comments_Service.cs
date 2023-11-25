using System;
using System.Data;

namespace Frameworks.Model.Services
{
    public class Comments_Service: IComment
    {
        IOperations Ioperations = new Operations();

        // Додати коментар до страви
        public int Add_Comment(int id_food, int id_client, string text, int points)
        {
            if (points <= 0 || points > 5) { return 150; }
            string[] arr = { "id_food", "id_client", "text", "datetime", "amount_points", "amount_like", "amount_dislike" };
            string[] arr2 = { id_food.ToString(), id_client.ToString(), text, DateTime.Now.ToString(), points.ToString(), "0", "0" };
            if (Ioperations.Insert_To_Table(Constants.COMMENT_TABLENAME, arr, arr2) != Constants.ERROR_ALL_OK )     return Constants.ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH;

            return 0;
        }
        // Допоміжний метод: отримати дані про коментар
        private DataRow? Get_Old_Points(int id_comment)
        {
            string[] arr5 = { "id" };
            string[] arr6 = { id_comment.ToString() };
            DataTable dataTable = Ioperations.Get_List_from_table(Constants.COMMENT_TABLENAME, arr5, arr6);
            if (dataTable.Rows.Count == 0) return null;
            return dataTable.Rows[0];
        }
        // Додати лайк до коментаря
        public int Give_Like(int id_comment)
        {
            DataRow? row = Get_Old_Points(id_comment);
            if (row == null) return 151;
            int new_amount_like = Convert.ToInt32(row[6]) + 1;

            string[] arr = { "amount_like" };
            string[] arr2 = { new_amount_like.ToString() };
            string[] arr3 = { "id" };
            string[] arr4 = { id_comment.ToString() };
            if (Ioperations.Update_From_Table(Constants.COMMENT_TABLENAME, arr, arr2, arr3, arr4) != Constants.ERROR_ALL_OK) return Constants.ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH;

            return 0;
        }
        // Додати дізлайк до коментаря
        public int Give_Dislike(int id_comment)
        {
            DataRow? row = Get_Old_Points(id_comment);
            if (row == null) return 151;
            int new_amount_dislike = Convert.ToInt32(row[7]) + 1;

            string[] arr = { "amount_dislike" };
            string[] arr2 = { new_amount_dislike.ToString() };
            string[] arr3 = { "id" };
            string[] arr4 = { id_comment.ToString() };
            if (Ioperations.Update_From_Table(Constants.COMMENT_TABLENAME, arr, arr2, arr3, arr4) != Constants.ERROR_ALL_OK) return Constants.ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH;

            return 0;
        }
        // Отримати всі коментарі про обрану страву
        public DataTable Show_Comments(int id_food) 
        {
            string[] arr = { "id_food" };
            string[] arr2 = { id_food.ToString() };
            return Ioperations.Get_List_from_table(Constants.COMMENT_TABLENAME, arr, arr2);
        }

        public bool Is_Comment(int id_food, int id_client)
        {
            string[] arr = { "id_food", "id_client" };
            string[] arr2 = { id_food.ToString(), id_client.ToString() };
            DataTable dt = Ioperations.Get_List_from_table(Constants.COMMENT_TABLENAME, arr, arr2);
            if (dt.Rows.Count == 0) return false;
            return true;
        }
    }
}