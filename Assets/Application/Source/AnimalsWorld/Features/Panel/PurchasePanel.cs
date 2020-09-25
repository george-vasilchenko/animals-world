using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class PurchasePanel : Panel
   {
      [SerializeField]
      private Button _restoreButton;

      [SerializeField]
      private Text _restoreText;

      [SerializeField]
      private Button _unlockButton;

      private TransactionType _transactionType;

      private void Awake()
      {
         Validation.Run(() => _restoreButton, nameof(_restoreButton), "Must not be null.");
         Validation.Run(() => _restoreText, nameof(_restoreText), "Must not be null.");
         Validation.Run(() => _unlockButton, nameof(_unlockButton), "Must not be null.");

         _restoreButton.onClick = new Button.ButtonClickedEvent();
         _unlockButton.onClick = new Button.ButtonClickedEvent();

#if !(UNITY_IOS || UNITY_EDITOR_OSX) && !UNITY_EDITOR
         Destroy(_restoreButton.gameObject);
         Destroy(_restoreText.gameObject);
#endif
      }

      private void OnEnable()
      {
         _restoreButton?.onClick.AddListener(OnRestoreClickHandler);
         _unlockButton?.onClick.AddListener(OnUnlockClickHandler);
      }

      private void OnDisable()
      {
         _restoreButton?.onClick.RemoveAllListeners();
         _unlockButton?.onClick.RemoveAllListeners();
      }

      private void OnRestoreClickHandler()
      {
         void RestoreDialogConfirmAction()
         {
            Debug.Log("Restore was confirmed.");

            // check if owned already
            var product = GameManager.Instance.PurchaseManager.PurchaseCatalog.PurchasingProducts.First();
            if (GameManager.Instance.PurchaseManager.StatusService.IsOpenForDelivery(product))
            {
               ShowAlreadyPurchasedMessage();
               return;
            }

            GameManager.Instance.PurchaseManager.RestoreTransactions();
         }

         void RestoreDialogCloseAction()
         {
            Debug.Log("Restore was closed.");
         }

         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_iap_restore_confirm");
         GameManager.Instance.ConfirmationDialogService.Open(message, RestoreDialogConfirmAction, RestoreDialogCloseAction);
         GameManager.Instance.UiManager.DisableInputForDuration(FeaturesConstants.ConfirmationDialogsInputDisabledDurationSeconds);
      }

      private void OnUnlockClickHandler()
      {
         // purchase
         void UnlockDialogConfirmAction()
         {
            Debug.Log("Purchase was confirmed.");

            // check if owned already
            var product = GameManager.Instance.PurchaseManager.PurchaseCatalog.PurchasingProducts.First();
            if (GameManager.Instance.PurchaseManager.StatusService.IsOpenForDelivery(product))
            {
               ShowAlreadyPurchasedMessage();
               return;
            }

            GameManager.Instance.PurchaseManager.Purchase(PurchaseCatalog.UnlockAllBoardsProductId);
         }

         void UnlockDialogCloseAction()
         {
            Debug.Log("Purchase was closed.");
         }

         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_iap_confirm");
         GameManager.Instance.ConfirmationDialogService.Open(message, UnlockDialogConfirmAction, UnlockDialogCloseAction);
         GameManager.Instance.UiManager.DisableInputForDuration(FeaturesConstants.ConfirmationDialogsInputDisabledDurationSeconds);
      }

      public override void ApplyData(object data)
      {
         throw new System.NotImplementedException();
      }

      private static void ShowAlreadyPurchasedMessage()
      {
         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_purch_already_done");
         GameManager.Instance.Logger.Log(new InfoMessage(message));
      }
   }
}