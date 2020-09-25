using MalenkiyApps.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing.Security;

namespace AnimalsWorld
{
   public class UnityInAppPurchaseValidator : IPurchaseValidator
   {
      public bool IsReceiptValid(string receipt)
      {
         bool validPurchase = true;

#if (UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX)

         var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

         try
         {
            var result = validator.Validate(receipt);

            Debug.Log("Receipt is valid.");

            foreach (var productReceipt in result)
            {
               Debug.LogFormat("Product Id: {0}\r\nPurchase date: {1}\r\nTransaction Id: {2}",
                  productReceipt.productID, productReceipt.purchaseDate, productReceipt.transactionID);
            }
         }
         catch (IAPSecurityException)
         {
            Debug.Log("Invalid receipt.");
            validPurchase = false;
         }
#endif

#if UNITY_EDITOR
         Debug.Log("Invalid receipt, but was set as valid for Editor only.");
         validPurchase = true;
#endif
         return validPurchase;
      }
   }
}