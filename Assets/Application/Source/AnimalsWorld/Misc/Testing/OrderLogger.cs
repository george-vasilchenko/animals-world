using UnityEngine;

namespace AnimalsWorld
{
   public class OrderLogger : MonoBehaviour
   {
      private bool _isFirstFrame = true;

      private void Awake()
      {
         Debug.Log($"{name} : Awake");
      }

      private void OnEnable()
      {
         Debug.Log($"{name} : OnEnable");
      }

      //private void OnDisable()
      //{
      //   Debug.Log($"{name} : OnDisable");
      //}

      private void Start()
      {
         Debug.Log($"{name} : Start");
      }

      private void Update()
      {
         if (!_isFirstFrame)
         {
            return;
         }

         _isFirstFrame = false;

         Debug.Log($"{name} : Update");
      }
   }
}