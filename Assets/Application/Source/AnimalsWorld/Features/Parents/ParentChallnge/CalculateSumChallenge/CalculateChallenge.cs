using System;
using System.Collections.Generic;
using System.Linq;
using MalenkiyApps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AnimalsWorld
{
   public class CalculateChallenge : MiniActivity
   {
      public Text OutputText;
      public CipherButton[] CipherButtons;
      public Button SubmitButton;
      public Button ResetButton;
      public Text AssignmentText;

      private int _firstOperandNumber;
      private int _secondOperandNumber;
      private int _expectedResultNumber;

      private bool _isClicked;
      private bool _isForcedToStop;
      private bool _isCorrect;

      public new void Awake()
      {
         Validation.Run(() => AssignmentText != null, "AssignmentText", "Must be be null.");
         Validation.Run(() => OutputText != null, "OutputText", "Must be be null.");
         Validation.Run(() => SubmitButton != null, "SubmitButton", "Must be be null.");
         Validation.Run(() => ResetButton != null, "ResetButton", "Must be be null.");
         Validation.Run(() => CipherButtons != null, "CipherButtons", "Must be be null.");
         Validation.Run(() => CipherButtons.Any(), "CipherButtons", "Must be be empty.");
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
         // generate numbers
         _firstOperandNumber = Random.Range(1, 6);
         _secondOperandNumber = Random.Range(1, 6);
         _expectedResultNumber = _firstOperandNumber + _secondOperandNumber;

         // cipher buttons
         foreach (var button in CipherButtons)
         {
            button.RegisterChallengeListener(OnCipherClickedHandler);
         }

         // output text
         OutputText.text = "";

         // control buttons
         SubmitButton.onClick.AddListener(OnSubmitClickedHandler);
         SubmitButton.onClick.AddListener(() => GameManager.Instance.SoundManager.PlayClickSound());
         ResetButton.onClick.AddListener(OnResetClickedHandler);
         ResetButton.onClick.AddListener(() => GameManager.Instance.SoundManager.PlayClickSound());

         // assignment text
         var translationText = GameManager.Instance.LocalizationManager.GetTranslation("k_parent_calc_format");
         var assignmentText = string.Format(translationText, _firstOperandNumber, _secondOperandNumber);
         AssignmentText.text = assignmentText;
      }

      private void OnSubmitClickedHandler()
      {
         _isClicked = true;
         _isCorrect = EvaluateCalculation();
      }

      private void OnResetClickedHandler() => OutputText.text = "";

      private bool EvaluateCalculation()
      {
         var calculationResult = Convert.ToInt32(OutputText.text);
         return _expectedResultNumber == calculationResult;
      }

      private void OnCipherClickedHandler(int cipher)
      {
         if (OutputText.text.Length < 4)
         {
            var cipherText = cipher.ToString();
            OutputText.text += cipherText;
         }
      }

      public override void OnActivityDisabled()
      {
         foreach (var button in CipherButtons)
         {
            button.CleanUp();
         }

         SubmitButton.onClick.RemoveAllListeners();
         ResetButton.onClick.RemoveAllListeners();

         Cleanup();
      }

      public override void Cleanup()
      {
         OutputText.text = "";
         _firstOperandNumber = 0;
         _secondOperandNumber = 0;
         _expectedResultNumber = 0;

         _isCorrect = false;
         _isClicked = false;
         _isForcedToStop = false;

         StopAllCoroutines();
      }
   }
}