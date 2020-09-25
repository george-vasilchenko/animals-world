using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MalenkiyApps
{
   public class CliParser
   {
      public static void ProcessArguments<T>(string[] args, Action<T> callback)
       where T : class, new()
      {
         if (!args.Any())
         {
            throw new ArgumentException("Arguments collection is empty", nameof(args));
         }

         var targetObject = new T();
         var targetObjectType = typeof(T);
         var targetObjectProperties = targetObjectType.GetProperties(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

         if (!targetObjectProperties.Any())
         {
            throw new Exception("Object has no suitable properties.");
         }

         var keyValuePairsArguments = ParseArguments(args);

         foreach (var property in targetObjectProperties)
         {
            var propertyAttributes = property.GetCustomAttributes(true);

            if (!propertyAttributes.Any())
            {
               continue;
            }

            foreach (var attribute in propertyAttributes)
            {
               if (attribute is CliOptionAttribute cliAttribute)
               {
                  var key = string.Concat('-', cliAttribute.Key);

                  if (keyValuePairsArguments.ContainsKey(key))
                  {
                     if (keyValuePairsArguments.TryGetValue(key, out var value))
                     {
                        property.SetValue(targetObject, value, null);
                     }
                  }
               }
            }
         }

         callback.Invoke(targetObject);
      }

      public static T ProcessArguments<T>(string[] args)
         where T : class, new()
      {
         if (!args.Any())
         {
            throw new ArgumentException("Arguments collection is empty", nameof(args));
         }

         var targetObject = new T();
         var targetObjectType = typeof(T);
         var targetObjectProperties = targetObjectType.GetProperties(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

         if (!targetObjectProperties.Any())
         {
            throw new Exception("Object has no suitable properties.");
         }

         var keyValuePairsArguments = ParseArguments(args);

         foreach (var property in targetObjectProperties)
         {
            var propertyAttributes = property.GetCustomAttributes(true);

            if (!propertyAttributes.Any())
            {
               continue;
            }

            foreach (var attribute in propertyAttributes)
            {
               if (attribute is CliOptionAttribute cliAttribute)
               {
                  var key = string.Concat('-', cliAttribute.Key);

                  if (keyValuePairsArguments.ContainsKey(key))
                  {
                     if (keyValuePairsArguments.TryGetValue(key, out var value))
                     {
                        property.SetValue(targetObject, value, null);
                     }
                  }
               }
            }
         }

         return targetObject;
      }

      private static Dictionary<string, string> ParseArguments(IList<string> args)
      {
         var results = new Dictionary<string, string>();

         for (var i = 0; i < args.Count; i++)
         {
            if (!args[i].StartsWith("-"))
            {
               continue;
            }

            var current = args[i];
            var key = current;
            var value = "";
            var next = i < args.Count - 1 ? args[i + 1] : null;

            if (next != null)
            {
               if (!next.StartsWith("-"))
               {
                  value = next;
               }
            }

            results.Add(key, value);
         }

         return results;
      }
   }
}