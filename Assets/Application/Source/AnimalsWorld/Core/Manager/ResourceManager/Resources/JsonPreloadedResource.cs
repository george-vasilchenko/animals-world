namespace AnimalsWorld
{
   public class JsonPreloadedResource : PreloadedResource<string>
   {
      public JsonPreloadedResource(string json)
         : base(json)
      {
         if (string.IsNullOrWhiteSpace(json))
         {
            throw new System.ArgumentException("Json must no be null or empty.", nameof(json));
         }
      }
   }
}