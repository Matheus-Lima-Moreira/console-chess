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

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        board.Position origin = Screen.ReadChessPosition().ToBoardPosition();
                        match.ValidateOriginPosition(origin);

                        Console.Clear();
                        bool[,] possibleMoves = match.Board.GetPiece(origin)!.PossibleMoves();
                        Screen.PrintBoard(match.Board, possibleMoves);

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
                Screen.PrintMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}