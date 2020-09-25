using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MalenkiyApps
{
   public class LocalizationCsvCreator : ScriptableObject
   {
      public LocalizationCollection LocalizationCollection;

      public void Load()
      {
         var folderPath = LocalizationConstants.GetLocalizationAssetsPath();
         var filePath = Path.Combine(folderPath, LocalizationConstants.LocalizationCsvFileName);
         var csvHandler = new LocalizationCsvHandler();

         LocalizationCollection = csvHandler.CreateLocaizationCollectionFromCsvFile(filePath);
      }

      public void Save()
      {
         var folderPath = LocalizationConstants.GetLocalizationAssetsPath();
         var filePath = Path.Combine(folderPath, LocalizationConstants.LocalizationCsvFileName);
         var csvHandler = new LocalizationCsvHandler();
         csvHandler.CreateOrUpdateCsvFromLocalizationCollection(LocalizationCollection, filePath);
      }

      public bool CreateCollection()
      {
         var folderPath = LocalizationConstants.GetLocalizationAssetsPath();
         var filePath = Path.Combine(folderPath, LocalizationConstants.LocalizationCsvFileName);
         var csvHandler = new LocalizationCsvHandler();

         var keysColumn = new LocalizationColumn(LocalizationConstants.LocalizationKeyColumnName);
         LocalizationCollection = new LocalizationCollection(new List<LocalizationColumn> { keysColumn });
         csvHandler.CreateOrUpdateCsvFromLocalizationCollection(LocalizationCollection, filePath);

         return true;
      }

      public bool AddLanguage(string title)
      {
         var result = LocalizationCollection.AddLocalizationColumn(title);

         if (result)
         {
            Save();
         }

         return result;
      }

      public bool RemoveLanguage(string title)
      {
         var result = LocalizationCollection.RemoveLocalizationColumn(title);
         if (result)
         {
            Save();
         }

         return result;
      }

      public bool AddEntry(string key)
      {
         var result = LocalizationCollection.AddLocalizationEntry(key);
         if (result)
         {
            Save();
         }

         return result;
      }

      public bool RemoveEntry(string key)
      {
         var result = LocalizationCollection.RemoveLocalizationEntry(key);
         if (result)
         {
            Save();
         }

         return result;
      }
   }
}