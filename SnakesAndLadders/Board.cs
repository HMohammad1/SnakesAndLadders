using static System.Text.Json.JsonSerializer;

namespace SnakesAndLadders;



public class Board
{
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
    
    private int BoardNum { get; set; }

    private void SetBoardNum()
    {
         if (_oddEdgeNum.Contains(BoardNum))
         {
             Console.WriteLine($"oddEdgeNum: {BoardNum}");
             // 4 - 5
             // 6 - 7
             BoardNum -= _boardSize + 1;
             _oddEdge = true;
         }
         else  if (_evenEdgeNum.Contains(BoardNum))
         {
             // 4 - 3
             // 6 - 5
             BoardNum -= _boardSize - 1;
             _oddEdge = false;
             Console.WriteLine($"evenEdgeNum: {BoardNum}");
         }

         if (_oddEdge)
         {
             BoardNum++;
         }
         else
         {
             BoardNum--;
         }
    }
    
    // TODO: 3 digit numbers (increase _boardRowWidth to 8)
    private void CreateBoard()
    {
        // 4 - 113
        // 5 - 176
        for (var i = 1; i < _boardSpaces; i++)
        {
            if (i % 7 == 0)
            {
                _board.Add(i, "|");
            } 
            else if ((i - 5) % 7 == 0)
            {
                _board.Add(i, BoardNum.ToString()[0].ToString());
            }
            else if ((i - 6) % 7 == 0)
            {
                _board.Add(i, BoardNum.ToString().Length > 1 ? BoardNum.ToString()[1].ToString() : " ");
                //BoardNum--;
                SetBoardNum();
            }
            else
            {
                _board.Add(i, " ");
            }
        }

        //DisplayBoard();


        // Console.WriteLine();
        // var json = Serialize(_board);
        // Console.WriteLine(json);
    }

    private void DisplayBoard()
    {
        foreach (var i in _board)
        {
            if ((i.Key - 1) % _boardRowLength == 0)
            {
                Console.WriteLine();
                for (var j = 0; j < _boardRowLength; j++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
            
            //Console.WriteLine($"k:{i.Key} v: {i.Value}");
            Console.Write(i.Value);
        }
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
        _oddEdgeNum.ForEach(n => Console.WriteLine($"odd edge: {n}"));
        _evenEdgeNum.ForEach(n => Console.WriteLine($"Even edge: {n}"));
        Console.WriteLine("------------------------------------------------");
    }



}