namespace AnimalsWorld
{
   public interface IPanelManager
   {
      event OnPanelChangedDelegate OnPanelChanged;

      IPanel ChapterSelectionPanel { get; }

      IPanel GamePanel { get; }

      IPanel LevelSelectionChapter1Panel { get; }

      IPanel LevelSelectionChapter2Panel { get; }

      IPanel RewardPanel { get; }

      IPanel StartPanel { get; }

      IPanel ParentPanel { get; }

      IPanel GameSettingsPanel { get; }

      IPanel PurchasingPanel { get; }

      void SwitchToPanel(Panel panel);

      void SwitchToPanel(IPanel panel);

      void SwitchToPanelAndApply(IPanel panel, object data);

      void HideAllPanels();
   }
}