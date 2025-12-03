
namespace BoxingGame
{
    public class InputHandler
    {
        public void ProcessInput(Player player1, Player player2)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                // Player 1 controls
                if (key.Key == Config.P1_JAB)
                    player1.SetQueuedAction(ActionType.Jab);
                else if (key.Key == Config.P1_DODGE)
                    player1.SetQueuedAction(ActionType.Dodge);
                else if (key.Key == Config.P1_BLOCK)
                    player1.SetQueuedAction(ActionType.Block);
                else if (key.Key == Config.P1_HOOK)
                    player1.SetQueuedAction(ActionType.Hook);

                // Player 2 controls
                if (key.Key == Config.P2_JAB)
                    player2.SetQueuedAction(ActionType.Jab);
                else if (key.Key == Config.P2_DODGE)
                    player2.SetQueuedAction(ActionType.Dodge);
                else if (key.Key == Config.P2_BLOCK)
                    player2.SetQueuedAction(ActionType.Block);
                else if (key.Key == Config.P2_HOOK)
                    player2.SetQueuedAction(ActionType.Hook);
            }
        }
    }
}
