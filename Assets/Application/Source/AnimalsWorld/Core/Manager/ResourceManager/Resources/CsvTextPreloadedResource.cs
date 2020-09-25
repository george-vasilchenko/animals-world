namespace AnimalsWorld
{
   public class CsvTextPreloadedResource : PreloadedResource<string>
   {
      public CsvTextPreloadedResource(string csvText)
      : base(csvText)
      {
         if (string.IsNullOrWhiteSpace(csvText))
         {
            throw new System.ArgumentException("Csv text must no be null or empty.", nameof(csvText));
         }
      }
   }
}