namespace AnimalsWorld
{
   public static class Mapper
   {
      public static float MapRange(float fromMin, float fromMax, float toMin, float toMax, float current)
      {
         return (current - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
      }
   }
}