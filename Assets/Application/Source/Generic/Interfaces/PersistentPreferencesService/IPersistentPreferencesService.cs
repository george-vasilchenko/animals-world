namespace MalenkiyApps.Interfaces
{
   public interface IPersistentPreferencesService
   {
      int GetIntEntry(string key);

      int SetIntEntry(string key, int data);

      float GetFloatEntry(string key);

      float SetFloatEntry(string key, float data);

      string GetStringEntry(string key);

      string SetStringEntry(string key, string data);

      bool HasKey(string key);

      void Save();
   }
}