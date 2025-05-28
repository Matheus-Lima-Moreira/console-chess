using board;

namespace chess
{
  class Queen : Piece
  {
    public Queen(Board board, Color color) : base(color, board)
    {
    }

    public override string ToString()
    {
      return "Q";
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

      // nw
      pos.SetValues(Position!.Row - 1, Position!.Column - 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row - 1, pos.Column - 1);
      }

      // ne
      pos.SetValues(Position!.Row - 1, Position!.Column + 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row - 1, pos.Column + 1);
      }

      // se
      pos.SetValues(Position!.Row + 1, Position!.Column + 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row + 1, pos.Column + 1);
      }

      // sw
      pos.SetValues(Position!.Row + 1, Position!.Column - 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row + 1, pos.Column - 1);
      }

      // up
      pos.SetValues(Position!.Row - 1, Position!.Column);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row - 1, pos.Column);
      }
      // down
      pos.SetValues(Position!.Row + 1, Position!.Column);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row + 1, pos.Column);
      }
      // left
      pos.SetValues(Position!.Row, Position!.Column - 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row, pos.Column - 1);
      }
      // right
      pos.SetValues(Position!.Row, Position!.Column + 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        if (Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
        {
          break;
        }
        pos.SetValues(pos.Row, pos.Column + 1);
      }
      return mat;
    }
  }
}