using System;

namespace AnimalsWorld
{
   public class OnFigureSnapEventArgs : EventArgs
   {
      public string FigureName { get; private set; }

      public OnFigureSnapEventArgs(string figureName)
      {
         FigureName = figureName;
      }
   }
}