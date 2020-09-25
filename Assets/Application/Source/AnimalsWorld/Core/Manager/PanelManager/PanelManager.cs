using MalenkiyApps;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class PanelManager : MonoBehaviour, IPanelManager
   {
      public event OnPanelChangedDelegate OnPanelChanged;

      public Panel StartPanel => _startPanel;

      public Panel ChapterSelectionPanel => _chapterSelectionPanel;

      public Panel LevelSelectionChapter1Panel => _levelSelectionChapter1Panel;

      public Panel LevelSelectionChapter2Panel => _levelSelectionChapter2Panel;

      public Panel GamePanel => _gamePanel;

      public Panel RewardPanel => _rewardPanel;

      public Panel ParentPanel => _parentPanel;

      public Panel GameSettingsPanel => _gameSettingsPanel;

      public Panel PurchasingPanel => _purchasingPanel;

      private Panel _previousPanel;

      private List<Panel> Panels
      {
         get
         {
            _panels = _panels ?? new List<Panel>
            {
               StartPanel,
               ChapterSelectionPanel,
               LevelSelectionChapter1Panel,
               LevelSelectionChapter2Panel,
               GamePanel,
               RewardPanel,
               ParentPanel,
               GameSettingsPanel,
               PurchasingPanel
            };
            return _panels;
         }
      }

      private List<Panel> _panels;

      [SerializeField] private Panel _startPanel = default;
      [SerializeField] private Panel _chapterSelectionPanel = default;
      [SerializeField] private Panel _levelSelectionChapter1Panel = default;
      [SerializeField] private Panel _levelSelectionChapter2Panel = default;
      [SerializeField] private Panel _gamePanel = default;
      [SerializeField] private Panel _rewardPanel = default;
      [SerializeField] private Panel _parentPanel = default;
      [SerializeField] private Panel _gameSettingsPanel = default;
      [SerializeField] private Panel _purchasingPanel = default;

      public void HideAllPanels()
      {
         foreach (var panel in Panels)
         {
            panel.Hide();
         }
      }

      public void SwitchToPanel(Panel panel)
      {
         if (panel == null)
         {
            throw new NullReferenceException("Provided panel game object must not be null.");
         }

         if (Panels == null || Panels.Count == 0)
         {
            Debug.LogWarning("Redundant call. Panels collection is empty.");
            return;
         }

         if (Panels.Count == 1)
         {
            Debug.LogWarning("Redundant call. Panels collection contains only 1 element.");
            return;
         }

         var otherPanels = Panels.Where(o => o != panel);

         foreach (var otherPanel in otherPanels)
         {
            otherPanel.Hide();
         }

         panel.Show();

         OnPanelChanged?.Invoke(new OnPanelChangedEventArgs(_previousPanel, panel));
         _previousPanel = panel;
      }

      #region Unity API

      public void Awake()
      {
         Validation.Run(() => StartPanel != null, "StartPanel", "Must not be null.");
         Validation.Run(() => ChapterSelectionPanel != null, "ChapterSelectionPanel", "Must not be null.");
         Validation.Run(() => LevelSelectionChapter1Panel != null, "LevelSelectionChapter1Panel", "Must not be null.");
         Validation.Run(() => LevelSelectionChapter2Panel != null, "LevelSelectionChapter2Panel", "Must not be null.");
         Validation.Run(() => GamePanel != null, "GamePanel", "Must not be null.");
         Validation.Run(() => RewardPanel != null, "RewardPanel", "Must not be null.");
         Validation.Run(() => GameSettingsPanel != null, "GameSettingsPanel", "Must not be null.");
         Validation.Run(() => PurchasingPanel != null, "PurchasingPanel", "Must not be null.");
      }

      private void Start() => SwitchToPanel(StartPanel);

      #endregion Unity API

      #region IPanelManager explicit

      IPanel IPanelManager.ChapterSelectionPanel => ChapterSelectionPanel;

      IPanel IPanelManager.GamePanel => GamePanel;

      IPanel IPanelManager.LevelSelectionChapter1Panel => LevelSelectionChapter1Panel;

      IPanel IPanelManager.LevelSelectionChapter2Panel => LevelSelectionChapter2Panel;

      IPanel IPanelManager.RewardPanel => RewardPanel;

      IPanel IPanelManager.StartPanel => StartPanel;

      IPanel IPanelManager.ParentPanel => ParentPanel;

      IPanel IPanelManager.GameSettingsPanel => GameSettingsPanel;

      IPanel IPanelManager.PurchasingPanel => PurchasingPanel;

      void IPanelManager.SwitchToPanel(IPanel panel) => SwitchToPanel((Panel)panel);

      void IPanelManager.SwitchToPanelAndApply(IPanel panel, object data)
      {
         if (panel == null)
         {
            throw new NullReferenceException("Provided panel game object must not be null.");
         }

         if (Panels == null || Panels.Count == 0)
         {
            Debug.LogWarning("Redundant call. Panels collection is empty.");
            return;
         }

         if (Panels.Count == 1)
         {
            Debug.LogWarning("Redundant call. Panels collection contains only 1 element.");
            return;
         }

         var otherPanels = Panels.Where(o => o != (Panel)panel);

         foreach (var otherPanel in otherPanels)
         {
            otherPanel.Hide();
         }

         if (data != null)
         {
            panel.ApplyData(data);
         }

         panel.Show();

         OnPanelChanged?.Invoke(new OnPanelChangedEventArgs(_previousPanel, panel));
         _previousPanel = (Panel)panel;
      }

      #endregion IPanelManager explicit
   }
}