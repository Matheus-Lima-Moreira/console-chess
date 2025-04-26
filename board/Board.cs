namespace board
{
  class Board
  {
    public int Rows { get; set; }
    public int Columns { get; set; }
    private Piece?[,] Pieces;

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
      if (ExistsPiece(position))
      {
        throw new BoardException("There is already a piece in this position!");
      }
      Pieces[position.Row, position.Column] = piece;
      piece.Position = position;
    }

    public Piece? RemovePiece(Position position)
    {
      if (GetPiece(position) == null)
      {
        return null;
      }
      Piece piece = GetPiece(position)!;
      piece.Position = null;
      Pieces[position.Row, position.Column] = null;
      return piece;
    }

    public bool IsValidPosition(Position position)
    {
      return position.Row >= 0 && position.Row < Rows && position.Column >= 0 && position.Column < Columns;
    }

    public bool ExistsPiece(Position position)
    {
      ValidatePosition(position);
      return GetPiece(position) != null;
    }

    public bool ValidatePosition(Position position)
    {
      if (!IsValidPosition(position))
      {
        throw new BoardException("Invalid position!");
      }
      return true;
    }
  }
}