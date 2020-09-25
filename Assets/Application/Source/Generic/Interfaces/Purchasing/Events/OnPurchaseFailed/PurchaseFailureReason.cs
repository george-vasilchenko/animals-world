namespace MalenkiyApps.Interfaces
{
   public enum PurchaseFailureReason
   {
      PurchasingUnavailable,
      ExistingPurchasePending,
      ProductUnavailable,
      SignatureInvalid,
      UserCancelled,
      PaymentDeclined,
      DuplicateTransaction,
      Unknown,
   }
}