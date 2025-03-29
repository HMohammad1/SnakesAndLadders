using System.Text;
using SnakesAndLadders.Interfaces;
using static System.Text.Json.JsonSerializer;

namespace SnakesAndLadders;



public class Board
{
    private bool _debugMode = true;
    
    private readonly int _boardSize;
    private readonly int _boardSpaces;
    private readonly int _boardRowLength;
    private readonly SortedList<int, string> _board;
    private readonly List<int> _oddEdgeNum;
    private readonly List<int> _evenEdgeNum;
    private bool _oddEdge;

    public Board(int boardSize)
    {
        _boardSize = boardSize;
        _oddEdge = false;
        _board = new SortedList<int, string>();
        _evenEdgeNum = [];
        BoardNum = _boardSize * _boardSize;
        _oddEdgeNum = [];
        _boardSpaces = 7 * _boardSize * _boardSize + 1;
        _boardRowLength = 7 * _boardSize;
    }

    public void Start()
    {
        BoardEdgeNum();
        CreateBoard();
        DisplayBoard();
    }
    
    private enum Direction
    {
        LeftToRight = 1,
        RightToLeft = -1
    }
    
    private int BoardNum { get; set; }

    private void AdvanceBoardNumber()
    {
        if (_oddEdgeNum.Contains(BoardNum) || _evenEdgeNum.Contains(BoardNum))
        {
            _oddEdge = _oddEdgeNum.Contains(BoardNum);
            BoardNum -= _boardSize;
            return;
        }

        var direction = _oddEdge ? Direction.LeftToRight : Direction.RightToLeft;
        BoardNum += (int)direction;
    }
    
    // TODO: 3 digit numbers (increase _boardRowWidth to 8)
    // TODO: add second empty row
    private void CreateBoard()
    {
        const int cellSize = 7;
        const int tensDigitCol = 5;
        const int unitsDigitCol = 6;
        const int edgeCol = 0;

        for (var i = 1; i < _boardSpaces; i++)
        {
            string cellContent;

            switch (i % cellSize)
            {
                case edgeCol:
                    cellContent = "|";
                    break;
                case tensDigitCol:
                    cellContent = (BoardNum / 10 > 0) ? (BoardNum / 10).ToString() : " ";
                    break;
                case unitsDigitCol:
                    cellContent = (BoardNum % 10).ToString();
                    AdvanceBoardNumber();
                    break;
                default:
                    cellContent = " ";
                    break;
            }

            _board.Add(i, cellContent);
        }

        //DisplayBoard();


        // Console.WriteLine();
        // var json = Serialize(_board);
        // Console.WriteLine(json);
    }

    private void DisplayBoard()
    {
        var sb = new StringBuilder();
    
        foreach (var i in _board)
        {
            if ((i.Key - 1) % _boardRowLength == 0)
            {
                sb.AppendLine();
                sb.AppendLine(new string('-', _boardRowLength));
            }
            sb.Append(i.Value);
        }
    
        Console.WriteLine(sb.ToString());
    }

    private void BoardEdgeNum()
    {
        for (var rowIndex = 0; rowIndex < _boardSize; rowIndex++)
        {
            int lastNumber;
    
            if (rowIndex % 2 == 0)  // odd rows (left-to-right)
            {
                lastNumber = (rowIndex + 1) * _boardSize;
                _evenEdgeNum.Add(lastNumber);
            }
            else  // even rows (right-to-left)
            {
                lastNumber = (rowIndex * _boardSize) + 1;
                _oddEdgeNum.Add(lastNumber);
            }

            //Console.WriteLine($"Row {rowIndex + 1} last number: {lastNumber}");
            
        }
        Console.WriteLine("------------------------------------------------");
        _oddEdgeNum.ForEach(n => LogDebug($"odd edge: {n}"));
        _evenEdgeNum.ForEach(n => LogDebug($"Even edge: {n}"));
        Console.WriteLine("------------------------------------------------");
    }

    private void LogDebug(string message)
    {
        if (_debugMode)
            Console.WriteLine(message);
    }

}