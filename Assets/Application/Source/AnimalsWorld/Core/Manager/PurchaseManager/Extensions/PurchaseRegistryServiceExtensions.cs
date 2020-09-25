using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public static class PurchaseRegistryServiceExtensions
   {
      public static bool IsIntegerValueMatching(this IPersistentPreferencesService service, string key, int expectedValue)
      {
         if (!service.HasKey(key))
         {
            return false;
         }

         return service.GetIntEntry(key) == expectedValue;
      }
   }
}