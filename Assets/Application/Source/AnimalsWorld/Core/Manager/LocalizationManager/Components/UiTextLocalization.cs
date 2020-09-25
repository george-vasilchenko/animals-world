using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class UiTextLocalization : MonoBehaviour
   {
      public string Key;

      private Text _textComponent;

      public void Awake()
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(Key), "Key", "Key must not be null or white space.");

         _textComponent = GetComponent<Text>();
         Validation.Run(() => _textComponent != null, "Text", "Text must not be null.");

         _textComponent.text = GameManager.Instance.LocalizationManager.GetTranslation(Key);
      }
   }
}