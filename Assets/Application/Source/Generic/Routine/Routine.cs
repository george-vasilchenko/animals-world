using System;
using System.Collections;

namespace AnimalsWorld
{
   public class Routine<TResult> : IRoutine<TResult>
   {
      public TResult Result { get; private set; }
      public Exception Exception { get; private set; }
      private readonly IEnumerator _routine;
      private readonly Func<bool> _cancellationExpression;

      private bool _wasInvoked;

      public static IRoutine<TResult> Create(IEnumerator routine, Func<bool> cancellationExpression = null)
      {
         return new Routine<TResult>(routine, cancellationExpression);
      }

      private Routine(IEnumerator routine, Func<bool> cancellationExpression = null)
      {
         _routine = routine;
         _cancellationExpression = cancellationExpression;

         Exception = new Exception("Routine was not yet invoked. Result is undefined.");
      }

      public IEnumerator Invoke()
      {
         if (_wasInvoked)
         {
            Exception = new Exception("Routine was already invoked, dispose this object.");
            yield break;
         }

         _wasInvoked = true;
         Exception = null;

         while (true)
         {
            try
            {
               if (_cancellationExpression != null)
               {
                  if (_cancellationExpression.Invoke())
                  {
                     yield break;
                  }
               }

               if (!_routine.MoveNext())
               {
                  yield break;
               }
            }
            catch (Exception e)
            {
               Exception = e;
               yield break;
            }

            var yielded = _routine.Current;

            if (yielded != null && yielded is TResult variable)
            {
               Result = variable;
               yield break;
            }

            yield return yielded;
         }
      }
   }
}