using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MalenkiyApps
{
   public class LocalizationCsvHandler
   {
      public LocalizationCollection CreateLocalizationCollectionFromCsvText(string text, char separatorCharacter = ';')
      {
         var textRows = Regex.Split(text, "\r\n|\r|\n");
         return ConvertTextLinesToLocalizationCollection(textRows, separatorCharacter);
      }

      public LocalizationCollection CreateLocaizationCollectionFromCsvFile(string path, char separatorCharacter = ';')
      {
         var textRows = FileHandler.ReadTextLines(path);
         return ConvertTextLinesToLocalizationCollection(textRows, separatorCharacter);
      }

      private static LocalizationCollection ConvertTextLinesToLocalizationCollection(IReadOnlyList<string> textRows, char separatorCharacter)
      {
         if (textRows == null || textRows.Count == 0)
         {
            Debug.Log("File doesn't exist or contains no text rows.");
            return null;
         }

         var columns = new List<LocalizationColumn>();
         var firstLineWords = textRows[0].Split(new[] { separatorCharacter }, StringSplitOptions.RemoveEmptyEntries);
         var columnsCount = firstLineWords.Length;

         for (var i = 0; i < columnsCount; i++)
         {
            columns.Add(new LocalizationColumn(firstLineWords[i]));
         }

         for (var i = 1; i < textRows.Count; i++)
         {
            var line = textRows[i];
            var lineWords = line.Split(new[] { separatorCharacter }, StringSplitOptions.RemoveEmptyEntries);

            for (var j = 0; j < lineWords.Length; j++)
            {
               var word = lineWords[j];
               columns[j].Entries.Add(word);
            }
         }

         return new LocalizationCollection(columns);
      }

      public void CreateOrUpdateCsvFromLocalizationCollection(LocalizationCollection collection, string path, char separatorCharacter = ';')
      {
         var text = collection.FlattenLocalizationColumnsToText();

         if (string.IsNullOrWhiteSpace(text))
         {
            throw new Exception("Localization collection is empty.");
         }

         FileHandler.WriteTextToFile(text, path);
      }
   }
}