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
}