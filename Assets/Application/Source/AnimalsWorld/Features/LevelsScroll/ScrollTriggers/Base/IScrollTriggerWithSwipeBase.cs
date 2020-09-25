namespace AnimalsWorld
{
   public interface IScrollTriggerWithSwipeBase : IScrollTrigger
   {
      ISwipeHandler SwipeHandler { get; }
   }
}