using MalenkiyApps;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   public class CatchFallingObjectGame : MiniActivity
   {
      public float GameDuration;
      public CatchFallingObjectCatcher Catcher;
      public CatchFallingObjectSpawner Spawner;
      public CatchFallingObjectFloor Floor;
      public CatchFallingObjectContainer Container;

      private float _counter;
      private bool _isForcedToStop;

      public new void Awake()
      {
         base.Awake();

         Validation.Run(() => Catcher != null, "Catcher", "Must not be null.");
         Validation.Run(() => Spawner != null, "Spawner", "Must not be null.");
         Validation.Run(() => Floor != null, "Floor", "Must not be null.");
         Validation.Run(() => Container != null, "Container", "Must not be null.");
         Validation.Run(() => GameDuration > 0, "GameDuration", "Must be grater than 0.");
      }

      public override IEnumerator<object> RunActivity()
      {
         Show();

         while (!IsStopCondition())
         {
            var instance = Spawner.Spawn();
            if (instance != null)
            {
               Container.Add(instance);
            }

            yield return null;
         }

         Hide();
      }

      public override void ForceStop()
      {
         GameManager.Instance.AnalyticsManager.LogRewardGameCloseInfo(new RewardGameCloseInfo(Name));
         _isForcedToStop = true;
      }

      public override bool IsStopCondition() => (_counter -= Time.deltaTime) < 0 || _isForcedToStop;

      public override void OnActivityEnabled()
      {
         Catcher.OnObjectCaught += Catcher_OnObjectCaught;
         Floor.OnObjectLost += Floor_OnObjectLost;

         GameManager.Instance.AnalyticsManager.LogRewardGameStartInfo(new RewardGameStartInfo(Name));
         Container.Setup();
         _counter = GameDuration;
      }

      private static void Floor_OnObjectLost() => GameManager.Instance.SoundManager.PlaySnapSound();

      private static void Catcher_OnObjectCaught() => GameManager.Instance.SoundManager.PlayClickSound();

      public override void OnActivityDisabled()
      {
         Catcher.OnObjectCaught -= Catcher_OnObjectCaught;
         Floor.OnObjectLost -= Floor_OnObjectLost;

         Spawner.Stop();
         Container.Clean();

         Cleanup();
      }

      public override void Cleanup()
      {
         _counter = 0;
         _isForcedToStop = false;

         StopAllCoroutines();
      }
   }
}