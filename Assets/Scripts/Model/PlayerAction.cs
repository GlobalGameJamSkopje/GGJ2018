using System;

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

    public void NewTurn()
    {
        AnyAction = 2;
        Move = 0;
        Dig = 0;
        Build = 0;
    }

    public bool CanUseAction(PlayerActionType actionType)
    {
        switch (actionType)
        {
            case PlayerActionType.Move:
                return Move > 0 || AnyAction > 0;

            case PlayerActionType.Dig:
                return Dig > 0 || AnyAction > 0;

            case PlayerActionType.Build:
                return Build > 0 || AnyAction > 0;

            default:
                return false;
        }
    }

    public void UseAction(PlayerActionType actionType)
    {
        switch (actionType)
        {
            case PlayerActionType.Move:
                if (Move > 0) Move--;
                else AnyAction--;
                break;

            case PlayerActionType.Dig:
                if (Dig > 0) Dig--;
                else AnyAction--;
                break;

            case PlayerActionType.Build:
                if (Build > 0) Build--;
                else AnyAction--;
                break;

            default:
                throw new ArgumentOutOfRangeException("actionType", actionType, null);
        }
    }

    public void GetSideQuestReward(SideQuestItem quest)
    {
        switch (quest.Reward)
        {
            case Reward.None:
                break;
            case Reward.Move:
                Move++;
                break;
            case Reward.Dig:
                Dig++;
                break;
            case Reward.Build:
                Build++;
                break;
            case Reward.MoveX2:
                Move += 2;
                break;
            case Reward.DigX2:
                Dig += 2;
                break;
            case Reward.BuildX2:
                Build += 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
