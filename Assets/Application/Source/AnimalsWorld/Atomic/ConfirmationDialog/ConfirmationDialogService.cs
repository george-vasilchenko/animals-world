using System;
using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class ConfirmationDialogService : MonoBehaviour, IConfirmationDialogService
   {
      [SerializeField] private GameObject _backgroundCoverImage = default;
      [SerializeField] private ConfirmationDialog _confirmationDialogPrefab = default;

      private ConfirmationDialog _currentInstance = default;

      private void Awake()
      {
         Validation.Run(() => _confirmationDialogPrefab != null, nameof(_confirmationDialogPrefab),
            "Must not be null.");
         Validation.Run(() => _backgroundCoverImage != null, nameof(_backgroundCoverImage),
            "Must not be null.");

         _backgroundCoverImage.SetActive(false);
      }

      public void Open(string message, Action onConfirmAction, Action onCloseAction)
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(message), nameof(message),
            "Must not be null or white space.");
         Validation.Run(() => onConfirmAction != null, nameof(onConfirmAction),
            "Must not be null.");
         Validation.Run(() => onCloseAction != null, nameof(onCloseAction),
            "Must not be null.");

         if (_currentInstance != null)
         {
            Destroy(_currentInstance);
         }

         _backgroundCoverImage.SetActive(true);
         _currentInstance = Instantiate(_confirmationDialogPrefab);
         _currentInstance.InitializeDialog(message,
            () =>
         {
            onConfirmAction.Invoke();
            _backgroundCoverImage.SetActive(false);
         }, () =>
         {
            onCloseAction.Invoke();
            _backgroundCoverImage.SetActive(false);
         });
         _currentInstance.transform.SetParent(transform, false);
      }
   }
}