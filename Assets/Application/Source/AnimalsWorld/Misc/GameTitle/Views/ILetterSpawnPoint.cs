using UnityEngine;

namespace AnimalsWorld
{
   public interface ILetterSpawnPoint
   {
      string Letter { get; }

      Vector2 GetLocation();
   }
}