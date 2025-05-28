using board;

namespace chess
{
  class Pawn : Piece
  {
    public Pawn(Board board, Color color) : base(color, board)
    {
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

    public override bool[,] PossibleMoves()
    {
      bool[,] mat = new bool[Board.Rows, Board.Columns];

      board.Position pos = new(0, 0);

      if (Color == Color.White)
      {
        pos.SetValues(Position!.Row - 1, Position.Column);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 2, Position.Column);
        if (Board.IsValidPosition(pos) && CanMove(pos) && QtdMoves == 0)
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 1, Position.Column - 1);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row - 1, Position.Column + 1);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }
      }
      else
      {
        pos.SetValues(Position!.Row + 1, Position.Column);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 2, Position.Column);
        if (Board.IsValidPosition(pos) && CanMove(pos) && QtdMoves == 0)
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 1, Position.Column - 1);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }

        pos.SetValues(Position.Row + 1, Position.Column + 1);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }
      }

      return mat;
    }
  }
}