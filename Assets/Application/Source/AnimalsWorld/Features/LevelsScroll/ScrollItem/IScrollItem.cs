namespace AnimalsWorld
{
   public interface IScrollItem : IOnClickHandler
   {
      string ItemName { get; }
      int ItemIndex { get; }
   }
}