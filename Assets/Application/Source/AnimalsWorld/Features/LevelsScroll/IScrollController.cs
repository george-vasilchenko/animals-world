using System.Collections.Generic;

namespace AnimalsWorld
{
   public interface IScrollController
   {
      IScrollItemsCollection ScrollItemsCollection { get; }

      IEnumerable<IScrollTrigger> ScrollTriggers { get; }

      event OnScrollControllerCurrentIndexChangedDelegate OnScrollControllerCurrentIndexChanged;
   }
}