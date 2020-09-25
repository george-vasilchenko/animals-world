namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseStatusService
   {
      bool IsRegistered(IPurchaseProduct product);

      bool IsRegistered(string productId);

      void AddToRegistry(IPurchaseProduct product);

      void SetOpenForDelivery(IPurchaseProduct product);

      bool IsOpenForDelivery(IPurchaseProduct product);

      bool IsOpenForDelivery(string productId);
   }
}