using board;

namespace chess
{
  class Match
  {
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool Finished { get; private set; }
    public HashSet<Piece> Pieces { get; set; }
    public HashSet<Piece> CapturedPieces { get; set; }
    public bool Check { get; private set; }

    public Match()
    {
      Board = new Board(8, 8);
      Turn = 1;
      CurrentPlayer = Color.White;
      Finished = false;
      Pieces = new HashSet<Piece>();
      CapturedPieces = new HashSet<Piece>();
      Check = false;

      PutPieces();
    }

    public Piece? ExecuteMove(board.Position origin, board.Position destination)
    {
      Piece? piece = Board.RemovePiece(origin);
      piece!.IncrementQtdMoves();
      Piece? capturedPiece = Board.RemovePiece(destination);
      Board.PlacePiece(piece, destination);
      if (capturedPiece != null)
      {
        CapturedPieces.Add(capturedPiece);
      }
      return capturedPiece;
    }

    public void UndoMove(board.Position origin, board.Position destination, Piece? capturedPiece)
    {
      Piece? piece = Board.RemovePiece(destination);
      piece!.DecrementQtdMoves();
      if (capturedPiece != null)
      {
        Board.PlacePiece(capturedPiece, destination);
        CapturedPieces.Remove(capturedPiece);
      }
      Board.PlacePiece(piece, origin);
    }

    public HashSet<Piece> GetCapturedPieces(Color color)
    {
      HashSet<Piece> aux = new HashSet<Piece>();
      foreach (Piece p in CapturedPieces)
      {
        if (p.Color == color)
        {
          aux.Add(p);
        }
      }
      return aux;
    }

    public HashSet<Piece> GetPiecesInGame(Color color)
    {
      HashSet<Piece> aux = new HashSet<Piece>();
      foreach (Piece p in Pieces)
      {
        if (p.Color == color)
        {
          aux.Add(p);
        }
      }
      aux.ExceptWith(GetCapturedPieces(color));
      return aux;
    }

    private static Color GetOpponent(Color color)
    {
      if (color == Color.White)
      {
        return Color.Black;
      }
      else
      {
        return Color.White;
      }
    }

    private Piece? GetKing(Color color)
    {
      foreach (Piece p in GetPiecesInGame(color))
      {
        if (p is King)
        {
          return p;
        }
      }
      return null;
    }

    public bool IsCheck(Color color)
    {
      Piece? king = GetKing(color);
      if (king == null || king.Position == null)
      {
        throw new BoardException("There is no " + color + " king on the board!");
      }
      foreach (Piece p in GetPiecesInGame(GetOpponent(color)))
      {
        bool[,] mat = p.PossibleMoves();
        if (mat[king.Position.Row, king.Position.Column])
        {
          return true;
        }
      }
      return false;
    }

    public bool IsCheckMate(Color color)
    {
      if (!IsCheck(color))
      {
        return false;
      }
      foreach (Piece p in GetPiecesInGame(color))
      {
        bool[,] mat = p.PossibleMoves();
        for (int i = 0; i < Board.Rows; i++)
        {
          for (int j = 0; j < Board.Columns; j++)
          {
            if (mat[i, j])
            {
              board.Position origin = p.Position!;
              board.Position destination = new(i, j);
              Piece? capturedPiece = ExecuteMove(origin, destination);
              bool isCheck = IsCheck(color);
              UndoMove(origin, destination, capturedPiece);
              if (!isCheck)
              {
                return false;
              }
            }
          }
        }
      }
      return true;
    }

    public void PutNewPiece(char column, int row, Piece piece)
    {
      Board.PlacePiece(piece, new Position(column, row).ToBoardPosition());
      Pieces.Add(piece);
    }

    public void PutPieces()
    {
      PutNewPiece('a', 1, new Tower(Board, Color.White));
      PutNewPiece('b', 1, new Knight(Board, Color.White));
      PutNewPiece('c', 1, new Bishop(Board, Color.White));
      PutNewPiece('d', 1, new Queen(Board, Color.White));
      PutNewPiece('e', 1, new King(Board, Color.White));
      PutNewPiece('f', 1, new Bishop(Board, Color.White));
      PutNewPiece('g', 1, new Knight(Board, Color.White));
      PutNewPiece('h', 1, new Tower(Board, Color.White));
      PutNewPiece('a', 2, new Pawn(Board, Color.White));
      PutNewPiece('b', 2, new Pawn(Board, Color.White));
      PutNewPiece('c', 2, new Pawn(Board, Color.White));
      PutNewPiece('d', 2, new Pawn(Board, Color.White));
      PutNewPiece('e', 2, new Pawn(Board, Color.White));
      PutNewPiece('f', 2, new Pawn(Board, Color.White));
      PutNewPiece('g', 2, new Pawn(Board, Color.White));
      PutNewPiece('h', 2, new Pawn(Board, Color.White));

      PutNewPiece('a', 8, new Tower(Board, Color.Black));
      PutNewPiece('b', 8, new Knight(Board, Color.Black));
      PutNewPiece('c', 8, new Bishop(Board, Color.Black));
      PutNewPiece('d', 8, new Queen(Board, Color.Black));
      PutNewPiece('e', 8, new King(Board, Color.Black));
      PutNewPiece('f', 8, new Bishop(Board, Color.Black));
      PutNewPiece('g', 8, new Knight(Board, Color.Black));
      PutNewPiece('h', 8, new Tower(Board, Color.Black));
      PutNewPiece('a', 7, new Pawn(Board, Color.Black));
      PutNewPiece('b', 7, new Pawn(Board, Color.Black));
      PutNewPiece('c', 7, new Pawn(Board, Color.Black));
      PutNewPiece('d', 7, new Pawn(Board, Color.Black));
      PutNewPiece('e', 7, new Pawn(Board, Color.Black));
      PutNewPiece('f', 7, new Pawn(Board, Color.Black));
      PutNewPiece('g', 7, new Pawn(Board, Color.Black));
      PutNewPiece('h', 7, new Pawn(Board, Color.Black));
    }

    public void RealizeMove(board.Position origin, board.Position destination)
    {
      Piece? capturedPiece = ExecuteMove(origin, destination);

      if (IsCheck(CurrentPlayer))
      {
        UndoMove(origin, destination, capturedPiece);
        throw new BoardException("You cannot put yourself in check!");
      }

      if (IsCheck(GetOpponent(CurrentPlayer)))
      {
        Check = true;
      }
      else
      {
        Check = false;
      }

      if (IsCheckMate(GetOpponent(CurrentPlayer)))
      {
        Finished = true;
      }
      else
      {
        Turn++;
        ChangePlayer();
      }
    }

    public void ChangePlayer()
    {
      if (CurrentPlayer == Color.White)
      {
        CurrentPlayer = Color.Black;
      }
      else
      {
        CurrentPlayer = Color.White;
      }
    }

    public void ValidateOriginPosition(board.Position position)
    {
      if (Board.GetPiece(position) == null)
      {
        throw new BoardException("No piece on the origin position!");
      }
      if (CurrentPlayer != Board.GetPiece(position)!.Color)
      {
        throw new BoardException("The chosen piece is not yours!");
      }
      if (!Board.GetPiece(position)!.IsThereAnyPossibleMove())
      {
        throw new BoardException("There are no possible moves for the chosen piece!");
      }
    }

    public void ValidateDestinationPosition(board.Position origin, board.Position destination)
    {
      if (!Board.GetPiece(origin)!.CanMoveTo(destination))
      {
        throw new BoardException("Invalid destination position!");
      }
    }
  }
}