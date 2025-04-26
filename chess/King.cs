using board;

namespace chess
{
  class King : Piece
  {
    public King(Board board, Color color) : base(color, board)
    {
    }

    public override string ToString()
    {
      return "K";
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

      // up
      pos.SetValues(Position!.Row - 1, Position!.Column);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // ne
      pos.SetValues(Position!.Row - 1, Position!.Column + 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // right
      pos.SetValues(Position!.Row, Position!.Column + 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // se
      pos.SetValues(Position!.Row + 1, Position!.Column + 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // down
      pos.SetValues(Position!.Row + 1, Position!.Column);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // sw
      pos.SetValues(Position!.Row + 1, Position!.Column - 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // left
      pos.SetValues(Position!.Row, Position!.Column - 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      // nw
      pos.SetValues(Position!.Row - 1, Position!.Column - 1);
      if (Board.IsValidPosition(pos) && CanMove(pos))
      {
        mat[pos.Row, pos.Column] = true;
      }
      return mat;
    }
  }
}