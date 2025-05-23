using board;

namespace console_chess
{
  class Screen
  {
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

    public static void PrintBoard(Board board, bool[,] possibleMoves)
    {
      ConsoleColor originalBackground = Console.BackgroundColor;
      ConsoleColor highlightedBackground = ConsoleColor.DarkGray;

      for (int r = 0; r < board.Rows; r++)
      {
        Console.Write(8 - r + " ");
        for (int c = 0; c < board.Columns; c++)
        {
          if (possibleMoves[r, c])
          {
            Console.BackgroundColor = highlightedBackground;
          }
          else
          {
            Console.BackgroundColor = originalBackground;
          }

          PrintPiece(board.GetPiece(new Position(r, c)));

          Console.BackgroundColor = originalBackground;
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
        if (piece.Color == Color.White)
        {
          Console.Write(piece);
        }
        else
        {
          ConsoleColor aux = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write(piece);
          Console.ForegroundColor = aux;
        }
        Console.Write(" ");
      }
    }

    public static chess.Position ReadChessPosition()
    {
      string s = Console.ReadLine()!;
      char column = s[0];
      int row = int.Parse(s[1] + "");
      return new chess.Position(column, row);
    }
  }
}