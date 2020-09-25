using System;

namespace MalenkiyApps.Interfaces
{
   public class OnPurchaseOccuredEventArgs : EventArgs
   {
      public OnPurchaseOccuredEventArgs(IPurchaseProduct product)
      {
         Product = product;
      }

      public IPurchaseProduct Product { get; }
   }
}