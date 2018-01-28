
using System.Collections.Generic;
using System.Linq;

public class QuestItemSolver : IQuestItemSolver
{
    public bool CanBeSolved(List<QuestItem> quests, PlayerResources playerResources)
    {
        var totalRedRequired = quests.Sum(x => x.RequiredRedResources);
        var totalGreenRequired = quests.Sum(x => x.RequiredGreenResources);
        var totalBlueRequired = quests.Sum(x => x.RequiredBlueResources);

        var redCompleted = totalRedRequired <= playerResources.NumberOfRedResources;
        var greenCompleted = totalGreenRequired <= playerResources.NumberOfGreenResources;
        var blueCompleted = totalBlueRequired <= playerResources.NumberOfBlueResources;

        return redCompleted 
               && greenCompleted 
               && blueCompleted;
    }

    public bool CanBeSolved(QuestItem questItem, PlayerResources playerResources)
    {
        var areRedResourcesMatched = questItem.RequiredRedResources <= playerResources.NumberOfRedResources;
        var areGreenResourcesMatched = questItem.RequiredGreenResources <= playerResources.NumberOfGreenResources;
        var areBlueResourcesMatched = questItem.RequiredBlueResources <= playerResources.NumberOfBlueResources;

        return areRedResourcesMatched
               && areGreenResourcesMatched
               && areBlueResourcesMatched;
    }

}