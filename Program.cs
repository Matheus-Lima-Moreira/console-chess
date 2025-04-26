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
                Board board = new(8, 8);

                board.PlacePiece(new King(board, Color.Black), new board.Position(0, 5));
                board.PlacePiece(new Tower(board, Color.Black), new board.Position(0, 7));
                board.PlacePiece(new Tower(board, Color.Black), new board.Position(0, 0));

                board.PlacePiece(new King(board, Color.White), new board.Position(7, 5));
                board.PlacePiece(new Tower(board, Color.White), new board.Position(7, 7));
                board.PlacePiece(new Tower(board, Color.White), new board.Position(7, 0));

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