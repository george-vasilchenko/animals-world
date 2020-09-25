using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsWorld
{
   public interface ILoggingManager
   {
      void LogMessage(string message);

      void LogLocalizedMessage(string key);
   }
}