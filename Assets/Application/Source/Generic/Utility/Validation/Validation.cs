using System;
using System.Diagnostics;

namespace MalenkiyApps
{
   [DebuggerStepThrough]
   public static class Validation
   {
      ////public static void Run(Expression<Func<bool>> expression, string parameterName, string exceptionMessage)
      ////{
      ////   var compiled = expression.Compile();
      ////   var result = compiled.Invoke();

      ////   if (!result)
      ////   {
      ////      throw new Exception($"Validation failed for: '{parameterName}'. Details: {exceptionMessage}");
      ////   }
      ////}

      public static void Run(Func<bool> expressionFunc, string parameterName, string exceptionMessage)
      {
         var result = expressionFunc.Invoke();

         if (!result)
         {
            throw new Exception($"Validation failed for: '{parameterName}'. Details: {exceptionMessage}");
         }
      }
   }
}