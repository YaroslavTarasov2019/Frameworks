namespace Frameworks.Model.Services
{
    public class Attach_Service : IAttach
    {
        IOperations Ioperations = new Operations();
        public void Attach_Features_to_Food(int id_features, int id_food)
        {
            string[] arr = { "id_food", "id_features" };
            string[] arr2 = { id_food.ToString(), id_features.ToString() };
            Ioperations.Insert_To_Table(Constants.FOODFEATURES_TABLENAME, arr, arr2);
        }
        public void Attach_Food_to_Restaurant(int id_restaurant, int id_food, int cost)
        {
            string[] arr = { "id_restaurant", "id_food", "cost" };
            string[] arr2 = { id_restaurant.ToString(), id_food.ToString(), cost.ToString() };
            Ioperations.Insert_To_Table(Constants.RESTAURANTFOOD_TABLENAME, arr, arr2);
        }
    }
}
