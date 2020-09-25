namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseManager : IPreloadable
   {
      PurchaseManagerStatus PurchaseManagerStatus { get; }

      IPurchaseExternalSystemHandler PurchaseExternalSystemHandler { get; }

      IPurchaseStatusService StatusService { get; }

      IPurchaseCatalog PurchaseCatalog { get; }

      void Purchase(IPurchaseProduct product);

      void Purchase(string productId);

      void RestoreTransactions();
   }
}