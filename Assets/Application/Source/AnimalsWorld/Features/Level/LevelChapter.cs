using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   [Serializable]
   public class LevelChapter
   {
      public List<Level> Levels;

      public void HideAllLevels()
      {
         if (Levels == null || Levels.Count == 0)
         {
            Debug.LogWarning("Levels collection is null or empty.");
            return;
         }

         foreach (var level in Levels)
         {
            level.Hide();
         }
      }

      public Level LoadLevelByIndex(int index)
      {
         if (Levels == null || Levels.Count == 0)
         {
            Debug.LogWarning("Levels collection is null or empty.");
            return null;
         }

         var level = Levels[index];
         level.Show();

         var otherLevels = Levels.Where(o => o != level).ToList();
         foreach (var otherLevel in otherLevels)
         {
            otherLevel.Hide();
         }

         return level;
      }
   }
}