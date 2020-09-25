using UnityEngine;

namespace AnimalsWorld
{
   public class MobilePerformanceUtility : MonoBehaviour
   {
      public void Awake() => Application.targetFrameRate = 60;
   }
}