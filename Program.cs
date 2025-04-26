using board;
using Chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new(8, 8);

            board.PlacePiece(new King(board, Color.Black), new Position(0, 0));
            board.PlacePiece(new Tower(board, Color.Black), new Position(1, 0));

            Screen.PrintBoard(board);
            Console.ReadLine();
        }
    }
}