public partial class SideQuestItem : QuestItem
{
    public bool Hold { get; set; }
    public bool Completed { get; set; }
    public QuestDifficulty Difficulty { get; private set; }

    public SideQuestItem(int requiredRed, int requiredGreen, int requiredBlue, QuestDifficulty difficulty) : base(requiredRed, requiredGreen, requiredBlue)
    {
        Difficulty = difficulty;
        Hold = false;
        Completed = false;
    }

    public SideQuestItem(QuestDraft questDraft, QuestDifficulty difficulty) : base(questDraft)
    {
        Difficulty = difficulty;
        Hold = false;
        Completed = false;
    }
}