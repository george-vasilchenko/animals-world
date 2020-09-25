using MalenkiyApps;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class MessageBubble : MonoBehaviour
   {
      public Image[] ElementsImagesToFadeOut;
      public Text BubbleText;
      public float Duration = 4f;

      public void Initialize(string message)
      {
         if (BubbleText != null)
         {
            BubbleText.text = message;
         }
      }

      private void Awake()
      {
         Validation.Run(() => BubbleText != null, "BubbleText", "Must not be null.");
         Validation.Run(() => ElementsImagesToFadeOut != null, "ElementsImagesToFadeOut", "Must not be null.");
         Validation.Run(() => ElementsImagesToFadeOut.Length > 0, "ElementsImagesToFadeOut", "Must not be empty.");
      }

      public void DismissLog()
      {
         StopAllCoroutines();
         Destroy(gameObject);
      }

      private void Start()
      {
         StartCoroutine(ShowMessage());
      }

      private IEnumerator ShowMessage()
      {
         yield return new WaitForSeconds(Duration);
         yield return DestroyFadingOut();
      }

      private IEnumerator DestroyFadingOut()
      {
         var alpha = 1f;

         while ((alpha -= Time.deltaTime) > 0)
         {
            foreach (var image in ElementsImagesToFadeOut)
            {
               image.color = new Color
               (
                  image.color.r,
                  image.color.g,
                  image.color.b,
                  alpha
               );
            }

            BubbleText.color = new Color
            (
               BubbleText.color.r,
               BubbleText.color.g,
               BubbleText.color.b,
               alpha
            );

            yield return null;
         }

         Destroy(gameObject);
      }
   }
}