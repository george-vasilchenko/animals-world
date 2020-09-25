using System;
using System.Collections;
using System.IO;
using MalenkiyApps;
using MalenkiyApps.Interfaces;
using UnityEngine;

namespace AnimalsWorld
{
   public class ResourceManager : MonoBehaviour, IResourceManager
   {
      PreloadedResource<string> IResourceManager.LocalizationPreloadedResource => _csvTextPreloadedResource;

      private PreloadedResource<string> _csvTextPreloadedResource;

      IEnumerator IPreloadable.PreloadRoutine()
      {
         yield return LoadCsvText();
      }

      private IEnumerator LoadCsvText()
      {
         var folderPath = LocalizationConstants.GetLocalizationAssetsPath();
         var filePath = Path.Combine(folderPath, LocalizationConstants.LocalizationCsvFileName);

         Debug.Log($"CSV file path: {filePath}");

         var fileRoutine = Routine<string>.Create(FileHandler.GetStreamingAssetContentTextRoutine(filePath));

         yield return fileRoutine.Invoke();

         if (fileRoutine.Exception != null)
         {
            throw fileRoutine.Exception;
         }

         if (string.IsNullOrWhiteSpace(fileRoutine.Result))
         {
            throw new Exception("Couldn't get streaming asset content text.");
         }

         _csvTextPreloadedResource = new CsvTextPreloadedResource(fileRoutine.Result);
      }
   }
}