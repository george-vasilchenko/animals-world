using System;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Button))]
   public class CipherButton : MonoBehaviour
   {
      public int Cipher;
      private Button _buttonComponent;

      public void RegisterChallengeListener(Action<int> listenerAction)
      {
         Validation.Run(() => Cipher >= 0, "Cipher", "Must be a positive integer.");

         _buttonComponent = _buttonComponent ?? GetComponent<Button>();
         _buttonComponent.onClick.AddListener(() => listenerAction(Cipher));
         _buttonComponent.onClick.AddListener(() => GameManager.Instance.SoundManager.PlayClickSound());
      }

      public void CleanUp() => _buttonComponent.onClick.RemoveAllListeners();
   }
}