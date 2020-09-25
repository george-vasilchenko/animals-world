using System.Collections;
using UnityEngine.Networking;
using static System.IO.File;

namespace MalenkiyApps
{
   public static class FileHandler
   {
      public static string[] ReadTextLines(string path) => !Exists(path) ? null : ReadAllLines(path);

      public static void WriteTextToFile(string text, string path) => WriteAllText(path, text);

      public static IEnumerator GetStreamingAssetContentTextRoutine(string filePath)
      {
#if (UNITY_IOS || UNITY_EDITOR_OSX) && !UNITY_EDITOR_WIN
         yield return System.IO.File.ReadAllText(filePath);
#else
         using (var request = UnityWebRequest.Get(filePath))
         {
            yield return request.SendWebRequest();
            yield return request.downloadHandler.text;
         }
#endif
      }
   }
}