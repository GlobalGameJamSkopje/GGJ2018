using System.Collections.Generic;
using System.Linq;

public class SideQuestItem : QuestItem
{
    public bool Hold { get; set; }
    public bool Completed { get; set; }
    public QuestDifficulty Difficulty { get; private set; }
    public Reward Reward { get; private set; }

    public SideQuestItem(int requiredRed, int requiredGreen, int requiredBlue, QuestDifficulty difficulty, Reward reward) : base(requiredRed, requiredGreen, requiredBlue)
    {
        Difficulty = difficulty;
        Reward = reward;
        Hold = false;
        Completed = false;
    }

    public SideQuestItem(QuestDraft questDraft, QuestDifficulty difficulty, Reward reward) : base(questDraft)
    {
        Difficulty = difficulty;
        Reward = reward;
        Hold = false;
        Completed = false;
    }

    public override string ToString()
    {
        var rewards = new List<string>();

        if (Reward == Reward.Move) rewards.Add("1 Move");
        if (Reward == Reward.Dig) rewards.Add("1 Dig");
        if (Reward == Reward.Build) rewards.Add("1 Build");
        if (Reward == Reward.MoveX2) rewards.Add("2 Move");
        if (Reward == Reward.Dig) rewards.Add("2 Dig");
        if (Reward == Reward.Build) rewards.Add("2 Build");

        return string.Format("Requirements: (RGB):({0},{1},{2}) \n", RequiredRedResources, RequiredGreenResources, RequiredBlueResources) +
            "Reward: " + string.Join(",", rewards.ToArray());
    }
}