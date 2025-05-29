using board;

namespace chess
{
  class King : Piece
  {
    private Match _match;

    public King(Board board, Color color, Match match) : base(color, board)
    {
      _match = match;
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

    private bool CanCastling(board.Position pos)
    {
      Piece? p = Board.GetPiece(pos);
      return p != null && p is Tower && p.Color == Color && p.QtdMoves == 0;
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

      // Castling
      if (QtdMoves == 0 && !_match.Check)
      {
        // Kingside castling
        board.Position rookPos = new(Position.Row, Position.Column + 3);
        if (CanCastling(rookPos))
        {
          board.Position pos1 = new(Position.Row, Position.Column + 1);
          board.Position pos2 = new(Position.Row, Position.Column + 2);
          if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null)
          {
            mat[Position.Row, Position.Column + 2] = true;
          }
        }
        // Queenside castling
        rookPos.SetValues(Position.Row, Position.Column - 4);
        if (CanCastling(rookPos))
        {
          board.Position pos1 = new(Position.Row, Position.Column - 1);
          board.Position pos2 = new(Position.Row, Position.Column - 2);
          board.Position pos3 = new(Position.Row, Position.Column - 3);
          if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null && Board.GetPiece(pos3) == null)
          {
            mat[Position.Row, Position.Column - 2] = true;
          }
        }
      }

      return mat;
    }
  }
}