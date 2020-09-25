using System;
using System.Collections;
using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class PurchasingScreenBlocker : MonoBehaviour
   {
      [SerializeField]
      public GameObject _screenBlocker = default;

      [SerializeField]
      private LoadingStepperBehaviour _loadingStepperBehaviour = default;

      private void Awake()
      {
         Validation.Run(() => _screenBlocker != null, nameof(_screenBlocker), "Must not be null.");
         Validation.Run(() => _loadingStepperBehaviour != null, nameof(_loadingStepperBehaviour), "Must not be null.");
      }

      private void Start()
      {
         EnableScreenBlocker(false);
      }

      private void OnEnable()
      {
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseInitiated +=
            PurchaseExternalSystemHandler_OnRestoreOrPurchaseInitiated;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnRestoreTransactionsInitiated +=
            PurchaseExternalSystemHandler_OnRestoreOrPurchaseInitiated;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseOccured +=
            PurchaseExternalSystemHandler_OnPurchaseOccured;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseFailed +=
            PurchaseExternalSystemHandler_OnPurchaseFailed;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnRestoreTransactionsFinished +=
            PurchaseExternalSystemHandler_OnRestoreTransactionsFinished;
      }

      private void OnDisable()
      {
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseInitiated -=
            PurchaseExternalSystemHandler_OnRestoreOrPurchaseInitiated;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnRestoreTransactionsInitiated -=
            PurchaseExternalSystemHandler_OnRestoreOrPurchaseInitiated;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseOccured -=
            PurchaseExternalSystemHandler_OnPurchaseOccured;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnPurchaseFailed -=
            PurchaseExternalSystemHandler_OnPurchaseFailed;
         GameManager.Instance.PurchaseManager.PurchaseExternalSystemHandler.OnRestoreTransactionsFinished -=
            PurchaseExternalSystemHandler_OnRestoreTransactionsFinished;
      }

      private void PurchaseExternalSystemHandler_OnRestoreTransactionsFinished(bool isSuccess)
      {
         EnableScreenBlocker(false);
      }

      private void PurchaseExternalSystemHandler_OnPurchaseFailed(MalenkiyApps.Interfaces.OnPurchaseFailedEventArgs args)
      {
         EnableScreenBlocker(false);
      }

      private void PurchaseExternalSystemHandler_OnPurchaseOccured(MalenkiyApps.Interfaces.OnPurchaseOccuredEventArgs args)
      {
         EnableScreenBlocker(false);
      }

      private void PurchaseExternalSystemHandler_OnRestoreOrPurchaseInitiated()
      {
         EnableScreenBlocker(true);
      }

      private void EnableScreenBlocker(bool isEnabled)
      {
         Debug.Log($"Purchase Screen Blocker is: {(isEnabled ? "enabled" : "disabled")}");

         _screenBlocker.SetActive(isEnabled);
         _loadingStepperBehaviour.gameObject.SetActive(isEnabled);
      }
   }
}