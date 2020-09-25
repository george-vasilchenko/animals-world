using System;
using System.Collections;

namespace AnimalsWorld
{
   public class RewardPanel : MiniActivityPanel
   {
      private int _completedLevelChapterIndex;

      public override void ApplyData(object data) => _completedLevelChapterIndex = Convert.ToInt32(data);

      public void OnDisable()
      {
         StopCoroutine(RunActivity());

         if (CurrentActivity != null)
         {
            CurrentActivity.Cleanup();
            CurrentActivity = null;
         }
      }

      protected override IEnumerator RunActivity()
      {
         yield return GetRandomActivity().RunActivity();
         GameManager.Instance.UiManager.DisableInputForDuration(FeaturesConstants.InputDisabledDurationSeconds);
         LoadLevelSelectionPanel();
      }

      private void OnEnable() => StartCoroutine(RunActivity());

      private void LoadLevelSelectionPanel()
      {
         var levelsPanelToLoad = _completedLevelChapterIndex == 0
            ? GameManager.Instance.PanelManager.LevelSelectionChapter1Panel
            : GameManager.Instance.PanelManager.LevelSelectionChapter2Panel;

         GameManager.Instance.PanelManager.SwitchToPanelAndApply(levelsPanelToLoad, null);
      }
   }
}