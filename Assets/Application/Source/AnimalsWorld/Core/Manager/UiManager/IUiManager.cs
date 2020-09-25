namespace AnimalsWorld
{
   public interface IUiManager
   {
      void SetInputEnabled(bool enable);

      void DisableInputForDuration(float duration);

      float ScaleFactor { get; }
   }
}