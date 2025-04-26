using board;

namespace chess
{
  class Match
  {
    public Board board { get; private set; }
    public int turn { get; private set; }
    public Color currentPlayer { get; private set; }
    public bool finished { get; private set; }

    public Match()
    {
      board = new Board(8, 8);
      turn = 1;
      currentPlayer = Color.White;
      finished = false;

      PutPieces();
    }

    public void ExecuteMove(board.Position origin, board.Position destination)
    {
      Piece? piece = board.RemovePiece(origin);
      piece!.IncrementQtdMoves();
      Piece? capturedPiece = board.RemovePiece(destination);
      board.PlacePiece(piece, destination);
    }

    public void PutPieces()
    {
      board.PlacePiece(new King(board, Color.White), new Position('c', 1).ToBoardPosition());
      board.PlacePiece(new King(board, Color.White), new Position('c', 2).ToBoardPosition());
      board.PlacePiece(new King(board, Color.White), new Position('b', 1).ToBoardPosition());
      board.PlacePiece(new King(board, Color.White), new Position('b', 2).ToBoardPosition());
      board.PlacePiece(new Tower(board, Color.White), new Position('d', 1).ToBoardPosition());
      board.PlacePiece(new Tower(board, Color.White), new Position('d', 2).ToBoardPosition());

      board.PlacePiece(new King(board, Color.Black), new Position('c', 7).ToBoardPosition());
      board.PlacePiece(new King(board, Color.Black), new Position('c', 8).ToBoardPosition());
      board.PlacePiece(new King(board, Color.Black), new Position('d', 7).ToBoardPosition());
      board.PlacePiece(new King(board, Color.Black), new Position('d', 8).ToBoardPosition());
    }

    public void RealizeMove(board.Position origin, board.Position destination)
    {
      ExecuteMove(origin, destination);
      turn++;
      ChangePlayer();
    }

    public void ChangePlayer()
    {
      if (currentPlayer == Color.White)
      {
        currentPlayer = Color.Black;
      }
      else
      {
        currentPlayer = Color.White;
      }
    }

    public void ValidateOriginPosition(board.Position position)
    {
      if (board.GetPiece(position) == null)
      {
        throw new BoardException("No piece on the origin position!");
      }
      if (currentPlayer != board.GetPiece(position)!.Color)
      {
        throw new BoardException("The chosen piece is not yours!");
      }
      if (!board.GetPiece(position)!.IsThereAnyPossibleMove())
      {
        throw new BoardException("There are no possible moves for the chosen piece!");
      }
    }

    public void ValidateDestinationPosition(board.Position origin, board.Position destination)
    {
      if (!board.GetPiece(origin)!.CanMoveTo(destination))
      {
        throw new BoardException("Invalid destination position!");
      }
    }
  }
}