using System;
using System.Collections;

namespace AnimalsWorld
{
   public interface IRoutine<out TResult>
   {
      TResult Result { get; }
      Exception Exception { get; }

      IEnumerator Invoke();
   }
}