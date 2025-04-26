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
                    Console.Clear();
                    Screen.PrintBoard(match.board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    board.Position origin = Screen.ReadChessPosition().ToBoardPosition();

                    Console.Write("Destination: ");
                    board.Position destination = Screen.ReadChessPosition().ToBoardPosition();

                    match.ExecuteMove(origin, destination);
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