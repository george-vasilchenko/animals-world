using System;
using System.Collections.Generic;

namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseExternalSystemHandler
   {
      event OnInitializationOccuredDelegate OnInitializationOccured;

      event OnInitializationFailedDelegate OnInitializationFailed;

      event OnPurchaseInitiatedDelegate OnPurchaseInitiated;

      event OnRestoreTransactionsInitiatedDelegate OnRestoreTransactionsInitiated;

      event OnRestoreTransactionsFinishedDelegate OnRestoreTransactionsFinished;

      event OnPurchaseOccuredDelegate OnPurchaseOccured;

      event OnPurchaseFailedDelegate OnPurchaseFailed;

      void Purchase(IPurchaseProduct product);

      void Purchase(string productId);

      void Configure(IEnumerable<IPurchaseProduct> productsToConfigure);

      void RestoreTransactions();
   }
}