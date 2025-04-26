namespace board
{
  class Board
  {
    public int Rows { get; set; }
    public int Columns { get; set; }
    private Piece[,] Pieces;

    public Board(int rows, int columns)
    {
      Rows = rows;
      Columns = columns;
      Pieces = new Piece[rows, columns];
    }

    public Piece? GetPiece(Position position)
    {
      return Pieces[position.Row, position.Column];
    }

    public void PlacePiece(Piece piece, Position position)
    {
      Pieces[position.Row, position.Column] = piece;
    }
  }
}