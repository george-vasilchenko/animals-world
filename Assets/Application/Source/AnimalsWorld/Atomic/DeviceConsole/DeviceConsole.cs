using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class DeviceConsole : MonoBehaviour, IDeviceConsole
   {
      public static IDeviceConsole Instance;
      public Text Text;
      public Button ClearTextButton;

      private float _count;

      public void WriteLine(string text)
      {
         var timeStamp = DateTime.Now;
         var newEntry = $"{timeStamp:hh:mm:ss:fff}  {text}";
         Text.text = string.Concat(newEntry, Environment.NewLine, Text.text);
      }

      private void Awake()
         => Instance = this;

      private void OnEnable()
         => ClearTextButton.onClick.AddListener(OnClearTextButtonHandler);

      private void OnDisable()
         => ClearTextButton.onClick.RemoveAllListeners();

      private void OnClearTextButtonHandler()
         => Text.text = string.Empty;
   }
}