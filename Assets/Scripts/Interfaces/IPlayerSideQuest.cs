public interface IPlayerSideQuest
{
    void RefillSideQuests();
    void ToggleHoldOnQuest(SideQuestItem quest);
    void CompleteQuest(SideQuestItem quest);
}