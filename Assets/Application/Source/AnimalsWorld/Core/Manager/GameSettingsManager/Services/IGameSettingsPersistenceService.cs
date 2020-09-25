using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsWorld
{
   public interface IGameSettingsPersistenceService
   {
      IGameSettings Load();

      void Save(IGameSettings gameSettings);
   }
}