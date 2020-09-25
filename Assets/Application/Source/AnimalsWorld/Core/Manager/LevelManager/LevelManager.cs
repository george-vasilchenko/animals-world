using MalenkiyApps;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class LevelManager : MonoBehaviour, ILevelManager
   {
      public LevelRegistryEntry[] LevelsChapter1;
      public LevelRegistryEntry[] LevelsChapter2;

      public ILevel GetLevelAtIndex(int chapterIndex, int levelIndex)
      {
         var chapter = GetChapter(chapterIndex);

         foreach (var entry in chapter)
         {
            if (entry.Level.LevelIndex == levelIndex)
            {
               return entry.Level;
            }
         }

         return null;
      }

      IEnumerable<ILevel> ILevelManager.GetAllLevels()
      {
         var index = 0;
         var levels = new ILevel[LevelsChapter1.Length + LevelsChapter2.Length];

         foreach (var level in LevelsChapter1)
         {
            levels[index++] = level.Level;
         }

         foreach (var level in LevelsChapter2)
         {
            levels[index++] = level.Level;
         }

         return levels;
      }

      ILevel ILevelManager.GetLevelAtIndex(int chapterIndex, int levelIndex) => GetLevelAtIndex(chapterIndex, levelIndex);

      bool ILevelManager.IsLevelUnlocked(ILevel level)
      {
         var chapter = GetChapter(level.ChapterIndex);
         var result = false;

         foreach (var entry in chapter)
         {
            if (entry.Level == (Level)level)
            {
               result = entry.IsUnlocked;
               break;
            }
         }

         return result;
      }

      public void UnlockAllLevels()
      {
         Debug.Log("Unlocking all boards.");

         if (LevelsChapter1.Any(o => !o.IsUnlocked))
         {
            foreach (var level in LevelsChapter1)
            {
               level.IsUnlocked = true;
            }
         }

         if (LevelsChapter2.Any(o => !o.IsUnlocked))
         {
            foreach (var level in LevelsChapter2)
            {
               level.IsUnlocked = true;
            }
         }
      }

      public void Awake()
      {
         Validation.Run(() => LevelsChapter1 != null, "LevelsChapter1", "Must not be null.");
         Validation.Run(() => LevelsChapter1[0].Level != null, "LevelsChapter1[0].Level", "Must not be null.");
         Validation.Run(() => LevelsChapter2 != null, "LevelsChapter2", "Must not be null.");
         Validation.Run(() => LevelsChapter2[0].Level != null, "LevelsChapter2[0].Level", "Must not be null.");
         Validation.Run(() => LevelsChapter1.Any(), "LevelsChapter1", "Must not be empty.");
         Validation.Run(() => LevelsChapter2.Any(), "LevelsChapter2", "Must not be empty.");
      }

      private IEnumerable<LevelRegistryEntry> GetChapter(int chapterIndex)
      {
         switch (chapterIndex)
         {
            case 0:
               return LevelsChapter1;

            case 1:
               return LevelsChapter2;

            default:
               throw new ArgumentException("Chapter index is not valid.", nameof(chapterIndex));
         }
      }
   }
}