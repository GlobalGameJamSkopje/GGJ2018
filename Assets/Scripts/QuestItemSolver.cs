
public class QuestItemSolver : IQuestItemSolver
{
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