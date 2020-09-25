using MalenkiyApps;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class LevelSnapSingleTarget : LevelSnap
   {
      public Image CompleteCharacterImage;

      private float _swapCounter;

      public override void Awake()
      {
         Validation.Run(() => CompleteCharacterImage != null, nameof(CompleteCharacterImage), "Must not be null.");
         base.Awake();
      }

      protected override void OnLevelEnabled()
      {
         _swapCounter = 0;
         base.OnLevelEnabled();
      }

      protected override void OnLevelDisabled()
      {
         CompleteCharacterImage.color = new Color(1, 1, 1, 0);
         base.OnLevelDisabled();
      }

      protected override IEnumerator OnLevelCompleteExtraActivity()
      {
         yield return RevealCompleteCharacterImage();
         yield return new WaitForSeconds(1);
      }

      private IEnumerator RevealCompleteCharacterImage()
      {
         while ((_swapCounter += Time.deltaTime) < 1)
         {
            CompleteCharacterImage.color = new Color
            {
               r = CompleteCharacterImage.color.r,
               g = CompleteCharacterImage.color.g,
               b = CompleteCharacterImage.color.b,
               a = _swapCounter
            };

            yield return null;
         }
      }
   }
}