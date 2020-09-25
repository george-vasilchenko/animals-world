using System;

namespace AnimalsWorld
{
   public interface IConfirmationDialogService
   {
      void Open(string message, Action onConfirmAction, Action onCloseAction);
   }
}