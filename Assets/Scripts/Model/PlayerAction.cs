public class PlayerAction
{
    public int AnyAction { get; private set; }

    public int Move { get; set; }
    public int Dig { get; set; }
    public int Build { get; set; }

    public PlayerAction(int anyAction, int move, int dig, int build)
    {
        AnyAction = anyAction;
        Move = move;
        Dig = dig;
        Build = build;
    }

    public void GetSideQuestReward(SideQuestItem quest)
    {
        if (quest.Reward == Reward.Move) Move++;
        if (quest.Reward == Reward.Dig) Dig++;
        if (quest.Reward == Reward.Build) Build++;
        if (quest.Reward == Reward.MoveX2) Move++;
        if (quest.Reward == Reward.Dig) Dig++;
        if (quest.Reward == Reward.Build) Build++;
    }
}
