using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class RewardGameCloseInfo : IRewardGameCloseInfo
   {
      public string Name { get; private set; }

      public RewardGameCloseInfo(string name)
      {
         Name = name;
      }
   }
}