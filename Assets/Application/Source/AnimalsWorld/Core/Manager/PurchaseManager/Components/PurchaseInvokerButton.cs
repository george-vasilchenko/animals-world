using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Button))]
   public class PurchaseInvokerButton : MonoBehaviour
   {
      private Button _buttonComponent = default;
      private IPurchaseInvokerService _purchaseInvokerService;

      private void Awake()
      {
         _purchaseInvokerService = new PurchaseInvokerService();
         _buttonComponent = GetComponent<Button>();
         _buttonComponent.onClick = new Button.ButtonClickedEvent();

         Validation.Run(() => _buttonComponent != null, nameof(_buttonComponent), "Must not be null.");
      }

      private void OnEnable()
      {
         _buttonComponent.onClick.AddListener(_purchaseInvokerService.InvokeUnlockAllBoards);
      }

      private void OnDisable()
      {
         _buttonComponent.onClick.RemoveAllListeners();
      }
   }
}