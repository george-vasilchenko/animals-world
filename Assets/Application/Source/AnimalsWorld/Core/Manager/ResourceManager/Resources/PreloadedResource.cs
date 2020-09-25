namespace AnimalsWorld
{
   public abstract class PreloadedResource<TResource>
   {
      public TResource Resource { get; }

      protected PreloadedResource(TResource resource)
      {
         Resource = resource;
      }
   }
}