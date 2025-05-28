using board;

namespace chess
{
  class Knight : Piece
  {
    public Knight(Board board, Color color) : base(color, board)
    {
    }

    public override string ToString()
    {
      return "N";
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

      int[] rows = { -2, -1, 1, 2, 2, 1, -1, -2 };
      int[] cols = { -1, -2, -2, -1, 1, 2, 2, 1 };

      for (int i = 0; i < rows.Length; i++)
      {
        pos.SetValues(Position!.Row + rows[i], Position.Column + cols[i]);
        if (Board.IsValidPosition(pos) && CanMove(pos))
        {
          mat[pos.Row, pos.Column] = true;
        }
      }

      return mat;
    }
  }
}