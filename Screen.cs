using board;
using chess;

namespace console_chess
{
  class Screen
  {
    public static void PrintMatch(Match match)
    {
      PrintBoard(match.Board);
      PrintCapturedPieces(match);
      Console.WriteLine();
      Console.WriteLine($"Turn: {match.Turn}");
      Console.WriteLine($"Waiting for player: {match.CurrentPlayer}");
    }

    public static void PrintCapturedPieces(Match match)
    {
      Console.WriteLine("Captured pieces:");
      Console.Write("White: ");
      PrintSet(match.GetCapturedPieces(Color.White));
      Console.WriteLine();
      Console.Write("Black: ");
      ConsoleColor aux = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      PrintSet(match.GetCapturedPieces(Color.Black));
      Console.ForegroundColor = aux;
      Console.WriteLine();
    }

    public static void PrintSet(HashSet<Piece> set)
    {
      Console.Write("[");
      foreach (Piece p in set)
      {
        Console.Write(p + " ");
      }
      Console.Write("]");
    }

    public static void PrintBoard(Board board)
    {
      for (int r = 0; r < board.Rows; r++)
      {
        Console.Write(8 - r + " ");
        for (int c = 0; c < board.Columns; c++)
        {
          PrintPiece(board.GetPiece(new board.Position(r, c)));
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

          PrintPiece(board.GetPiece(new board.Position(r, c)));

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