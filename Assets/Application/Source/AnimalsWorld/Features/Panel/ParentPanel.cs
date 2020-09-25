using System;
using System.Collections;

namespace AnimalsWorld
{
   public class ParentPanel : MiniActivityPanel
   {
      public void OnDisable()
      {
         StopCoroutine(RunActivity());

         if (CurrentActivity != null)
         {
            CurrentActivity.Cleanup();
            CurrentActivity = null;
         }
      }

      public override void ApplyData(object data)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerator RunActivity()
      {
         CurrentActivity = GetRandomActivity();

         var challengeRoutine = CurrentActivity.RunActivity();
         var resultObject = default(object);

         while (challengeRoutine.MoveNext())
         {
            resultObject = challengeRoutine.Current;
            yield return null;
         }

         var isChallengeSolved = Convert.ToBoolean(resultObject);

         if (isChallengeSolved)
         {
            GameManager.Instance.PanelManager.SwitchToPanel(GameManager.Instance.PanelManager.PurchasingPanel);
         }
         else
         {
            var badMessage = GameManager.Instance.LocalizationManager.GetTranslation("k_parent_bad");
            GameManager.Instance.Logger.Log(new InfoMessage(badMessage));
            GameManager.Instance.PanelManager.SwitchToPanel(GameManager.Instance.PanelManager.StartPanel);
         }
      }

      //private void ProcessChallengeResult(TransactionType transactionType, bool result)
      //{
      //   void OnCloseCallback()
      //   {
      //      Debug.Log("IAP dialog was closed.");
      //   }

      // if (result) { string message; Action onConfirmAction; switch (transactionType) { case
      // TransactionType.Purchase: message =
      // GameManager.Instance.LocalizationManager.GetTranslation("k_iap_confirm"); onConfirmAction =
      // () => { Debug.Log("Purchase was confirmed.");
      // GameManager.Instance.PurchaseManager.Purchase(PurchaseCatalog.UnlockAllBoardsProductId); }; break;

      // case TransactionType.Restore: message =
      // GameManager.Instance.LocalizationManager.GetTranslation("k_iap_restore_confirm");
      // onConfirmAction = () => { Debug.Log("Restore was confirmed.");
      // GameManager.Instance.PurchaseManager.RestoreTransactions(); }; break;

      // default: throw new ArgumentOutOfRangeException(); }

      //      GameManager.Instance.ConfirmationDialogService.Open(message, onConfirmAction, OnCloseCallback);
      //   }
      //   else
      //   {
      //      var badMessage = GameManager.Instance.LocalizationManager.GetTranslation("k_parent_bad");
      //      GameManager.Instance.Logger.Log(new InfoMessage(badMessage));
      //      Debug.Log(badMessage);
      //   }
      //}

      private void OnEnable() => StartCoroutine(RunActivity());
   }
}