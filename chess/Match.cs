using board;

namespace chess
{
  class Match
  {
    public Board board { get; private set; }
    private int turn;
    private Color currentPlayer;
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

      board.PlacePiece(new King(board, Color.Black), new Position('c', 7).ToBoardPosition());
      board.PlacePiece(new King(board, Color.Black), new Position('c', 8).ToBoardPosition());
    }
  }
}