using System;

namespace AnimalsWorld
{
   public class OnScrollItemSelectedEventArgs : EventArgs
   {
      public IScrollItem ScrollItem { get; }

      public OnScrollItemSelectedEventArgs(IScrollItem scrollItem)
      {
         ScrollItem = scrollItem;
      }
   }
}