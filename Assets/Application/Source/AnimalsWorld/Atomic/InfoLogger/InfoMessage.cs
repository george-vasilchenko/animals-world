using MalenkiyApps;
using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class InfoMessage : ILogMessage
   {
      public string Content { get; }

      public InfoMessage(string content)
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(content), nameof(content), "Must not be null or white space.");

         Content = content;
      }
   }
}