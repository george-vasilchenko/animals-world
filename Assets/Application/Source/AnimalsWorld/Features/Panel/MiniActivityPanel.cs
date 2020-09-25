using MalenkiyApps;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public abstract class MiniActivityPanel : Panel
   {
      public MiniActivity[] Activities;
      protected MiniActivity CurrentActivity;

      public void Awake()
      {
         Validation.Run(() => Activities != null, "Activities", "Must not be null.");
         Validation.Run(() => Activities.Any(), "Activities", "Must not be empty.");
      }

      public override void ApplyData(object data)
      {
         throw new System.NotImplementedException("Implement this method in a child class.");
      }

      protected abstract IEnumerator RunActivity();

      protected virtual MiniActivity GetRandomActivity()
      {
         var randomIndex = Random.Range(0, Activities.Length);
         var activity = Activities[randomIndex];
         return activity;
      }
   }
}