using board;

namespace chess
{
  class Bishop : Piece
  {
    public Bishop(Board board, Color color) : base(color, board)
    {
    }

    public override string ToString()
    {
      return "B";
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

      return mat;
    }
  }
}