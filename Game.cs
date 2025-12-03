namespace BoxingGame
{
    public enum GameState
    {
        Menu,
        Game,
        WinScreen
    }

    public class Game
    {
        private GameState currentState;
        private Player player1 = null!;
        private Player player2 = null!;
        private readonly Renderer renderer;
        private readonly InputHandler inputHandler;
        private int currentFrame = 0;

        public Game()
        {
            currentState = GameState.Menu;
            renderer = new Renderer();
            inputHandler = new InputHandler();
        }

        public void Run()
        {
            while (true)
            {
                switch (currentState)
                {
                    case GameState.Menu:
                        ShowMenu();
                        break;
                    case GameState.Game:
                        GameLoop();
                        break;
                    case GameState.WinScreen:
                        ShowWinScreen();
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.Clear();
            Renderer.RenderMenu();
            Console.WriteLine("\nTryk ENTER for at starte spillet");
            Console.WriteLine("ESC for at afslutte");
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                InitializeGame();
                currentState = GameState.Game;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        private void InitializeGame()
        {
            player1 = new Player("Bokser 1", PlayerNumber.Player1);
            player2 = new Player("Bokser 2", PlayerNumber.Player2);
            currentFrame = 0;
        }

        private void GameLoop()
        {

            while (currentState == GameState.Game)
            {
                DateTime frameStart = DateTime.Now;

                InputHandler.ProcessInput(player1, player2);

                UpdateGame();

                Console.Clear();
                renderer.RenderGame(player1, player2, currentFrame);

                CheckWinCondition();

                currentFrame++;
                int elapsedMs = (int)(DateTime.Now - frameStart).TotalMilliseconds;
                int sleepTime = Math.Max(0, Config.FRAME_TIME_MS - elapsedMs);
                Thread.Sleep(sleepTime);
            }
        }

        private void UpdateGame()
        {
            player1.Update();
            player2.Update();

            ResolveCombat();
        }

        private void ResolveCombat()
        {

            if (player1.IsActionComplete())
            {
                ProcessAttack(player1, player2);
            }

            if (player2.IsActionComplete())
            {
                ProcessAttack(player2, player1);
            }
        }

        private void ProcessAttack(Player attacker, Player defender)
        {
            var attackAction = attacker.GetCurrentActionType();
            
            if (attackAction == ActionType.Jab || attackAction == ActionType.Hook)
            {
                var defenderAction = defender.GetCurrentActionType();
                int damage = attacker.CalculateDamage();

                // Check if defender is dodging
                if (defenderAction == ActionType.Dodge)
                {
                    var defenderActionData = ActionDatabase.GetAction(defenderAction);
                    Random rand = new Random();
                    if (rand.Next(100) < defenderActionData.DodgeChance) 
                    {
                        return;
                    }
                }

                // Check if defender is blocking
                var attackActionData = ActionDatabase.GetAction(attackAction);
                if (defenderAction == ActionType.Block && !attackActionData.IgnoresBlock)
                {
                    damage = (int)(damage * Config.BLOCK_DAMAGE_REDUCTION); 
                }

                defender.TakeDamage(damage, currentFrame);
            }
        }

        private void CheckWinCondition()
        {
            if (player1.Health <= 0)
            {
                currentState = GameState.WinScreen;
                renderer.SetWinner(player2.Name);
            }
            else if (player2.Health <= 0)
            {
                currentState = GameState.WinScreen;
                renderer.SetWinner(player1.Name);
            }
        }

        private void ShowWinScreen()
        {
            renderer.RenderWinScreen();
            Console.WriteLine("\nTryk ENTER for at vende tilbage til menuen");
            Console.WriteLine("ESC for at afslutte");

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                currentState = GameState.Menu;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
