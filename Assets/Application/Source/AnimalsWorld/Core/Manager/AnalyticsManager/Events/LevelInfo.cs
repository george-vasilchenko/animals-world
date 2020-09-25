using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class LevelInfo : ILevelInfo
   {
      public int ChapterIndex { get; private set; }

      public int LevelIndex { get; private set; }

      public LevelInfo(int chapterIndex, int levelIndex)
      {
         ChapterIndex = chapterIndex;
         LevelIndex = levelIndex;
      }
   }
}