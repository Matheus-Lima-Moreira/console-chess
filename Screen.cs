using board;

namespace console_chess {
  class Screen {
    public static void PrintBoard(Board board)
    {
      for (int r = 0; r < board.Rows; r++)
      {
        Console.Write(8 - r + " ");
        for (int c = 0; c < board.Columns; c++)
        {
          PrintPiece(board.GetPiece(new Position(r, c)));
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
    }

    private static void PrintPiece(Piece? piece)
    {
      if (piece == null)
      {
        Console.Write("- ");
      }
      else
      {
        Console.Write(piece + " ");
      }
    }
  }
}