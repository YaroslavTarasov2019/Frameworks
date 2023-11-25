using Frameworks.Model.Services;
using Frameworks.Model;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace Frameworks.ViewModel
{
    public class SpecialFunctionsForViewModel
    {
        public string Find_Image_str(string image_path)
        {
            string path;
            path = @"C:\Users\taras\source\repos\Frameworks\Frameworks\Photo\" + image_path + ".jpeg";
            if (!File.Exists(path)) path = @"C:\Users\taras\source\repos\Frameworks\Frameworks\Photo\Empty_com_file.jpg";
            return path;
        }
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentException("minValue повинно бути менше або дорівнювати maxValue");

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumberBytes = new byte[4];
                rng.GetBytes(randomNumberBytes);
                uint randomUInt = BitConverter.ToUInt32(randomNumberBytes, 0);
                long range = (long)maxValue - minValue + 1;
                long scaledValue = (long)(randomUInt % range);
                return (int)(scaledValue + minValue);
            }
        }
        public DataTable RuleOrNo(string objects, string MyRule, bool IsFilter, string SelectedValue, int ID_Person)
        {
            if (objects == "Restaurant")
            {
                if (MyRule != "" && MyRule != null)
                    return new Operations().Get_Like_From_Table_With_One_Param(Constants.RESTAURANT_TABLENAME, "name", MyRule);
                return new Get_Something_Service().Get_All_Restaurant();
            }
            else if (objects == "Food")
            {
                if (!IsFilter)
                {
                    if (MyRule != "" && MyRule != null)     return new Operations().Get_Like_From_Table_With_One_Param(Constants.FOOD_TABLENAME, "name", MyRule);
                }
                else
                {
                    if (SelectedValue != "") return new Get_Something_Service().Get_Food_By_Features(SelectedValue);
                }
                return new Get_Something_Service().Get_All_Food();
            }
            else if (objects == "Basket") return new Get_Something_Service().Get_Part_of_Basket(new[] { "id_client" }, new[] { ID_Person.ToString() });
            else
            {
                return new Get_Something_Service().Get_Order_By_ID_Person(ID_Person);
            }
        }
    }
}
