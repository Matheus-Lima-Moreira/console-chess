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
    public Piece? VulnerableEnPassant { get; private set; }

    public Match()
    {
      Board = new Board(8, 8);
      Turn = 1;
      CurrentPlayer = Color.White;
      Finished = false;
      Pieces = new HashSet<Piece>();
      CapturedPieces = new HashSet<Piece>();
      Check = false;
      VulnerableEnPassant = null;

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

      if (piece is King && destination.Column == origin.Column + 2) // Castling kingside
      {
        board.Position originTower = new(origin.Row, origin.Column + 3);
        board.Position destinationTower = new(origin.Row, origin.Column + 1);
        Piece? tower = Board.RemovePiece(originTower);
        tower!.IncrementQtdMoves();
        Board.PlacePiece(tower, destinationTower);
      }
      else if (piece is King && destination.Column == origin.Column - 2) // Castling queenside
      {
        board.Position originTower = new(origin.Row, origin.Column - 4);
        board.Position destinationTower = new(origin.Row, origin.Column - 1);
        Piece? tower = Board.RemovePiece(originTower);
        tower!.IncrementQtdMoves();
        Board.PlacePiece(tower, destinationTower);
      }

      if (piece is Pawn)
      {
        if (origin.Column != destination.Column && capturedPiece == null)
        {
          board.Position posP;
          if (piece.Color == Color.White)
          {
            posP = new board.Position(destination.Row + 1, destination.Column);
          }
          else
          {
            posP = new board.Position(destination.Row - 1, destination.Column);
          }
          capturedPiece = Board.RemovePiece(posP);
          if (capturedPiece != null)
          {
            CapturedPieces.Add(capturedPiece);
          }
        }
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

      if (piece is King && destination.Column == origin.Column + 2) // Undo Castling kingside
      {
        board.Position originTower = new(origin.Row, origin.Column + 3);
        board.Position destinationTower = new(origin.Row, origin.Column + 1);
        Piece? tower = Board.RemovePiece(destinationTower);
        tower!.DecrementQtdMoves();
        Board.PlacePiece(tower, originTower);
      }
      else if (piece is King && destination.Column == origin.Column - 2) // Undo Castling queenside
      {
        board.Position originTower = new(origin.Row, origin.Column - 4);
        board.Position destinationTower = new(origin.Row, origin.Column - 1);
        Piece? tower = Board.RemovePiece(destinationTower);
        tower!.DecrementQtdMoves();
        Board.PlacePiece(tower, originTower);
      }

      if (piece is Pawn)
      {
        if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
        {
          Piece pawn = Board.RemovePiece(destination)!;
          board.Position posP;
          if (piece.Color == Color.White)
          {
            posP = new board.Position(3, destination.Column);
          }
          else
          {
            posP = new board.Position(4, destination.Column);
          }
          Board.PlacePiece(pawn!, posP);
        }
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
      PutNewPiece('e', 1, new King(Board, Color.White, this));
      PutNewPiece('f', 1, new Bishop(Board, Color.White));
      PutNewPiece('g', 1, new Knight(Board, Color.White));
      PutNewPiece('h', 1, new Tower(Board, Color.White));
      PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
      PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

      PutNewPiece('a', 8, new Tower(Board, Color.Black));
      PutNewPiece('b', 8, new Knight(Board, Color.Black));
      PutNewPiece('c', 8, new Bishop(Board, Color.Black));
      PutNewPiece('d', 8, new Queen(Board, Color.Black));
      PutNewPiece('e', 8, new King(Board, Color.Black, this));
      PutNewPiece('f', 8, new Bishop(Board, Color.Black));
      PutNewPiece('g', 8, new Knight(Board, Color.Black));
      PutNewPiece('h', 8, new Tower(Board, Color.Black));
      PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
      PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
    }

    public void RealizeMove(board.Position origin, board.Position destination)
    {
      Piece? capturedPiece = ExecuteMove(origin, destination);
      Piece p = Board.GetPiece(destination)!;

      if (IsCheck(CurrentPlayer))
      {
        UndoMove(origin, destination, capturedPiece);
        throw new BoardException("You cannot put yourself in check!");
      }

      if (p is Pawn)
      {
        if ((p.Color == Color.White && destination.Row == 0) || (p.Color == Color.Black && destination.Row == 7))
        {
          p = Board.RemovePiece(destination)!;
          Pieces.Remove(p);
          Piece newPiece = new Queen(Board, p.Color);
          Board.PlacePiece(newPiece, destination);
          Pieces.Add(newPiece);
        }
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

      if (p is Pawn && (destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2))
      {
        VulnerableEnPassant = p;
      }
      else
      {
        VulnerableEnPassant = null;
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