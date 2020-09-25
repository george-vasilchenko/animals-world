using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalsWorld
{
   public class LetterService : ILetterService
   {
      public void PlaceLetters(IEnumerable<ILetterSpawnPoint> points, IEnumerable<ILetter> letters)
      {
         var pointsArray = points.ToArray();
         var lettersArray = letters.ToArray();

         if (pointsArray.Length != lettersArray.Length)
         {
            throw new Exception("Letters amount doesn't match locations amount.");
         }

         for (var i = 0; i < pointsArray.Length; i++)
         {
            var location = pointsArray[i].GetLocation();
            lettersArray[i].SetLocation(location.x, location.y);
            lettersArray[i].TriggerAppearEffect();
         }
      }

      public void BubbleLetter(ILetter letter)
      {
         letter.TriggerBubbleEffect();
      }
   }
}