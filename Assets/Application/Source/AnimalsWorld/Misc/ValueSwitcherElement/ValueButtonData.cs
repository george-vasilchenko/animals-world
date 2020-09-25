using UnityEngine;

namespace AnimalsWorld
{
   public class ValueButtonData : MonoBehaviour
   {
      [SerializeField] private float _value = default;

      public float Value => _value;
   }
}