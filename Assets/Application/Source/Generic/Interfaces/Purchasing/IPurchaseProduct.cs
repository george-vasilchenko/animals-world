namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseProduct
   {
      string Id { get; }

      string Title { get; }

      PurchaseProductType ProductType { get; }

      string Receipt { get; }
   }
}