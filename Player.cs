
namespace BoxingGame;

public enum PlayerNumber
{
    Player1,
    Player2
}

public class Player 
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Stamina { get; private set; }
    public PlayerNumber PlayerNum { get; private set; }

    private ActionType currentAction;
    private ActionType queuedAction;
    private int actionFramesRemaining;
    private List<DamageEvent> recentDamage;

    public Player(string name, PlayerNumber playerNum)
    {
        Name = name;
        PlayerNum = playerNum;
        Health = Config.STARTING_HEALTH;
        Stamina = Config.STARTING_STAMINA;
        currentAction = ActionType.Passive;
        queuedAction = ActionType.Passive;
        actionFramesRemaining = 0;
        recentDamage = new List<DamageEvent>();
    }

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
            Stamina = Math.Min(Config.MAX_STAMINA, Stamina + Config.PASSIVE_STAMINA_REGEN);
        }

        // Update current action
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
        var actionData = ActionDatabase.GetAction(action);
        
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
            actionFramesRemaining = Config.Actions.Concussion.FRAME_DURATION; 
            recentDamage.Clear();
        }
    }

    public int CalculateDamage()
    {
        var actionData = ActionDatabase.GetAction(currentAction);
        Random rand = new Random();
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

public class DamageEvent
{
    public int Damage { get; set; }
    public int Frame { get; set; }

    public DamageEvent(int damage, int frame)
    {
        Damage = damage;
        Frame = frame;
    }
}
