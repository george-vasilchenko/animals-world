using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class LetterSpawnPointBehaviour : MonoBehaviour, ILetterSpawnPoint
   {
      public string Letter => _letter;
      [SerializeField] private string _letter = default;

      private void Awake()
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(_letter), nameof(_letter), "Must not be null or empty.");
      }

      public Vector2 GetLocation()
      {
         return transform.position;
      }
   }
}