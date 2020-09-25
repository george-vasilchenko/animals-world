using MalenkiyApps.Interfaces;

namespace AnimalsWorld
{
   public class RewardGameStartInfo : IRewardGameStartInfo
   {
      public string Name { get; private set; }

      public RewardGameStartInfo(string name)
      {
         Name = name;
      }
   }
}