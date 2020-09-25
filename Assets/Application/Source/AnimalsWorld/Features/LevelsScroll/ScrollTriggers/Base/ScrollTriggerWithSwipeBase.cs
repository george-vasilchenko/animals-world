namespace AnimalsWorld
{
   public abstract class ScrollTriggerWithSwipeBase : ScrollTriggerBase, IScrollTriggerWithSwipeBase
   {
      public abstract ISwipeHandler SwipeHandler { get; }
   }
}