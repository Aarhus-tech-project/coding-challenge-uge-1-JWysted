namespace BoxingGame;

public enum PlayerNumber
{
    Player1,
    Player2
}

public class Player(string name, PlayerNumber playerNum)
{
    public string Name { get; private set; } = name;
    public int Health { get; private set; } = Config.STARTING_HEALTH;
    public int Stamina { get; private set; } = Config.STARTING_STAMINA;
    public PlayerNumber PlayerNum { get; private set; } = playerNum;

    private ActionType currentAction = ActionType.Passive;
    private ActionType queuedAction = ActionType.Passive;
    private int actionFramesRemaining = 0;
    private readonly List<DamageEvent> recentDamage = [];

    public void SetQueuedAction(ActionType action)
    {
        queuedAction = action;
    }

    public void Update()
    {
        if (currentAction == ActionType.Concussion)
        {
            actionFramesRemaining--;
            if (actionFramesRemaining <= 0)
            {
                currentAction = ActionType.Passive;
                actionFramesRemaining = 0;
            }
            return;
        }

        if (currentAction == ActionType.Passive)
        {
            Stamina = Math.Min(Config.MAX_STAMINA, Stamina + Math.Abs(Config.ActionDatabase.Passive.StaminaCost));
        }

        if (actionFramesRemaining > 0)
        {
            actionFramesRemaining--;
        }
        else
        {
            currentAction = ActionType.Passive;

            if (queuedAction != ActionType.Passive)
            {
                TryExecuteAction(queuedAction);
                queuedAction = ActionType.Passive; 
            }
        }
    }

    private void TryExecuteAction(ActionType action)
    {
        var actionData = Config.ActionDatabase.GetAction(action);
        
        if (Stamina >= actionData.StaminaCost)
        {
            Stamina -= actionData.StaminaCost;
            currentAction = action;
            actionFramesRemaining = actionData.FrameDuration;
        }
    }

    public void TakeDamage(int damage, int currentFrame)
    {
        Health = Math.Max(0, Health - damage);
        
        recentDamage.Add(new DamageEvent(damage, currentFrame));

        recentDamage.RemoveAll(d => currentFrame - d.Frame > Config.CONCUSSION_FRAME_WINDOW);

        int damageInLast10Frames = recentDamage.Sum(d => d.Damage);
        if (damageInLast10Frames > Config.CONCUSSION_DAMAGE_THRESHOLD)
        {
            currentAction = ActionType.Concussion;
            actionFramesRemaining = Config.ActionDatabase.Concussion.FrameDuration; 
            recentDamage.Clear();
        }
    }

    public int CalculateDamage()
    {
        var actionData = Config.ActionDatabase.GetAction(currentAction);
        Random rand = new();
        return rand.Next(actionData.MinDamage, actionData.MaxDamage + 1);
    }

    public ActionType GetCurrentActionType()
    {
        return currentAction;
    }

    public bool IsActionComplete()
    {
        return actionFramesRemaining == 1; 
    }

    public int GetActionFramesRemaining()
    {
        return actionFramesRemaining;
    }
}

public class DamageEvent(int damage, int frame)
{
    public int Damage { get; set; } = damage;
    public int Frame { get; set; } = frame;
}
