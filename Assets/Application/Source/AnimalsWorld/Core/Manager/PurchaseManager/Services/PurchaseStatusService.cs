using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class PurchaseStatusService : IPurchaseStatusService
   {
      private readonly IPersistentPreferencesService _persistentPreferencesService;

      public PurchaseStatusService(IPersistentPreferencesService persistentPreferencesService)
      {
         _persistentPreferencesService = persistentPreferencesService;
      }

      bool IPurchaseStatusService.IsRegistered(IPurchaseProduct product)
         => _persistentPreferencesService.IsIntegerValueMatching(product.Id, PurchaseStatus.IsRegistered);

      bool IPurchaseStatusService.IsRegistered(string productId)
         => _persistentPreferencesService.IsIntegerValueMatching(productId, PurchaseStatus.IsRegistered);

      void IPurchaseStatusService.AddToRegistry(IPurchaseProduct product)
         => _persistentPreferencesService.SetIntEntry(product.Id, PurchaseStatus.IsRegistered);

      void IPurchaseStatusService.SetOpenForDelivery(IPurchaseProduct product)
         => _persistentPreferencesService.SetIntEntry(product.Id, PurchaseStatus.IsOpenForDelivery);

      bool IPurchaseStatusService.IsOpenForDelivery(IPurchaseProduct product)
         => _persistentPreferencesService.IsIntegerValueMatching(product.Id, PurchaseStatus.IsOpenForDelivery);

      bool IPurchaseStatusService.IsOpenForDelivery(string productId)
         => _persistentPreferencesService.IsIntegerValueMatching(productId, PurchaseStatus.IsOpenForDelivery);
   }
}