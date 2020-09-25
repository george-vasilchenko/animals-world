using MalenkiyApps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using InitializationFailureReason = UnityEngine.Purchasing.InitializationFailureReason;
using PurchaseFailureReason = UnityEngine.Purchasing.PurchaseFailureReason;

namespace AnimalsWorld
{
   public class UnityInAppPurchaseSystemHandler :
      IPurchaseExternalSystemHandler, IStoreListener
   {
      #region State

      private event OnInitializationOccuredDelegate OnInitializationOccured;

      private event OnInitializationFailedDelegate OnInitializationFailed;

      private event OnPurchaseInitiatedDelegate OnPurchaseInitiated;

      private event OnRestoreTransactionsInitiatedDelegate OnRestoreTransactionsInitiated;

      private event OnRestoreTransactionsFinishedDelegate OnRestoreTransactionsFinished;

      private event OnPurchaseOccuredDelegate OnPurchaseOccured;

      private event OnPurchaseFailedDelegate OnPurchaseFailed;

      private IStoreController _storeController;
      private IExtensionProvider _extensionProvider;

      #endregion State

      public UnityInAppPurchaseSystemHandler()
      {
      }

      #region IPurchaseExternalSystemHandler

      event OnInitializationOccuredDelegate IPurchaseExternalSystemHandler.OnInitializationOccured
      {
         add => OnInitializationOccured += value;
         remove => OnInitializationOccured -= value;
      }

      event OnInitializationFailedDelegate IPurchaseExternalSystemHandler.OnInitializationFailed
      {
         add => OnInitializationFailed += value;
         remove => OnInitializationFailed -= value;
      }

      event OnPurchaseInitiatedDelegate IPurchaseExternalSystemHandler.OnPurchaseInitiated
      {
         add => OnPurchaseInitiated += value;
         remove => OnPurchaseInitiated -= value;
      }

      event OnRestoreTransactionsInitiatedDelegate IPurchaseExternalSystemHandler.OnRestoreTransactionsInitiated
      {
         add => OnRestoreTransactionsInitiated += value;
         remove => OnRestoreTransactionsInitiated -= value;
      }

      event OnRestoreTransactionsFinishedDelegate IPurchaseExternalSystemHandler.OnRestoreTransactionsFinished
      {
         add => OnRestoreTransactionsFinished += value;
         remove => OnRestoreTransactionsFinished -= value;
      }

      event OnPurchaseOccuredDelegate IPurchaseExternalSystemHandler.OnPurchaseOccured
      {
         add => OnPurchaseOccured += value;
         remove => OnPurchaseOccured -= value;
      }

      event OnPurchaseFailedDelegate IPurchaseExternalSystemHandler.OnPurchaseFailed
      {
         add => OnPurchaseFailed += value;
         remove => OnPurchaseFailed -= value;
      }

      void IPurchaseExternalSystemHandler.Purchase(IPurchaseProduct product)
      {
         var storeProduct = _storeController.products.WithID(product.Id);

         if (storeProduct != null && storeProduct.availableToPurchase)
         {
            OnPurchaseInitiated?.Invoke();
            _storeController.InitiatePurchase(storeProduct);
         }
      }

      void IPurchaseExternalSystemHandler.Purchase(string productId)
      {
         var storeProduct = _storeController.products.WithID(productId);

         if (storeProduct != null && storeProduct.availableToPurchase)
         {
            OnPurchaseInitiated?.Invoke();
            _storeController.InitiatePurchase(storeProduct);
         }
      }

      void IPurchaseExternalSystemHandler.Configure(IEnumerable<IPurchaseProduct> productsToConfigure)
      {
         var products = productsToConfigure as IPurchaseProduct[] ?? productsToConfigure.ToArray();

         if (!products.Any())
         {
            throw new Exception("Purchasing Products collection must not be empty.");
         }

         var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

         foreach (var product in products)
         {
            builder.AddProduct(product.Id, (ProductType)(int)product.ProductType);
         }

         UnityPurchasing.Initialize(this, builder);
      }

      void IPurchaseExternalSystemHandler.RestoreTransactions()
      {
#if (UNITY_IOS || UNITY_EDITOR_OSX) && !UNITY_EDITOR
         if (_storeController == null || _extensionProvider == null)
            {
                Debug.Log("IAP restore: has not been initialized.");
                return;
            }

            OnRestoreTransactionsInitiated?.Invoke();
            _extensionProvider.GetExtension<IAppleExtensions>()
               .RestoreTransactions(isSuccess =>
               {
                   OnRestoreTransactionsFinished?.Invoke(isSuccess);
                   Debug.Log($"IAP Restore: {(isSuccess ? "OK" : "BAD")}.");
               });
#else
            Debug.Log("IAP Restore: not supported platform.");
#endif

        }

      #endregion IPurchaseExternalSystemHandler

      #region IStoreListener

      void IStoreListener.OnInitializeFailed(InitializationFailureReason reason)
      {
         OnInitializationFailed?.Invoke(
            new OnInitializationFailedEventArgs((MalenkiyApps.Interfaces.InitializationFailureReason)reason));
      }

      PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs e)
      {
         OnPurchaseOccured?.Invoke(new OnPurchaseOccuredEventArgs(e.purchasedProduct.ToPurchaseProduct()));
         return PurchaseProcessingResult.Complete;
      }

      void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
      {
         OnPurchaseFailed?.Invoke(new OnPurchaseFailedEventArgs(product.ToPurchaseProduct(),
            (MalenkiyApps.Interfaces.PurchaseFailureReason)purchaseFailureReason));
      }

      void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
      {
         _storeController = controller;
         _extensionProvider = extensions;
         OnInitializationOccured?.Invoke(
            new OnInitializationOccuredEventArgs(controller.products.all.Select(o => o.ToPurchaseProduct())));
      }

      #endregion IStoreListener
   }
}