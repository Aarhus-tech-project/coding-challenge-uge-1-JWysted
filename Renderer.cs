using System.Text;
namespace BoxingGame
{
    public class Renderer
    {
        private string winner = "";
        private int lastFrameHeight = 0;
        private StringBuilder frameBuffer;

        public Renderer()
        {
            frameBuffer = new StringBuilder();
            Console.CursorVisible = false; 
        }

        public void RenderMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        BOKSE KAMP - HOVEDMENU          ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  Spiller 1 kontroller (WASD):");
            Console.WriteLine("    W - Jab");
            Console.WriteLine("    A - Dodge");
            Console.WriteLine("    S - Block");
            Console.WriteLine("    D - Hook");
            Console.WriteLine();
            Console.WriteLine("  Spiller 2 kontroller (IJKL):");
            Console.WriteLine("    I - Jab");
            Console.WriteLine("    J - Dodge");
            Console.WriteLine("    K - Block");
            Console.WriteLine("    L - Hook");
        }

        public void RenderGame(Player player1, Player player2, int frame)
        {
            frameBuffer.Clear();

    
            frameBuffer.AppendLine($"Frame: {frame}");
            frameBuffer.AppendLine("════════════════════════════════════════════════════════════");
            
            AppendPlayerStats(player1, true);
            frameBuffer.AppendLine();
            AppendPlayerStats(player2, false);
            frameBuffer.AppendLine();
            frameBuffer.AppendLine("════════════════════════════════════════════════════════════");
            
            AppendArena(player1, player2);
            
            frameBuffer.AppendLine("════════════════════════════════════════════════════════════");
            frameBuffer.AppendLine("ESC for at afslutte");

            int currentHeight = CountLines(frameBuffer.ToString());
            for (int i = currentHeight; i < lastFrameHeight; i++)
            {
                frameBuffer.AppendLine(new string(' ', 60));
            }
            lastFrameHeight = currentHeight;

            Console.SetCursorPosition(0, 0);
            Console.Write(frameBuffer.ToString());
        }

        private void AppendPlayerStats(Player player, bool isLeft)
        {
            string side = isLeft ? "VENSTRE" : "HØJRE";
            frameBuffer.AppendLine($"[{side}] {player.Name}");
            frameBuffer.AppendLine($"  Health:  [{GetBar(player.Health, 100, 20)}] {player.Health}/100");
            frameBuffer.AppendLine($"  Stamina: [{GetBar(player.Stamina, 100, 20)}] {player.Stamina}/100");
            
            string actionDisplay = GetActionDisplay(player.GetCurrentActionType(), player.GetActionFramesRemaining());
            frameBuffer.AppendLine($"  Action:  {actionDisplay}");
        }

        private string GetActionDisplay(ActionType action, int framesRemaining)
        {
            if (action == ActionType.Passive)
                return "Passive (Recovering)";
            
            string actionName = action.ToString();
            string frameBar = GetFrameBar(framesRemaining);
            return $"{actionName} {frameBar} ({framesRemaining}f)";
        }

        private string GetFrameBar(int frames)
        {
            const int maxBarLength = 5;
            int filled = Math.Min(frames, maxBarLength);
            return "[" + new string('▓', filled) + new string('░', maxBarLength - filled) + "]";
        }

        private string GetBar(int value, int max, int length)
        {
            int filled = (int)(value / (float)max * length);
            filled = Math.Max(0, Math.Min(filled, length)); 
            return new string('█', filled) + new string('░', length - filled);
        }

        private void AppendArena(Player player1, Player player2)
        {
            frameBuffer.AppendLine();
            frameBuffer.AppendLine("           BOKSERING           ");
            frameBuffer.AppendLine("  ┌─────────────────────────────┐");
            
            string[] sprite1Lines = GetSpriteLines(player1.GetCurrentActionType(), true);
            string[] sprite2Lines = GetSpriteLines(player2.GetCurrentActionType(), false);
            
            for (int i = 0; i < 3; i++)
            {
                frameBuffer.AppendLine($"  │      {sprite1Lines[i]}     {sprite2Lines[i]}        │");
            }
            
            frameBuffer.AppendLine("  └─────────────────────────────┘");
        }

        private string[] GetSpriteLines(ActionType action, bool facingRight)
        {
            return action switch
            {
                ActionType.Passive => new[] { " O   ", "/|\\  ", "/ \\  " },
                ActionType.Jab => facingRight 
                    ? new[] { " O   ", "/|-- ", "/ \\  " }
                    : new[] { " O   ", "--|\\ ", "/ \\  " },
                ActionType.Hook => facingRight 
                    ? new[] { " O   ", "/|== ", "/ \\  " }
                    : new[] { " O   ", "==|\\ ", "/ \\  " },
                ActionType.Dodge => new[]{ "     ", "-O-  ", "/ \\  " },
                ActionType.Block => new[] { "/O\\  ", " |   ", "/ \\  " },
                ActionType.Concussion => new[] { " @   ", "/X\\  ", "/ \\  " },
                _ => new[] { " ?  ", " ?  ", " ?  " }
            };
        }

        public void RenderWinScreen()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            KAMP AFSLUTTET!             ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine($"        VINDER: {winner}!");
            Console.WriteLine();
        }

        public void SetWinner(string winnerName)
        {
            winner = winnerName;
        }

        private int CountLines(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            
            int count = 1;
            foreach (char c in text)
            {
                if (c == '\n')
                    count++;
            }
            return count;
        }
    }
}
