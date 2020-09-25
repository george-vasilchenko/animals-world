using MalenkiyApps;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   public abstract class MiniActivity : Element, IMiniActivity
   {
      public string Name => _name;

      [SerializeField]
      private string _name = default;

      public void Awake()
      {
         Validation.Run(() => !string.IsNullOrEmpty(Name), "Name", "Must not be null or empty.");
      }

      public abstract IEnumerator<object> RunActivity();

      public abstract void ForceStop();

      public abstract bool IsStopCondition();

      public abstract void OnActivityEnabled();

      public abstract void OnActivityDisabled();

      public abstract void Cleanup();

      private void OnEnable() => OnActivityEnabled();

      private void OnDisable() => OnActivityDisabled();
   }
}