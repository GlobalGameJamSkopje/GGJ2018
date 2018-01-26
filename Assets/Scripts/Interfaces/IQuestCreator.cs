using System.Collections.Generic;

public interface IQuestCreator
{
    List<QuestItem> CreateQuests(int red, int green, int blue);
}