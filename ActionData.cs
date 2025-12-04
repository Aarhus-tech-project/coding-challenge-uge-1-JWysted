namespace BoxingGame
{
    public class ActionData(ActionType type, int staminaCost, int frameDuration,
                     int dodgeChance, int minDamage, int maxDamage, bool ignoresBlock = false)
    {
        public ActionType Type { get; set; } = type;
        public int StaminaCost { get; set; } = staminaCost;
        public int FrameDuration { get; set; } = frameDuration;
        public int DodgeChance { get; set; } = dodgeChance;
        public int MinDamage { get; set; } = minDamage;
        public int MaxDamage { get; set; } = maxDamage;
        public bool IgnoresBlock { get; set; } = ignoresBlock;
    }
}

