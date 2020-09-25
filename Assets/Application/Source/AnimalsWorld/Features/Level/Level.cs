using MalenkiyApps;
using System;
using System.Collections;
using UnityEngine;

namespace AnimalsWorld
{
   public abstract class Level : Element, ILevel
   {
      public int ChapterIndex => Index[0];

      public int LevelIndex => Index[1];

      public const char LevelNameSplitCharacter = '.';

      private int[] Index
      {
         get
         {
            _levelNameParts = _levelNameParts ?? gameObject.name.Split(LevelNameSplitCharacter);
            var result = new[] { Convert.ToInt32(_levelNameParts[0]), Convert.ToInt32(_levelNameParts[1]) };
            return result;
         }
      }

      public GameObject BackButton;
      private string[] _levelNameParts;

      public virtual void Awake() => Validation.Run(() => BackButton != null, "BackButton", "Must not be null.");

      public void OnEnable()
      {
         LogLevelInfo();
         OnLevelEnabled();
      }

      public void OnDisable() => OnLevelDisabled();

      protected abstract void OnLevelEnabled();

      protected abstract void OnLevelDisabled();

      protected virtual IEnumerator OnLevelCompleteExtraActivity() => null;

      protected IEnumerator CompleteLevel()
      {
         BackButton.SetActive(false);

         GameManager.Instance.SoundManager.PlayVictorySound();
         GameManager.Instance.UiManager.DisableInputForDuration(FeaturesConstants.InputDisabledDurationSeconds);

         yield return OnLevelCompleteExtraActivity();
         yield return new WaitForSeconds(FeaturesConstants.InputDisabledDurationSeconds);

         GameManager.Instance.PanelManager.SwitchToPanelAndApply(GameManager.Instance.PanelManager.RewardPanel, ChapterIndex);

         BackButton.SetActive(true);
      }

      private void LogLevelInfo() => GameManager.Instance.AnalyticsManager.LogLevelInfo(new LevelInfo(ChapterIndex, LevelIndex));
   }
}