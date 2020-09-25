using System.Collections.Generic;

namespace AnimalsWorld
{
   public interface IMiniActivity
   {
      string Name { get; }

      IEnumerator<object> RunActivity();

      void ForceStop();

      bool IsStopCondition();

      void OnActivityEnabled();

      void OnActivityDisabled();

      void Cleanup();
   }
}