using System.Collections.Generic;

namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseCatalog
   {
      IReadOnlyCollection<IPurchaseProduct> PurchasingProducts { get; }
   }
}