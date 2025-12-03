namespace BoxingGame
{
    public class ActionData
    {
        public ActionType Type { get; set; }
        public int StaminaCost { get; set; }
        public int FrameDuration { get; set; }
        public int DodgeChance { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public bool IgnoresBlock { get; set; }

        public ActionData(ActionType type, int staminaCost, int frameDuration, 
                         int dodgeChance, int minDamage, int maxDamage, bool ignoresBlock = false)
        {
            Type = type;
            StaminaCost = staminaCost;
            FrameDuration = frameDuration;
            DodgeChance = dodgeChance;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            IgnoresBlock = ignoresBlock;
        }
    }

    public static class ActionDatabase
    {
        private static readonly Dictionary<ActionType, ActionData> actions = new()
        {
            { ActionType.Passive, new ActionData(ActionType.Passive, 
                Config.Actions.Passive.STAMINA_COST, 
                Config.Actions.Passive.FRAME_DURATION, 
                Config.Actions.Passive.DODGE_CHANCE, 
                Config.Actions.Passive.MIN_DAMAGE, 
                Config.Actions.Passive.MAX_DAMAGE, 
                Config.Actions.Passive.IGNORES_BLOCK) },
                
            { ActionType.Jab, new ActionData(ActionType.Jab, 
                Config.Actions.Jab.STAMINA_COST, 
                Config.Actions.Jab.FRAME_DURATION, 
                Config.Actions.Jab.DODGE_CHANCE, 
                Config.Actions.Jab.MIN_DAMAGE, 
                Config.Actions.Jab.MAX_DAMAGE, 
                Config.Actions.Jab.IGNORES_BLOCK) },
                
            { ActionType.Hook, new ActionData(ActionType.Hook, 
                Config.Actions.Hook.STAMINA_COST, 
                Config.Actions.Hook.FRAME_DURATION, 
                Config.Actions.Hook.DODGE_CHANCE, 
                Config.Actions.Hook.MIN_DAMAGE, 
                Config.Actions.Hook.MAX_DAMAGE, 
                Config.Actions.Hook.IGNORES_BLOCK) },
                
            { ActionType.Dodge, new ActionData(ActionType.Dodge, 
                Config.Actions.Dodge.STAMINA_COST, 
                Config.Actions.Dodge.FRAME_DURATION, 
                Config.Actions.Dodge.DODGE_CHANCE, 
                Config.Actions.Dodge.MIN_DAMAGE, 
                Config.Actions.Dodge.MAX_DAMAGE, 
                Config.Actions.Dodge.IGNORES_BLOCK) },
                
            { ActionType.Block, new ActionData(ActionType.Block, 
                Config.Actions.Block.STAMINA_COST, 
                Config.Actions.Block.FRAME_DURATION, 
                Config.Actions.Block.DODGE_CHANCE, 
                Config.Actions.Block.MIN_DAMAGE, 
                Config.Actions.Block.MAX_DAMAGE, 
                Config.Actions.Block.IGNORES_BLOCK) },
                
            { ActionType.Concussion, new ActionData(ActionType.Concussion, 
                Config.Actions.Concussion.STAMINA_COST, 
                Config.Actions.Concussion.FRAME_DURATION, 
                Config.Actions.Concussion.DODGE_CHANCE, 
                Config.Actions.Concussion.MIN_DAMAGE, 
                Config.Actions.Concussion.MAX_DAMAGE, 
                Config.Actions.Concussion.IGNORES_BLOCK) }
        };

        public static ActionData GetAction(ActionType type)
        {
            return actions[type];
        }
    }
}