using MalenkiyApps.Interfaces;
using System;

namespace AnimalsWorld
{
   public class PurchaseProduct : IPurchaseProduct
   {
      private readonly string _id;
      private readonly string _title;
      private readonly PurchaseProductType _productType;
      private readonly string _receipt;

      internal PurchaseProduct(string id, string title, PurchaseProductType productType, string receipt)
      {
         if (string.IsNullOrWhiteSpace(id))
         {
            throw new ArgumentException("Id must not be null or white space.");
         }

         if (string.IsNullOrWhiteSpace(title))
         {
            throw new ArgumentException("Title must not be null or white space.");
         }

         _id = id;
         _title = title;
         _productType = productType;
         _receipt = receipt;
      }

      public PurchaseProduct(IPurchaseProduct product)
      : this(product.Id, product.Title, product.ProductType, product.Receipt)
      {
      }

      #region Implementation of IPurchaseProduct

      string IPurchaseProduct.Id => _id;

      string IPurchaseProduct.Title => _title;

      PurchaseProductType IPurchaseProduct.ProductType => _productType;

      string IPurchaseProduct.Receipt => _receipt;

      #endregion Implementation of IPurchaseProduct
   }
}