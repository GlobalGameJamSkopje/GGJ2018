using System;
using System.Collections.Generic;
using System.Linq;

public class QuestCreator : IQuestCreator
{
    private const int MaxQuests = 5;
    private const int MaxQuestColorRequirement = 3;

    private readonly Random _random = new Random();

    public List<QuestItem> CreateQuests(int red, int green, int blue)
    {
        var questDrafts = GetQuestionDrafts();

        var modifiedRed = red + GetRandomBetween(1, 4);
        var modifiedGreen = green + GetRandomBetween(0, 4);
        var modifiedBlue = blue + GetRandomBetween(0, 4);

        var resources = GetShuffledResources(modifiedRed, modifiedGreen, modifiedBlue);
        while (resources.Any())
        {
            foreach (var item in questDrafts)
            {
                if (!resources.Any()) break;

                if (!ShouldEnter(resources.Peek(), item))
                    continue;

                switch (resources.Pop())
                {
                    case ResourceType.Red:
                        item.Red++;
                        break;
                    case ResourceType.Green:
                        item.Green++;
                        break;
                    case ResourceType.Blue:
                        item.Blue++;
                        break;
                    default:
                        break;
                }
            }
        }

        return questDrafts
            .Select(questDraft => new QuestItem(questDraft))
            .ToList();
    }

    private QuestDraft[] GetQuestionDrafts()
    {
        var questDrafts = new QuestDraft[MaxQuests];
        for (var i = 0; i < questDrafts.Length; i++)
            questDrafts[i] = new QuestDraft();
        return questDrafts;
    }

    private bool ShouldEnter(ResourceType resourceType, QuestDraft questDraft)
    {
        var result = GetRandomBetween(0, 10) >= 3;

        switch (resourceType)
        {
            case ResourceType.Red:
                result = result && questDraft.Red < MaxQuestColorRequirement;
                break;
            case ResourceType.Green:
                result = result && questDraft.Green < MaxQuestColorRequirement;
                break;
            case ResourceType.Blue:
                result = result && questDraft.Blue < MaxQuestColorRequirement;
                break;
            default:
                break;
        }

        return result;
    }

    private int GetRandomBetween(int min, int max)
    {
        return _random.Next(min, max);
    }

    private Stack<ResourceType> GetShuffledResources(int modifiedRed, int modifiedGreen, int modifiedBlue)
    {
        var resources = new List<ResourceType>();

        resources.AddRange(CreateRedResources(modifiedRed));
        resources.AddRange(CreateGreenResources(modifiedGreen));
        resources.AddRange(CreateBlueResources(modifiedBlue));

        resources.Shuffle();

        return new Stack<ResourceType>(resources);
    }
    private IEnumerable<ResourceType> CreateRedResources(int value)
    {
        for (var i = 0; i < value; i++)
            yield return ResourceType.Red;
    }
    private IEnumerable<ResourceType> CreateGreenResources(int value)
    {
        for (var i = 0; i < value; i++)
            yield return ResourceType.Green;
    }
    private IEnumerable<ResourceType> CreateBlueResources(int value)
    {
        for (var i = 0; i < value; i++)
            yield return ResourceType.Blue;
    }
}