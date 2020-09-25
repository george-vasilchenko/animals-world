using System.Collections.Generic;

namespace AnimalsWorld
{
   public interface ILevelManager
   {
      IEnumerable<ILevel> GetAllLevels();

      ILevel GetLevelAtIndex(int chapterIndex, int levelIndex);

      bool IsLevelUnlocked(ILevel level);

      void UnlockAllLevels();
   }
}