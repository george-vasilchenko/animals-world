namespace AnimalsWorld
{
   public interface INavigationManager<out TRoute>
   {
      TRoute GetLastRoute();
   }
}