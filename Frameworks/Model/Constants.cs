using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.Model
{
    public static class Constants
    {
        public const string BASKET_TABLENAME = "basket";
        public const string ENTRY_TABLENAME = "entry";
        public const string FEATURES_TABLENAME = "features";
        public const string FOOD_TABLENAME = "food";
        public const string FOODFEATURES_TABLENAME = "food_features";
        public const string ORDER_TABLENAME = "order_list";
        public const string ORDERFOOD_TABLENAME = "order_food";
        public const string PERSON_TABLENAME = "person";
        public const string RESTAURANT_TABLENAME = "restaurant";
        public const string RESTAURANTFOOD_TABLENAME = "restaurant_food";
        public const string STATUSORDER_TABLENAME = "status_order";
        public const string COMMENT_TABLENAME = "comment";


        public const string PATTERN_PHONENUMBER = @"^\d{3}-\d{3}-\d{4}$";


        public const string ROLE_ADMIN = "ADMIN";
        public const string ROLE_COURIER = "COURIER";
        public const string ROLE_CLIENT = "CLIENT";


        public const int ERROR_ALL_OK = 0;
        public const int ERROR_WRONGFORMAT_PHONENUMBER = 120;
        public const int ERROR_USERISALREADYEXIST = 101;
        public const int ERROR_RESTAURANTISALREADYEXIST = 102;
        public const int ERROR_FOODISALREADYEXIST = 103;
        public const int ERROR_NUMBEROFNAMESANDNUMBEROFVALUESDONOTMATCH = 107;
    }

}
