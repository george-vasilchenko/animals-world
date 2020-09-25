using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class CatchFallingObjectSpawner : MonoBehaviour
   {
      public GameObject FallingObject;
      public RectTransform[] Locations;
      public float SpawnIntervalSeconds = 1;
      private float _counter;

      public void Awake()
      {
         Validation.Run(() => FallingObject != null, "FallingObject", "Must not be null.");
         Validation.Run(() => Locations != null, "Locations", "Must not be null.");
         Validation.Run(() => Locations.Length > 0, "Locations.Length", "Must be grater than 0.");
      }

      public void Stop() => _counter = SpawnIntervalSeconds;

      public GameObject Spawn()
      {
         _counter -= Time.deltaTime;

         if (_counter <= 0)
         {
            _counter = SpawnIntervalSeconds;

            return Instantiate(FallingObject, Locations[Random.Range(0, Locations.Length)].position, Quaternion.identity);
         }

         return null;
      }

      private void Start() => _counter = SpawnIntervalSeconds;
   }
}