using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MalenkiyApps
{
   [Serializable]
   public class LocalizationCollection : ILocalizationCollection
   {
      public List<LocalizationColumn> Columns;

      public LocalizationCollection(List<LocalizationColumn> columns)
      {
         Columns = columns;
      }

      bool ILocalizationCollection.ContainsMinimalSetOfLanguages(string[] languages)
      {
         if (languages == null || languages.Length == 0)
         {
            throw new ArgumentException("At least one language has to be supplied.", nameof(languages));
         }

         var resultCount = Columns.Count(o => languages.Any(l => l == o.Title));

         return resultCount == languages.Length;
      }

      string ILocalizationCollection.GetTranslation(string key, string language)
      {
         if (Columns.Count <= 1)
         {
            throw new Exception("There are no localization columns available.");
         }

         if (Columns[0].Entries.Count == 0)
         {
            throw new Exception("There are no entries in the localization columns.");
         }

         var index = -1;

         for (var i = 0; i < Columns[0].Entries.Count; i++)
         {
            if (Columns[0].Entries[i] == key)
            {
               index = i;
            }
         }

         if (index == -1)
         {
            throw new Exception($"Key {key} was not found.");
         }

         var valuesColumn = default(LocalizationColumn);

         for (var i = 1; i < Columns.Count; i++)
         {
            if (Columns[i].Title == language)
            {
               valuesColumn = Columns[i];
               break;
            }
         }

         if (valuesColumn == null)
         {
            throw new Exception($"Localization column with language {language} was not found.");
         }

         return valuesColumn.Entries[index];
      }

      IEnumerable<string> ILocalizationCollection.GetAvailableLanguages()
      {
         if (Columns.Count <= 1)
         {
            throw new Exception("There are no localization columns available.");
         }

         var titles = new List<string>();

         foreach (var column in Columns)
         {
            if (column.Title != LocalizationConstants.LocalizationKeyColumnName)
            {
               titles.Add(column.Title);
            }
         }

         return titles;
      }

      public bool AddLocalizationColumn(string title)
      {
         LocalizationColumn column;

         if (Columns.Count > 1)
         {
            if (Columns.Any(o => o.Title == title))
            {
               Debug.LogFormat("Localization column with title {0} already exists.", title);
               return false;
            }

            column = new LocalizationColumn(title);

            for (var i = 0; i < Columns[0].Entries.Count; i++)
            {
               column.Entries.Add(LocalizationConstants.LocalizationCsvEmptyValuePlaceholder);
            }

            Columns.Add(column);
            return true;
         }

         column = new LocalizationColumn(title);
         Columns.Add(column);
         return true;
      }

      public bool RemoveLocalizationColumn(string title)
      {
         if (Columns.Count == 0)
         {
            Debug.Log("There are no columns present.");
            return false;
         }

         if (Columns.Count == 1)
         {
            Debug.Log("Cannot remove the last column.");
            return false;
         }

         var column = Columns.SingleOrDefault(o => o.Title == title);

         if (column == null)
         {
            throw new Exception($"Cannot remove column. Column with title {title} was not found.");
         }

         Columns.Remove(column);
         return true;
      }

      public bool AddLocalizationEntry(string key)
      {
         if (Columns.Count == 0)
         {
            Debug.Log("There are no columns in the collection.");
            return false;
         }

         if (string.IsNullOrWhiteSpace(key) || Columns[0].Entries.Contains(key))
         {
            Debug.Log("Key must be a unique string.");
            return false;
         }

         Columns[0].Entries.Add(key);

         if (Columns.Count > 1)
         {
            for (var i = 1; i < Columns.Count; i++)
            {
               Columns[i].Entries.Add(LocalizationConstants.LocalizationCsvEmptyValuePlaceholder);
            }
         }

         return true;
      }

      public bool RemoveLocalizationEntry(string key)
      {
         if (Columns.Count == 0)
         {
            Debug.Log("There are no columns in the collection.");
            return false;
         }

         if (string.IsNullOrWhiteSpace(key))
         {
            Debug.Log("Key must not be null or white space.");
            return false;
         }

         if (!Columns[0].Entries.Contains(key))
         {
            Debug.Log("Key must be a present in the collection.");
            return false;
         }

         if (Columns[0].Entries.Count(o => o == key) > 1)
         {
            Debug.Log("There are duplicates in the keys collection.");
            return false;
         }

         var index = Columns[0].Entries.IndexOf(key);

         for (var i = 0; i < Columns.Count; i++)
         {
            Columns[i].Entries.RemoveAt(index);
         }

         return true;
      }

      public string FlattenLocalizationColumnsToText()
      {
         if (Columns.Count <= 0)
         {
            return null;
         }

         var builder = new StringBuilder();
         var columnsCount = Columns.Count;
         var rowsCount = Columns[0].Entries.Count;

         for (var row = -1; row < rowsCount; row++)
         {
            var rowText = "";

            for (var col = 0; col < columnsCount; col++)
            {
               var text = row == -1 ? Columns[col].Title : Columns[col].Entries[row];
               var separatorCharacter = col != columnsCount - 1 ? ";" : "";
               rowText += $"{text}{separatorCharacter}";
            }

            builder.AppendLine(rowText);
         }

         return builder.ToString();
      }
   }
}