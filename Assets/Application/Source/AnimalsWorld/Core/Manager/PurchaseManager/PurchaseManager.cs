using System;
using MalenkiyApps.Interfaces;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimalsWorld
{
   public class PurchaseManager : IPurchaseManager
   {
      #region State

      private readonly IPurchaseExternalSystemHandler _externalSystemHandler;
      private readonly IPurchaseValidator _validator;
      private readonly IPurchaseCatalog _catalog;
      private readonly IPurchaseStatusService _statusService;

      private PurchaseManagerStatus _purchaseManagerStatus;

      #endregion State

      public PurchaseManager(IPersistentPreferencesService persistentPreferencesService)
      {
         _purchaseManagerStatus = PurchaseManagerStatus.Unknown;
         _externalSystemHandler = new UnityInAppPurchaseSystemHandler();
         _validator = new UnityInAppPurchaseValidator();

         _catalog = new PurchaseCatalog();
         _statusService = new PurchaseStatusService(persistentPreferencesService);

         _externalSystemHandler.OnInitializationOccured += ExternalSystemHandler_OnInitializationOccured;
         _externalSystemHandler.OnInitializationFailed += ExternalSystemHandler_OnInitializationFailed;
         _externalSystemHandler.OnPurchaseOccured += ExternalSystemHandler_OnPurchaseOccured;
         _externalSystemHandler.OnPurchaseFailed += ExternalSystemHandler_OnPurchaseFailed;
      }

      ~PurchaseManager()
      {
         _externalSystemHandler.OnInitializationOccured -= ExternalSystemHandler_OnInitializationOccured;
         _externalSystemHandler.OnInitializationFailed -= ExternalSystemHandler_OnInitializationFailed;
         _externalSystemHandler.OnPurchaseOccured -= ExternalSystemHandler_OnPurchaseOccured;
         _externalSystemHandler.OnPurchaseFailed -= ExternalSystemHandler_OnPurchaseFailed;
      }

      private void ExternalSystemHandler_OnPurchaseFailed(OnPurchaseFailedEventArgs args)
      {
         var logMessage = $"Purchase failed!\nProduct: {args.Product.Title}\nReason: {args.Reason}.";
         Debug.Log(logMessage);

         string userMessage;

         switch (args.Reason)
         {
            case PurchaseFailureReason.PurchasingUnavailable:
               userMessage = TryGetTranslation("k_purch_no_purch", UserMessages.InAppPurchasesErrorPurchasingUnavailable);
               break;

            case PurchaseFailureReason.ExistingPurchasePending:
               userMessage = TryGetTranslation("k_purch_existing_purch_pend", UserMessages.InAppPurchasesErrorExistingPurchasePending);
               break;

            case PurchaseFailureReason.ProductUnavailable:
               userMessage = TryGetTranslation("k_purch_prod_unavailable", UserMessages.InAppPurchasesErrorProductUnavailable);
               break;

            case PurchaseFailureReason.SignatureInvalid:
               userMessage = TryGetTranslation("k_purch_sign_inv", UserMessages.InAppPurchasesErrorSignatureInvalid);

               break;

            case PurchaseFailureReason.UserCancelled:
               userMessage = TryGetTranslation("k_purch_usr_cancel", UserMessages.InAppPurchasesErrorUserCancelled);
               break;

            case PurchaseFailureReason.PaymentDeclined:
               userMessage = TryGetTranslation("k_purch_payment_decl", UserMessages.InAppPurchasesErrorPaymentDeclined);
               break;

            case PurchaseFailureReason.DuplicateTransaction:
               userMessage = TryGetTranslation("k_purch_dup_trans", UserMessages.InAppPurchasesErrorDuplicateTransaction);
               break;

            case PurchaseFailureReason.Unknown:
               userMessage = TryGetTranslation("k_purch_unknown", UserMessages.InAppPurchasesErrorUnknown);
               break;

            default: throw new ArgumentOutOfRangeException(nameof(args.Reason), args.Reason, null);
         }

         GameManager.Instance.Logger.Log(new InfoMessage(userMessage));
      }

      private void ExternalSystemHandler_OnPurchaseOccured(OnPurchaseOccuredEventArgs args)
      {
#if (UNITY_IOS || UNITY_EDITOR_OSX) && !UNITY_EDITOR
         if (!IsUserTriggered())
         {
            Debug.Log("Blocked 'on purchase handler'. Was not triggered by user.");
            return;
         }
#endif

         Debug.Log($"Purchase occured.\nProduct: {args.Product.Title}.");

         TryOpenProductForDelivery(args.Product);
      }

      private static bool IsUserTriggered()
      {
         return SceneManager.GetActiveScene().name == "Main";
      }

      private void ExternalSystemHandler_OnInitializationFailed(OnInitializationFailedEventArgs args)
      {
         Debug.Log($"Purchasing failed to initialize, error: {args.Reason}.");
         _purchaseManagerStatus = PurchaseManagerStatus.Error;
      }

      private void ExternalSystemHandler_OnInitializationOccured(OnInitializationOccuredEventArgs args)
      {
         Debug.Log("Purchasing has been successfully initialized.");
         _purchaseManagerStatus = PurchaseManagerStatus.Initialized;

         if (!args.Products.Any())
         {
            Debug.Log("No products exist for some reason.");
         }

         ////if (args.Products.Any())
         ////{
         ////   foreach (var product in args.Products)
         ////   {
         ////      TryOpenProductForDelivery(product);
         ////   }
         ////}
         ////else
         ////{
         ////   Debug.Log("No products exist for some reason.");
         ////}
      }

      private bool TryOpenProductForDelivery(IPurchaseProduct product)
      {
         Debug.Log($"Trying to open product: {product.Title} for delivery.");

         var hasReceipt = !string.IsNullOrWhiteSpace(product.Receipt);

         //#if UNITY_EDITOR
         //         hasReceipt = true;
         //#endif

         if (!hasReceipt)
         {
            _statusService.AddToRegistry(product);
            Debug.Log($"Product: {product.Id} is added to registry. It has no receipt yet.");
            return false;
         }

         Debug.Log($"Receipt Content: {product.Receipt}");

         var isReceiptValid = _validator.IsReceiptValid(product.Receipt);

         //#if UNITY_EDITOR
         //         isReceiptValid = true;
         //#endif

         if (!isReceiptValid)
         {
            Debug.Log($"Product: {product.Id} has invalid receipt.");
            return false;
         }

         Debug.Log($"Product: {product.Id} is set open for delivery.");
         _statusService.SetOpenForDelivery(product);
         return true;
      }

      private string TryGetTranslation(string localizationKey, string fallBackTranslation)
      {
         return GameManager.Instance.LocalizationManager != null
         ? GameManager.Instance.LocalizationManager.GetTranslation(localizationKey)
         : fallBackTranslation;
      }

      #region IPurchaseManager

      PurchaseManagerStatus IPurchaseManager.PurchaseManagerStatus => _purchaseManagerStatus;

      IPurchaseExternalSystemHandler IPurchaseManager.PurchaseExternalSystemHandler => _externalSystemHandler;

      IPurchaseCatalog IPurchaseManager.PurchaseCatalog => _catalog;

      IPurchaseStatusService IPurchaseManager.StatusService => _statusService;

      void IPurchaseManager.Purchase(IPurchaseProduct product)
         => _externalSystemHandler.Purchase(product);

      void IPurchaseManager.Purchase(string productId)
         => _externalSystemHandler.Purchase(productId);

      void IPurchaseManager.RestoreTransactions()
      {
         _externalSystemHandler.RestoreTransactions();
      }

      #endregion IPurchaseManager

      #region IPreloadable

      IEnumerator IPreloadable.PreloadRoutine()
      {
         _externalSystemHandler.Configure(_catalog.PurchasingProducts);

         while (_purchaseManagerStatus == PurchaseManagerStatus.Unknown)
         {
            yield return new WaitForSeconds(1);
         }
      }

      #endregion IPreloadable
   }
}