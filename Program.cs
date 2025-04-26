using board;
using chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.board);
                        Console.WriteLine();
                        Console.WriteLine($"Turn: {match.turn}");
                        Console.WriteLine($"Waiting for player: {match.currentPlayer}");

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        board.Position origin = Screen.ReadChessPosition().ToBoardPosition();
                        match.ValidateOriginPosition(origin);

                        Console.Clear();
                        bool[,] possibleMoves = match.board.GetPiece(origin)!.PossibleMoves();
                        Screen.PrintBoard(match.board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        board.Position destination = Screen.ReadChessPosition().ToBoardPosition();
                        match.ValidateDestinationPosition(origin, destination);

                        match.RealizeMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}