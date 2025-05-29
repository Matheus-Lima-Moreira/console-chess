using board;

namespace chess
{
  class Pawn : Piece
  {
    private Match _match;

    public Pawn(Board board, Color color, Match match) : base(color, board)
    {
      _match = match;
    }

    public override string ToString()
    {
      return "P";
    }

    private bool CanMove(board.Position pos)
    {
      Piece? p = Board.GetPiece(pos);
      return p == null || p.Color != Color;
    }

    private bool CanMoveCapture(board.Position pos)
    {
      Piece? p = Board.GetPiece(pos);
      return p != null && p.Color != Color;
    }    

    private bool IsWithoutPiece(board.Position pos)
    {
      Piece? p = Board.GetPiece(pos);
      return p == null;
    }

    private bool CanEnPassant(board.Position pos)
    {
      if (Color == Color.White)
      {
        return pos.Row == Position!.Row - 1 && (pos.Column == Position.Column - 1 || pos.Column == Position.Column + 1) && Board.GetPiece(pos) is Pawn p && p.Color == Color.Black && p.QtdMoves == 1;
      }
      else
      {
        return pos.Row == Position!.Row + 1 && (pos.Column == Position.Column - 1 || pos.Column == Position.Column + 1) && Board.GetPiece(pos) is Pawn p && p.Color == Color.White && p.QtdMoves == 1;
      }
    }

    public override bool[,] PossibleMoves()
    {
      bool[,] mat = new bool[Board.Rows, Board.Columns];

      board.Position pos = new(0, 0);

      if (Color == Color.White)
      {
        pos.SetValues(Position!.Row - 1, Position.Column);
        if (Board.IsValidPosition(pos) && IsWithoutPiece(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 2, Position.Column);
        if (Board.IsValidPosition(pos) && IsWithoutPiece(pos) && QtdMoves == 0)
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 1, Position.Column - 1);
        if (Board.IsValidPosition(pos) && CanMoveCapture(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 1, Position.Column + 1);
        if (Board.IsValidPosition(pos) && CanMoveCapture(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        if (Position.Row == 3)
        {
          board.Position left = new(Position.Row, Position.Column - 1);
          if (Board.IsValidPosition(left) && CanMoveCapture(left) && Board.GetPiece(left) == _match.VulnerableEnPassant)
          {
            mat[left.Row - 1, left.Column] = true;
          }
          board.Position right = new(Position.Row, Position.Column + 1);
          if (Board.IsValidPosition(right) && CanMoveCapture(right) && Board.GetPiece(right) == _match.VulnerableEnPassant)
          {
            mat[right.Row - 1, right.Column] = true;
          }
        }
      }
      else
      {
        pos.SetValues(Position!.Row + 1, Position.Column);
        if (Board.IsValidPosition(pos) && IsWithoutPiece(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 2, Position.Column);
        if (Board.IsValidPosition(pos) && IsWithoutPiece(pos) && QtdMoves == 0)
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 1, Position.Column - 1);
        if (Board.IsValidPosition(pos) && CanMoveCapture(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 1, Position.Column + 1);
        if (Board.IsValidPosition(pos) && CanMoveCapture(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }
        
        if (Position.Row == 4)
        {
          board.Position left = new(Position.Row, Position.Column - 1);
          if (Board.IsValidPosition(left) && CanMoveCapture(left) && Board.GetPiece(left) == _match.VulnerableEnPassant)
          {
            mat[left.Row + 1, left.Column] = true;
          }
          board.Position right = new(Position.Row, Position.Column + 1);
          if (Board.IsValidPosition(right) && CanMoveCapture(right) && Board.GetPiece(right) == _match.VulnerableEnPassant)
          {
            mat[right.Row + 1, right.Column] = true;
          }
        }
      }

      return mat;
    }
  }
}