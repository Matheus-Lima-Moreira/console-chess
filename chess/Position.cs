using board;

namespace chess
{
  class Position
  {
    public char Column { get; set; }
    public int Row { get; set; }

    public Position(char column, int row)
    {
      this.Column = column;
      this.Row = row;
    }

    public override string ToString()
    {
      return "" + Column + Row;
    }

    public board.Position ToBoardPosition()
    {
      return new board.Position(8 - Row, Column - 'a');
    }
  }
}