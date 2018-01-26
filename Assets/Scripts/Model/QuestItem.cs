public class QuestItem
{
    public int RequiredRedResources { get; private set; }
    public int RequiredGreenResources { get; private set; }
    public int RequiredBlueResources { get; private set; }

    public QuestItem(int requiredRed, int requiredGreen, int requiredBlue)
    {
        RequiredRedResources = requiredRed;
        RequiredGreenResources = requiredGreen;
        RequiredBlueResources = requiredBlue;
    }
}