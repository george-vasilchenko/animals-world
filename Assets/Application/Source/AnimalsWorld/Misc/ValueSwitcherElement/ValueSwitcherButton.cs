using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(ValueButtonVisualState), typeof(ValueButtonData))]
   public class ValueSwitcherButton : Button
   {
      public ValueButtonVisualState ValueButtonVisualState { get; private set; }

      public ValueButtonData ValueButtonData { get; private set; }

      private new void Awake()
      {
         base.Awake();

         ValueButtonVisualState = GetComponent<ValueButtonVisualState>();
         ValueButtonData = GetComponent<ValueButtonData>();

         Validation.Run(() => ValueButtonVisualState != null, nameof(ValueButtonVisualState), "Must not be null.");
         Validation.Run(() => ValueButtonData != null, nameof(ValueButtonData), "Must not be null.");
      }
   }
}