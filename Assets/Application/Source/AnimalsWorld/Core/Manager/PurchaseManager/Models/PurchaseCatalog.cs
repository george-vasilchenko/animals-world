using MalenkiyApps.Interfaces;
using System.Collections.Generic;

namespace AnimalsWorld
{
   public class PurchaseCatalog : IPurchaseCatalog
   {
      public const string UnlockAllBoardsProductId = "ma_aw_unlock_all_boards_83ab35e310cdaca3741e7f68c01d42c5";

      private readonly List<PurchaseProduct> _purchasingProducts;

      public PurchaseCatalog()
      {
         _purchasingProducts = new List<PurchaseProduct>();
         var unlockAllBoardsProduct = new PurchaseProduct(
            UnlockAllBoardsProductId,
            "Unlock all puzzles",
            PurchaseProductType.NonConsumable, null);

         _purchasingProducts.Add(unlockAllBoardsProduct);
      }

      #region Implementation of IPurchaseCatalog

      IReadOnlyCollection<IPurchaseProduct> IPurchaseCatalog.PurchasingProducts => _purchasingProducts;

      #endregion Implementation of IPurchaseCatalog
   }
}