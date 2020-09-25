using System.Collections.Generic;

namespace MalenkiyApps
{
   public interface ILocalizationCollection
   {
      bool ContainsMinimalSetOfLanguages(string[] languages);

      string GetTranslation(string key, string language);

      IEnumerable<string> GetAvailableLanguages();
   }
}