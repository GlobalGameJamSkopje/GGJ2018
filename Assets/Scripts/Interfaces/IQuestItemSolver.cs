
using System.Collections.Generic;

public interface IQuestItemSolver
{
    bool CanBeSolved(List<QuestItem> quests, PlayerResources playerResources);
    bool CanBeSolved(QuestItem questItem, PlayerResources playerResources);
}