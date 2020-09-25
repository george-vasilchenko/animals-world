using System;
using System.Linq;

namespace AnimalsWorld
{
   public class RestoreInvokerService : IRestoreInvokerService
   {
      public void InvokeRestoreTransactions()
      {
         GameManager.Instance.SoundManager.PlayClickSound();

         if (!GameManager.Instance.SystemManager.IsConnectedToInternet)
         {
            ShowNoInternetMessage();
            return;
         }

         var product = GameManager.Instance.PurchaseManager.PurchaseCatalog.PurchasingProducts.First();
         if (GameManager.Instance.PurchaseManager.StatusService.IsOpenForDelivery(product))
         {
            ShowAlreadyPurchasedMessage();
            return;
         }

         OpenParentsPanelForChallenge();
      }

      private static void OpenParentsPanelForChallenge()
      {
         GameManager.Instance.PanelManager.SwitchToPanelAndApply(GameManager.Instance.PanelManager.ParentPanel, TransactionType.Restore);
      }

      private static void ShowNoInternetMessage()
      {
         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_purch_no_purch");
         GameManager.Instance.Logger.Log(new InfoMessage(message));
      }

      private static void ShowAlreadyPurchasedMessage()
      {
         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_purch_already_done");
         GameManager.Instance.Logger.Log(new InfoMessage(message));
      }
   }
}