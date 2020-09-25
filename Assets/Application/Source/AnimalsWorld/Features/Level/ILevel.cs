namespace AnimalsWorld
{
   public interface ILevel : IElement
   {
      int ChapterIndex { get; }

      int LevelIndex { get; }
   }
}