namespace BoxingGame
{
    public static class Config
    {
        // ═══════════════════════════════════════════════════════════════
        // GAME SETTINGS
        // ═══════════════════════════════════════════════════════════════
        public const int TARGET_FPS = 30;
        public const int FRAME_TIME_MS = 1000 / TARGET_FPS;

        // ═══════════════════════════════════════════════════════════════
        // PLAYER SETTINGS
        // ═══════════════════════════════════════════════════════════════
        public const int STARTING_HEALTH = 100;
        public const int STARTING_STAMINA = 100;
        public const int MAX_HEALTH = 100;
        public const int MAX_STAMINA = 100;
        public const int PASSIVE_STAMINA_REGEN = 1;

        // ═══════════════════════════════════════════════════════════════
        // COMBAT SETTINGS
        // ═══════════════════════════════════════════════════════════════
        // if damage treshhold is reached within the frame window, the players state changes to concussion
        public const int CONCUSSION_DAMAGE_THRESHOLD = 25;  
        public const int CONCUSSION_FRAME_WINDOW = 20;       
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
        public static class Actions
        {
            public static class Passive
            {
                public const int STAMINA_COST = -1;
                public const int FRAME_DURATION = 1;
                public const int DODGE_CHANCE = 10;
                public const int MIN_DAMAGE = 0;
                public const int MAX_DAMAGE = 0;
                public const bool IGNORES_BLOCK = false;
            }

            public static class Jab
            {
                public const int STAMINA_COST = 20;
                public const int FRAME_DURATION = 6;
                public const int DODGE_CHANCE = 0;
                public const int MIN_DAMAGE = 5;
                public const int MAX_DAMAGE = 20;
                public const bool IGNORES_BLOCK = false;
            }

            public static class Hook
            {
                public const int STAMINA_COST = 40;
                public const int FRAME_DURATION = 8;
                public const int DODGE_CHANCE = 0;
                public const int MIN_DAMAGE = 5;
                public const int MAX_DAMAGE = 20;
                public const bool IGNORES_BLOCK = true;
            }

            public static class Dodge
            {
                public const int STAMINA_COST = 10;
                public const int FRAME_DURATION = 6;
                public const int DODGE_CHANCE = 50;
                public const int MIN_DAMAGE = 0;
                public const int MAX_DAMAGE = 0;
                public const bool IGNORES_BLOCK = false;
            }

            public static class Block
            {
                public const int STAMINA_COST = 10;
                public const int FRAME_DURATION = 6;
                public const int DODGE_CHANCE = 10;
                public const int MIN_DAMAGE = 0;
                public const int MAX_DAMAGE = 0;
                public const bool IGNORES_BLOCK = false;
            }

            public static class Concussion
            {
                public const int STAMINA_COST = 0;
                public const int FRAME_DURATION = 10;
                public const int DODGE_CHANCE = 0;
                public const int MIN_DAMAGE = 0;
                public const int MAX_DAMAGE = 0;
                public const bool IGNORES_BLOCK = false;
            }
        }
    }
}