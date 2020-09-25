namespace AnimalsWorld
{
   public interface ILoadingProgressBehaviour
   {
      void Initialize(int stepsCount);

      void SetProgress(int value);
   }
}