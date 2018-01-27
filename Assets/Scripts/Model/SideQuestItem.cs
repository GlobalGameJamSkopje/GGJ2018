public partial class SideQuestItem : QuestItem
{
    public bool Hold { get; set; }
    public bool IsUsed { get; set; }
    public QuestDifficulty Difficulty { get; private set; }

    public SideQuestItem(int requiredRed, int requiredGreen, int requiredBlue, QuestDifficulty difficulty) : base(requiredRed, requiredGreen, requiredBlue)
    {
        Difficulty = difficulty;
        Hold = false;
        IsUsed = false;
    }

    public SideQuestItem(QuestDraft questDraft, QuestDifficulty difficulty) : base(questDraft)
    {
        Difficulty = difficulty;
        Hold = false;
        IsUsed = false;
    }
}