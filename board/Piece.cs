namespace board
{
  abstract class Piece
  {
    public Position? Position { get; set; }
    public Color Color { get; protected set; }
    public int QtdMoves { get; protected set; }
    public Board Board { get; protected set; }

    public Piece(Color color, Board board)
    {
      Position = null;
      Color = color;
      Board = board;
      QtdMoves = 0;
    }

    public void IncrementQtdMoves()
    {
      QtdMoves++;
    }

    public bool IsThereAnyPossibleMove()
    {
      bool[,] mat = PossibleMoves();
      for (int i = 0; i < Board.Rows; i++)
      {
        for (int j = 0; j < Board.Columns; j++)
        {
          if (mat[i, j])
          {
            return true;
          }
        }
      }
      return false;
    }

    public bool CanMoveTo(Position pos)
    {
      return PossibleMoves()[pos.Row, pos.Column];
    }

    public abstract bool[,] PossibleMoves();
  }
}