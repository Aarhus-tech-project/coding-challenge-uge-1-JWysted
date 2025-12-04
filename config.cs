namespace BoxingGame
{
    public static class Config
    {
        // ═══════════════════════════════════════════════════════════════
        // GAME SETTINGS
        // ═══════════════════════════════════════════════════════════════
        
        
        //works best on higher fps, be aware that balancing is currently tied to framerate
        public const int TARGET_FPS = 240; 
        public const int FRAME_TIME_MS = 1000 / TARGET_FPS;

        // ═══════════════════════════════════════════════════════════════
        // PLAYER SETTINGS
        // ═══════════════════════════════════════════════════════════════
        public const int STARTING_HEALTH = 100;
        public const int STARTING_STAMINA = 100;
        public const int MAX_STAMINA = 100;

        // ═══════════════════════════════════════════════════════════════
        // COMBAT SETTINGS
        // ═══════════════════════════════════════════════════════════════
        // if damage treshhold is reached within the frame window, the players state changes to concussion
        public const int CONCUSSION_DAMAGE_THRESHOLD = 25;  
        public const int CONCUSSION_FRAME_WINDOW = 50;       
        public const double BLOCK_DAMAGE_REDUCTION = 0.5;    

        // ═══════════════════════════════════════════════════════════════
        // PLAYER 1 CONTROLS
        // ═══════════════════════════════════════════════════════════════
        public const ConsoleKey P1_JAB = ConsoleKey.W;
        public const ConsoleKey P1_DODGE = ConsoleKey.A;
        public const ConsoleKey P1_BLOCK = ConsoleKey.S;
        public const ConsoleKey P1_HOOK = ConsoleKey.D;

        // ═══════════════════════════════════════════════════════════════
        // PLAYER 2 CONTROLS
        // ═══════════════════════════════════════════════════════════════
        public const ConsoleKey P2_JAB = ConsoleKey.I;
        public const ConsoleKey P2_DODGE = ConsoleKey.J;
        public const ConsoleKey P2_BLOCK = ConsoleKey.K;
        public const ConsoleKey P2_HOOK = ConsoleKey.L;

        // ═══════════════════════════════════════════════════════════════
        // ACTION DATA (Stamina, Frames, Dodge%, MinDmg, MaxDmg)
        // ═══════════════════════════════════════════════════════════════
 
    public static class ActionDatabase
    {
        public static readonly ActionData Passive = new(
            type: ActionType.Passive,
            staminaCost: -1,
            frameDuration: 1,
            dodgeChance: 10,
            minDamage: 0,
            maxDamage: 0,
            ignoresBlock: false
        );

        public static readonly ActionData Jab = new(
            type: ActionType.Jab,
            staminaCost: 20,
            frameDuration: 10,
            dodgeChance: 0,
            minDamage: 5,
            maxDamage: 20,
            ignoresBlock: false
        );

        public static readonly ActionData Hook = new(
            type: ActionType.Hook,
            staminaCost: 40,
            frameDuration: 20,
            dodgeChance: 0,
            minDamage: 5,
            maxDamage: 20,
            ignoresBlock: true
        );

        public static readonly ActionData Dodge = new(
            type: ActionType.Dodge,
            staminaCost: 10,
            frameDuration: 20,
            dodgeChance: 50,
            minDamage: 0,
            maxDamage: 0,
            ignoresBlock: false
        );

        public static readonly ActionData Block = new(
            type: ActionType.Block,
            staminaCost: 10,
            frameDuration: 20,
            dodgeChance: 10,
            minDamage: 0,
            maxDamage: 0,
            ignoresBlock: false
        );

        public static readonly ActionData Concussion = new(
            type: ActionType.Concussion,
            staminaCost: 0,
            frameDuration: 40,
            dodgeChance: 0,
            minDamage: 0,
            maxDamage: 0,
            ignoresBlock: false
        );

        private static readonly Dictionary<ActionType, ActionData> _actionsByType = new()
        {
            { ActionType.Passive, Passive },
            { ActionType.Jab, Jab },
            { ActionType.Hook, Hook },
            { ActionType.Dodge, Dodge },
            { ActionType.Block, Block },
            { ActionType.Concussion, Concussion }
        };

        public static readonly IReadOnlyList<ActionData> All = 
            [Passive, Jab, Hook, Dodge, Block, Concussion];
        

        public static ActionData GetAction(ActionType type)
        {
            return _actionsByType[type];
        }

        public static bool TryGetAction(ActionType type, out ActionData? action)
        {
            return _actionsByType.TryGetValue(type, out action);
        }
    }
}
}