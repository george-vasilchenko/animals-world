using MalenkiyApps.Interfaces;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class PurchaseDeliveryService : MonoBehaviour, IPurchaseDeliveryService
   {
      public PurchaseDeliveryPackage[] PurchaseDeliveryPackages;

      private IPurchaseStatusService _purchaseStatusService;
      private IPurchaseCatalog _purchaseCatalog;

      private void Awake()
      {
         _purchaseStatusService = GameManager.Instance.PurchaseManager.StatusService;
         _purchaseCatalog = GameManager.Instance.PurchaseManager.PurchaseCatalog;
      }

      private void Start()
      {
         Debug.Log("Trying to deliver on Start.");

         Deliver();
      }

      public void Deliver()
      {
         Debug.Log("Delivery is triggered.");

         if (!PurchaseDeliveryPackages.Any())
         {
            Debug.Log("No delivery packages exist.");
            return;
         }

         if (!_purchaseCatalog.PurchasingProducts.Any())
         {
            Debug.Log("No products exist.");
            return;
         }

         foreach (var package in PurchaseDeliveryPackages)
         {
            DeliverPackage(package);
         }
      }

      private void DeliverPackage(PurchaseDeliveryPackage package)
      {
         // search product
         var product = _purchaseCatalog.PurchasingProducts
            .FirstOrDefault(o => o.Id == package.ProductId);

         if (product == null)
         {
            Debug.Log($"Purchasing configuration mismatch. Product with Id {package.ProductId} can't be found in the catalog.");
            return;
         }

         // check if product is open for delivery and deliver it
         if (!_purchaseStatusService.IsOpenForDelivery(product))
         {
            return;
         }

         Debug.Log($"Delivering {product.Title}.");
         package.OnDeliveredEvent.Invoke();
      }
   }
}