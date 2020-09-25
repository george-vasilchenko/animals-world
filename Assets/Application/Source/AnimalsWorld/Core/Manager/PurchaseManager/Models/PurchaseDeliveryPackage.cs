using System;
using UnityEngine.Events;

namespace AnimalsWorld
{
   [Serializable]
   public struct PurchaseDeliveryPackage
   {
      public string ProductId;
      public UnityEvent OnDeliveredEvent;
   }
}