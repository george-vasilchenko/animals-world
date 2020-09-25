using System;

namespace AnimalsWorld
{
   public class OnPanelChangedEventArgs : EventArgs
   {
      public IPanel PanelFrom { get; }

      public IPanel PanelTo { get; }

      public OnPanelChangedEventArgs(IPanel panelFrom, IPanel panelTo)
      {
         PanelFrom = panelFrom;
         PanelTo = panelTo;
      }
   }
}