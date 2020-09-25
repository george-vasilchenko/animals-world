using System;

namespace MalenkiyApps
{
   [AttributeUsage(AttributeTargets.Property)]
   public class CliOptionAttribute : Attribute
   {
      public string Key { get; }

      public CliOptionAttribute(string key)
      {
         Key = key;
      }
   }
}