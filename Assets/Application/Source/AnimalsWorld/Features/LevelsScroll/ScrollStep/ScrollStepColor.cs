using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Image))]
   public class ScrollStepColor : ScrollStep
   {
      public Color TargetColor;
      public Image Image;

      private float _diff;

      public void Awake() => Image = GetComponent<Image>();

      public void Update() => UpdateState();

      public override void OnPointerClick(PointerEventData eventData) => ClickCallback?.Invoke(Index);

      public override void SetEnabled(bool isEnabled, bool forceInstant = false)
      {
         IsEnabled = isEnabled;
         IsDone = false;

         if (forceInstant)
         {
            TargetColor.a = IsEnabled ? 1.0f : 0.25f;
            Image.color = TargetColor;
         }
      }

      public override void UpdateState()
      {
         if (IsDone)
         {
            return;
         }

         TargetColor.a = Mathf.Lerp(TargetColor.a, IsEnabled ? 1.0f : 0.25f, Time.deltaTime * 5);
         _diff = Image.color.a - TargetColor.a;
         Image.color = TargetColor;

         IsDone = Mathf.Abs(_diff) < 0.001f;
      }
   }
}