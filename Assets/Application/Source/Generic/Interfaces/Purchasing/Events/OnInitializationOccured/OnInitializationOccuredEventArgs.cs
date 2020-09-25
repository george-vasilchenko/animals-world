using System;
using System.Collections.Generic;

namespace MalenkiyApps.Interfaces
{
   public class OnInitializationOccuredEventArgs : EventArgs
   {
      public OnInitializationOccuredEventArgs(IEnumerable<IPurchaseProduct> products)
      {
         Products = products;
      }

      public IEnumerable<IPurchaseProduct> Products { get; }
   }
}