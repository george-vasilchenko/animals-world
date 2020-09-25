using System;

namespace MalenkiyApps.Interfaces
{
   public class OnInitializationFailedEventArgs : EventArgs
   {
      public OnInitializationFailedEventArgs(InitializationFailureReason reason)
      {
         Reason = reason;
      }

      public InitializationFailureReason Reason { get; }
   }
}