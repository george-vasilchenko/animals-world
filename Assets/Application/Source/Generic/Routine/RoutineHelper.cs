using System;
using System.Collections;
using System.Linq.Expressions;

namespace AnimalsWorld
{
   public static class RoutineHelper
   {
      public static IEnumerator StartRoutine<T>(IEnumerator routine, Action<T> onCompleteAction, Action<string> onErrorAction,
         Expression<Func<bool>> cancellationExpression = null)
      {
         if (onCompleteAction == null)
         {
            throw new ArgumentNullException(nameof(onCompleteAction), "OnCompleteAction is not specified.");
         }

         if (onErrorAction == null)
         {
            throw new ArgumentNullException(nameof(onErrorAction), "OnErrorAction is not specified.");
         }

         while (true)
         {
            try
            {
               if (cancellationExpression != null)
               {
                  var func = cancellationExpression.Compile();
                  var cancelRoutine = func.Invoke();

                  if (cancelRoutine)
                  {
                     yield break;
                  }
               }

               if (!routine.MoveNext())
               {
                  yield break;
               }
            }
            catch (Exception e)
            {
               onErrorAction.Invoke(e.ToString());
               yield break;
            }

            var yielded = routine.Current;

            if (yielded != null && yielded is T variable)
            {
               onCompleteAction.Invoke(variable);
               yield break;
            }

            yield return yielded;
         }
      }
   }
}