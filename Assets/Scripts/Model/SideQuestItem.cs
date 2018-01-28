using System;
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

        switch (Reward)
        {
            case Reward.None:
                break;
            case Reward.Move:
                rewards.Add("1 Move");
                break;
            case Reward.Dig:
                rewards.Add("1 Dig");
                break;
            case Reward.Build:
                rewards.Add("1 Build");
                break;
            case Reward.MoveX2:
                rewards.Add("2 Move");
                break;
            case Reward.DigX2:
                rewards.Add("2 Dig");
                break;
            case Reward.BuildX2:
                rewards.Add("2 Build");
                break;
            case Reward.Move | Reward.Dig | Reward.Build:
                rewards.Add("1 Move");
                rewards.Add("1 Dig");
                rewards.Add("1 Build");
                break;
            case Reward.MoveX2 | Reward.Dig:
                rewards.Add("2 Move");
                rewards.Add("1 Dig");
                break;
            case Reward.MoveX2 | Reward.Build:
                rewards.Add("2 Move");
                rewards.Add("1 Build");
                break;
            case Reward.DigX2 | Reward.Move:
                rewards.Add("1 Move");
                rewards.Add("2 Dig");
                break;
            case Reward.DigX2 | Reward.Build:
                rewards.Add("2 Dig");
                rewards.Add("1 Build");
                break;
            case Reward.BuildX2 | Reward.Move:
                rewards.Add("1 Move");
                rewards.Add("2 Build");
                break;
            case Reward.BuildX2 | Reward.Dig:
                rewards.Add("1 Dig");
                rewards.Add("2 Build");
                break;
            default:
                break;
        }

        return string.Format("Requirements: (RGB):({0},{1},{2}) \n", RequiredRedResources, RequiredGreenResources, RequiredBlueResources) +
            "Reward: " + string.Join(",", rewards.ToArray());
    }
}