using UnityEngine;

namespace AnimalsWorld
{
   public abstract class ScrollTriggerBase : MonoBehaviour, IScrollTrigger
   {
      public abstract event OnScrollTriggerDelegate OnTriggered;

      public abstract void OnTargetValueExternallyChanged(int newValue);
   }
}