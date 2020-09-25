using System.Collections;

namespace AnimalsWorld
{
   public interface ILoadingStepperBehaviour
   {
      void Initialize(int stepsCount, float intervalSeconds);

      void SetEnabled(bool isEnabled);
   }
}