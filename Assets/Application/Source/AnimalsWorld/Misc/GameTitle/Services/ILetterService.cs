using System.Collections.Generic;

namespace AnimalsWorld
{
   public interface ILetterService
   {
      void PlaceLetters(IEnumerable<ILetterSpawnPoint> points, IEnumerable<ILetter> letters);

      void BubbleLetter(ILetter letter);
   }
}