using static System.Console;

const int boardSize = 5;

var rawInput = File.ReadLines("input.txt")
    .Where(s => !string.IsNullOrWhiteSpace(s))
    .Select(s => s.Replace("  ", " ").Trim())
    .ToList();

var numbers = rawInput.First()
    .Split(",")
    .Select(int.Parse)
    .ToList();
rawInput.RemoveAt(0);

var numBoards = rawInput.Count / boardSize;
var boards = Enumerable.Range(0, numBoards)
    .Select(i => rawInput.GetRange(boardSize * i, boardSize))
    .Select(list => list.Select(
        s => s.Split(" ")
            .Select(int.Parse)
            .ToArray()
    ).ToArray())
    .ToList();

var boardMarks = Enumerable.Range(0, numBoards)
    .Select(_ => new Dictionary<(int, int), bool>())
    .ToList();

var (winningOrders, winningNumbers) = GetWinningBoard();

var taskOne = winningOrders.Select((value, idx) => (idx, value))
    .OrderBy(tuple => tuple.value)
    .Select(tuple => GetScore(tuple.idx, winningNumbers[tuple.idx]))
    .First();

var taskTwo = winningOrders.Select((value, idx) => (idx, value))
    .OrderByDescending(tuple => tuple.value)
    .Select(tuple => GetScore(tuple.idx, winningNumbers[tuple.idx]))
    .First();

WriteLine(taskOne);
WriteLine(taskTwo);

(int[], int[]) GetWinningBoard()
{
    var winIdx = 1;
    var winOrder = new int[numBoards];
    var winNumber = new int[numBoards];
    foreach (var number in numbers)
    {
        for (var boardIdx = 0; boardIdx < numBoards; boardIdx++)
        {
            if (winOrder[boardIdx] != 0)
                continue;

            var board = boards[boardIdx];
            for (var k = 0; k < boardSize; k++)
            {
                for (var j = 0; j < boardSize; j++)
                {
                    if (board[k][j] != number)
                        continue;

                    boardMarks[boardIdx].Add((k, j), true);
                    if (IsWinningMove((k, j), boardIdx))
                    {
                        winOrder[boardIdx] = winIdx;
                        winNumber[boardIdx] = number;

                        winIdx++;
                    }
                }
            }
        }
    }

    return (winOrder, winNumber);
}

bool IsWinningMove((int k, int j) tuple, int boardIdx)
{
    var boardMark = boardMarks[boardIdx];
    return Enumerable.Range(0, boardSize)
               .All(i => boardMark.ContainsKey((i, tuple.j))) ||
           Enumerable.Range(0, boardSize)
               .All(i => boardMark.ContainsKey((tuple.k, i)));
}

int GetScore(int boardIdx, int number)
{
    var brd = boards[boardIdx];
    var sum = Enumerable.Range(0, boardSize)
        .SelectMany(i => Enumerable.Range(0, boardSize).Select(j => (i, j)))
        .Where(tuple => !boardMarks[boardIdx].ContainsKey(tuple))
        .Sum(tuple => brd[tuple.i][tuple.j]);

    return sum * number;
}
