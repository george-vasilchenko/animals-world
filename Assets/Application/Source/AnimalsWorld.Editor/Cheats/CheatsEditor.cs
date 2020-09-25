using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;

namespace AnimalsWorld
{
   public class CheatsEditor
   {
      [MenuItem("AnimalsWorld/Unlock All Levels")]
      public static void UnlockLevelsForThisRun()
      {
         if (!EditorApplication.isPlaying)
         {
            throw new InvalidOperationException("Must be used only in Play mode");
         }

         var levelManager = Object.FindObjectOfType<LevelManager>();

         if (levelManager == null)
         {
            throw new NullReferenceException("Level Manager is null");
         }

         levelManager.UnlockAllLevels();
      }

      [MenuItem("AnimalsWorld/Start From Beginning")]
      public static void StartFromBeginning()
      {
         EditorSceneManager.OpenScene("Assets/Application/Scenes/Preload.unity");
         EditorApplication.isPlaying = true;
      }
   }
}