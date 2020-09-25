using MalenkiyApps.Interfaces;
using UnityEngine.Purchasing;

namespace AnimalsWorld
{
   public static class UnityInAppPurchaseExtensions
   {
      public static IPurchaseProduct ToPurchaseProduct(this Product product)
         => new PurchaseProduct(product.definition.id,
         product.metadata.localizedTitle,
         (PurchaseProductType)product.definition.type,
         product.receipt);
   }
}