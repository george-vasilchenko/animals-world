using System;
using System.Collections.Generic;

namespace MalenkiyApps
{
   [Serializable]
   public class LocalizationColumn
   {
      public string Title;

      public List<string> Entries;

      public LocalizationColumn(string title)
      {
         Title = title;
         Entries = new List<string>();
      }
   }
}