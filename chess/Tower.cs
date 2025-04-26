using board;

namespace chess
{
  class Tower : Piece
  {
    public Tower(Board board, Color color) : base(color, board)
    {
    }

    public override string ToString()
    {
      return "T";
    }

    private bool CanMove(board.Position pos)
    {
      Piece? p = Board.GetPiece(pos);
      return p == null || p.Color != Color;
    }

    public override bool[,] PossibleMoves()
    {
      bool[,] mat = new bool[Board.Rows, Board.Columns];

      board.Position pos = new board.Position(0, 0);

      // up
      pos.SetValues(Position!.Row - 1, Position!.Column);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        pos.SetValues(pos.Row - 1, pos.Column);
      }

      // down
      pos.SetValues(Position!.Row + 1, Position!.Column);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        pos.SetValues(pos.Row + 1, pos.Column);
      }

      // left
      pos.SetValues(Position!.Row, Position!.Column - 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        pos.SetValues(pos.Row, pos.Column - 1);
      }

      // right
      pos.SetValues(Position!.Row, Position!.Column + 1);
      while (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
        pos.SetValues(pos.Row, pos.Column + 1);
      }

      return mat;
    }
  }
}