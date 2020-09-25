using MalenkiyApps;
using System;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class LocalizationManager : MonoBehaviour, ILocalizationManager
   {
      public SystemLanguage ChosenLanguage;
      public bool UseSystemLanguage;
      public string[] AvailableTranslationLanguages;

      private ILocalizationCollection _localizationCollection;

      public void Awake() => ApplyLocalization();

      private void ApplyLocalization()
      {
         _localizationCollection = LoadLocalizationCollection();
         _localizationCollection.ContainsMinimalSetOfLanguages(new[] { "English" });

         AvailableTranslationLanguages = _localizationCollection.GetAvailableLanguages().ToArray();

         if (UseSystemLanguage)
         {
            var systemLanguageName = Enum.GetName(typeof(SystemLanguage), Application.systemLanguage);

            ChosenLanguage = AvailableTranslationLanguages.Contains(systemLanguageName)
               ? Application.systemLanguage
               : SystemLanguage.English;
         }
         else
         {
            var chosenLanguageName = Enum.GetName(typeof(SystemLanguage), ChosenLanguage);

            ChosenLanguage = AvailableTranslationLanguages.Contains(chosenLanguageName)
               ? ChosenLanguage
               : SystemLanguage.English;
         }
      }

      private static ILocalizationCollection LoadLocalizationCollection()
      {
         var csvHandler = new LocalizationCsvHandler();
         var csvText = GameManager.Instance.ResourceManager.LocalizationPreloadedResource.Resource;

         if (string.IsNullOrWhiteSpace(csvText))
         {
            Debug.LogError("Csv text is not available.");
         }

         return csvHandler.CreateLocalizationCollectionFromCsvText(csvText);
      }

      string ILocalizationManager.GetTranslation(string key)
      {
         if (ChosenLanguage == SystemLanguage.Unknown)
         {
            ChosenLanguage = SystemLanguage.English;
         }

         var language = Enum.GetName(typeof(SystemLanguage), ChosenLanguage);

         return _localizationCollection.GetTranslation(key, language);
      }
   }
}