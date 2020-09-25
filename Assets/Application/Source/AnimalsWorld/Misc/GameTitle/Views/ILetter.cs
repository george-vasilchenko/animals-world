namespace AnimalsWorld
{
   public interface ILetter
   {
      void TriggerAppearEffect();

      void TriggerBubbleEffect();

      void Set(char letter);

      void SetLocation(float xLocation, float yLocation);
   }
}