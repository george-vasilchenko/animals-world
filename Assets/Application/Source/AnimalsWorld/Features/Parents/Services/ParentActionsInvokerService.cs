using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class ParentActionsInvokerService : IParentActionsInvokerService
   {
      public void Invoke()
      {
         GameManager.Instance.SoundManager.PlayClickSound();

         if (!GameManager.Instance.SystemManager.IsConnectedToInternet)
         {
            ShowNoInternetMessage();
            return;
         }

         if (GameManager.Instance.PurchaseManager.PurchaseManagerStatus == PurchaseManagerStatus.Error)
         {
            ShowIapSystemFailure();
            return;
         }

         OpenParentsPanelForChallenge();
      }

      public static void ShowIapSystemFailure()
      {
         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_purch_error");
         GameManager.Instance.Logger.Log(new InfoMessage(message));
      }

      private static void OpenParentsPanelForChallenge()
      {
         GameManager.Instance.PanelManager.SwitchToPanel(GameManager.Instance.PanelManager.ParentPanel);
      }

      private static void ShowNoInternetMessage()
      {
         var message = GameManager.Instance.LocalizationManager.GetTranslation("k_purch_no_purch");
         GameManager.Instance.Logger.Log(new InfoMessage(message));
      }
   }
}