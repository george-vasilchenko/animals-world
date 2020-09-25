using MalenkiyApps;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   public class BaloonPokeGame : MiniActivity
   {
      public float GameDuration;
      public BaloonSpawner BaloonSpawner;
      public BaloonContainer BaloonContainer;
      private float _counter;
      private bool _isForcedToStop;

      public new void Awake()
      {
         base.Awake();

         Validation.Run(() => BaloonSpawner != null, "BaloonSpawner", "Must not be null.");
         Validation.Run(() => BaloonContainer != null, "BaloonContainer", "Must not be null.");
         Validation.Run(() => GameDuration > 0, "GameDuration", "Must be grater than 0.");
      }

      public override IEnumerator<object> RunActivity()
      {
         Show();

         while (!IsStopCondition())
         {
            var instance = BaloonSpawner.Spawn();
            if (instance != null)
            {
               BaloonContainer.Add(instance);
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
         GameManager.Instance.AnalyticsManager.LogRewardGameStartInfo(new RewardGameStartInfo(Name));
         BaloonContainer.Setup();
         _counter = GameDuration;
      }

      public override void OnActivityDisabled()
      {
         BaloonSpawner.Stop();
         BaloonContainer.Clean();

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