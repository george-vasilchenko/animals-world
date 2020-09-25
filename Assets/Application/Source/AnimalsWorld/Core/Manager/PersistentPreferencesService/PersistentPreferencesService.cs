using MalenkiyApps.Interfaces;
using UnityEngine;

namespace AnimalsWorld
{
   public class PersistentPreferencesService : IPersistentPreferencesService
   {
      public int GetIntEntry(string key) => PlayerPrefs.GetInt(key);

      public int SetIntEntry(string key, int data)
      {
         PlayerPrefs.SetInt(key, data);
         return data;
      }

      public float GetFloatEntry(string key) => PlayerPrefs.GetFloat(key);

      public float SetFloatEntry(string key, float data)
      {
         PlayerPrefs.SetFloat(key, data);
         return data;
      }

      public string GetStringEntry(string key) => PlayerPrefs.GetString(key);

      public string SetStringEntry(string key, string data)
      {
         PlayerPrefs.SetString(key, data);
         return data;
      }

      public bool HasKey(string key) => PlayerPrefs.HasKey(key);

      public void Save() => PlayerPrefs.Save();
   }
}