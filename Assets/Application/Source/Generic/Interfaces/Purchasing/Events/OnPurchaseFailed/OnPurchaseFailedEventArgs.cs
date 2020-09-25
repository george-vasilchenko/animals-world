using System;

namespace MalenkiyApps.Interfaces
{
   public class OnPurchaseFailedEventArgs : EventArgs
   {
      public IPurchaseProduct Product { get; }

      public PurchaseFailureReason Reason { get; }

      public OnPurchaseFailedEventArgs(IPurchaseProduct product, PurchaseFailureReason reason)
      {
         Product = product;
         Reason = reason;
      }
   }
}