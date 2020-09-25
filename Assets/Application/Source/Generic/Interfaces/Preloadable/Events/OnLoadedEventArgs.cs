using System;

namespace AnimalsWorld
{
   public class OnLoadedEventArgs<T> : EventArgs
   {
      public T Data { get; }

      public OnLoadedEventArgs(T data)
      {
         Data = data;
      }
   }
}