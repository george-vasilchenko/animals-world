using UnityEngine;

namespace MalenkiyApps
{
   public static class LocalizationConstants
   {
      public const string LocalizationKeyColumnName = "Keys";
      public const string LocalizationCsvFileName = "localizations.csv";
      public const string LocalizationCsvEmptyValuePlaceholder = "$null";

      public static string GetLocalizationAssetsPath() => Application.streamingAssetsPath;
   }
}