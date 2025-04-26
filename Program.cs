using board;
using Chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new(8, 8);

                board.PlacePiece(new King(board, Color.Black), new Position(0, 0));
                board.PlacePiece(new Tower(board, Color.Black), new Position(0, 0));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}