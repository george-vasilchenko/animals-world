namespace MalenkiyApps.Interfaces
{
   public interface IPurchaseValidator
   {
      bool IsReceiptValid(string receipt);
   }
}