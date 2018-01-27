using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerSideQuest : IPlayerSideQuest
{
    public static readonly List<SideQuestItem> AllSideQuests = new List<SideQuestItem>
    {
        new SideQuestItem(6, 0, 0, QuestDifficulty.Easy, Reward.Move),
        new SideQuestItem(0, 6, 0, QuestDifficulty.Easy, Reward.Dig),
        new SideQuestItem(0, 0, 6, QuestDifficulty.Easy, Reward.Build),

        new SideQuestItem(10, 0, 0, QuestDifficulty.Medium, Reward.MoveX2),
        new SideQuestItem(0, 10, 0, QuestDifficulty.Medium, Reward.DigX2),
        new SideQuestItem(0, 0, 10, QuestDifficulty.Medium, Reward.BuildX2),

        new SideQuestItem(10, 0, 5, QuestDifficulty.Hard, Reward.MoveX2 | Reward.Build),
        new SideQuestItem(10, 5, 0, QuestDifficulty.Hard, Reward.MoveX2 | Reward.Dig),
        new SideQuestItem(0, 10, 5, QuestDifficulty.Hard, Reward.DigX2 | Reward.Build),
        new SideQuestItem(5, 10, 0, QuestDifficulty.Hard, Reward.Move | Reward.DigX2),
        new SideQuestItem(0, 5, 10, QuestDifficulty.Hard, Reward.Dig | Reward.BuildX2),
        new SideQuestItem(5, 0, 10, QuestDifficulty.Hard, Reward.Move | Reward.BuildX2),
        new SideQuestItem(5, 5, 5, QuestDifficulty.Hard, Reward.Move | Reward.Dig | Reward.Build)
    };

    public List<SideQuestItem> SideQuests { get; private set; }

    private readonly Random _random = new Random();

    public PlayerSideQuest()
    {
        SideQuests = new List<SideQuestItem>();
        SideQuests.Add(GetNewSideQuest());
        SideQuests.Add(GetNewSideQuest());
    }

    public void RefillSideQuests()
    {
        for (var i = 0; i < SideQuests.Count; i++)
        {
            if (!SideQuests.ElementAt(i).Hold)
            {
                var sideQuest = GetNewSideQuest();
                SideQuests.RemoveAt(i);
                SideQuests.Add(sideQuest);
            }
        }
    }

    public void ToggleHoldOnQuest(SideQuestItem quest)
    {
        quest.Hold = !quest.Hold;
    }

    public void CompleteQuest(SideQuestItem quest)
    {
        quest.Completed = true;
    }

    private SideQuestItem GetNewSideQuest()
    {
        var randomValue = _random.Next(0, 100);
        if (randomValue > 85)
        {
            var hard = AllSideQuests
               .Except(SideQuests)
                .Where(x => x.Difficulty == QuestDifficulty.Hard)
                .ToList().Shuffle();

            return hard.First();
        }
        else if (randomValue > 50)
        {
            var medium = AllSideQuests
                .Except(SideQuests)
                .Where(x => x.Difficulty == QuestDifficulty.Medium)
                .ToList().Shuffle();

            return medium.First();
        }
        else
        {
            var easy = AllSideQuests
                .Except(SideQuests)
                .Where(x => x.Difficulty == QuestDifficulty.Easy)
                .ToList().Shuffle();

            return easy.First();
        }
    }
}

public interface IPlayerSideQuest
{
    void RefillSideQuests();
    void ToggleHoldOnQuest(SideQuestItem quest);
    void CompleteQuest(SideQuestItem quest);
}