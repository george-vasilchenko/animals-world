using MalenkiyApps;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class ChooseAnimalChallenge : MiniActivity
   {
      public Button[] AnimalButtons;
      public Text AssignmentText;
      private Button _correctButton;
      private string _animalName;

      private bool _isClicked;
      private bool _isForcedToStop;
      private bool _isCorrect;

      public new void Awake()
      {
         Validation.Run(() => AnimalButtons != null, "AnimalButtons", "Must not be null.");
         Validation.Run(() => AnimalButtons.Any(), "AnimalButtons", "Must not be empty.");
         Validation.Run(() => AssignmentText != null, "AssignmentText", "Must be be null or empty.");
      }

      public override IEnumerator<object> RunActivity()
      {
         // shows and sets up
         Show();

         while (!IsStopCondition())
         {
            yield return _isCorrect;
         }

         yield return _isCorrect;

         Hide();
      }

      public override void ForceStop() => _isForcedToStop = true;

      public override bool IsStopCondition() => _isClicked || _isForcedToStop;

      public override void OnActivityEnabled()
      {
         var randomButtonIndex = Random.Range(0, AnimalButtons.Length);

         for (var i = 0; i < AnimalButtons.Length; i++)
         {
            if (i == randomButtonIndex)
            {
               _correctButton = AnimalButtons[i];
               _animalName = _correctButton.name;

               _correctButton.onClick.AddListener(() =>
               {
                  _isClicked = true;
                  _isCorrect = true;
               });
            }
            else
            {
               AnimalButtons[i].onClick.AddListener(() =>
               {
                  _isClicked = true;
                  _isCorrect = false;
               });
            }
         }

         AssignmentText.text = $"Where is the {_animalName}?";
      }

      public override void OnActivityDisabled()
      {
         foreach (var button in AnimalButtons)
         {
            button.onClick.RemoveAllListeners();
         }

         _correctButton = null;
         _animalName = string.Empty;

         Cleanup();
      }

      public override void Cleanup()
      {
         _isCorrect = false;
         _isClicked = false;
         _isForcedToStop = false;

         StopAllCoroutines();
      }
   }
}