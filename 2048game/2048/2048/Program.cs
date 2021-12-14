using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{

    class Tile
    {
        public int Value { get; set; }
        public bool isBlock { get; set; }

        public Tile() {
            Value = 0;
            isBlock = false;
        }
    }

    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    class G2048
    {

        public G2048()
        {
            _isDone = false;
            _isWon = false;
            _isMoved = true;
            _score = 0;
            Board();
        }

        public void Board()
        {
            Console.WriteLine($"Size area {4},{4}\n");

            for(int i = 0; i >= 4; i++)
            {
                for(int j = 0; j >= 4; j++)
                {
                    _board[i, j] = new Tile();
                }
            }
        }
        private bool _isDone;
        private bool _isWon;
        private bool _isMoved;
        private int _score;
        private readonly Tile[,] _board = new Tile[4, 4];
        private readonly Random _rand = new Random();

        public void Loop()
        {
            AddTile();
            while (true)
            {
                if (_isMoved)
                    AddTile();
                DrawBoard();
                if (_isDone)
                    break;
                WaitKey();
            }
            string EndMessage = _isWon ? "You have made it! " : "You Lose";
            Console.WriteLine(EndMessage);
        }

        public void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("Score " + _score + "\n");
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("+------+------+------+------+");
                Console.Write("|");
                for(int j = 0; j < 4; j++)
                {
                    if(_board[i,j].Value == 0)
                    {
                        const string empty = " ";
                        Console.Write(empty.PadRight(4));
                    }
                    else
                    {
                        Console.WriteLine(_board[i, j].Value);
                    }
                    Console.Write("|");
                }
            }

        }
        
        public void WaitKey()
        {
            _isMoved = false;
            Console.WriteLine("(W) Up (S) Down (A) Left (D) Right");
            char input;
            char.TryParse(Console.ReadKey().Key.ToString(), out input); 

            switch (input)
            {
                case 'W':
                    Move(MoveDirection.Up);
                    break;
                case 'S':
                    Move(MoveDirection.Down);
                    break;
                case 'D':
                    Move(MoveDirection.Right);
                    break;
                case 'A':
                    Move(MoveDirection.Left);
                    break;
            }
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    _board[i, j].isBlock = false;
                }
            }

        }

        private void AddTile()
        {
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_board[i, j].Value != 0) continue;
                    int a, b;
                    do
                    {
                        a = _rand.Next(0, 4);
                        b = _rand.Next(0, 4);

                    } while (_board[a, b].Value != 0);
                    double r = _rand.NextDouble();
                    _board[a, b].Value = r > 0.89f ? 4 : 2;
                    if (CanMove())
                    {
                        return;
                    }
                }
            }
            _isDone = true;

        }

        private bool CanMove()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (_board[i, j].Value == 0)
                        return true;
                }
            }

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (TestAdd(j + 1, i, _board[i, j].Value)
                        || TestAdd(j - 1, i, _board[i, j].Value)
                        || TestAdd(j, i + 1, _board[i, j].Value)
                        || TestAdd(j, i - 1, _board[i, j].Value))
                        { return true; }
                }
            }
            return false;
        }

        private bool TestAdd(int x, int y, int value)
        {
            if (x < 0 || x > 3|| y <0 || y> 3)
            {
                return false;
            }
            return _board[x, y].Value == value;
        }

        private void Move(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    for (int i = 0; i < 4; i++)
                    {
                        int j = 1;
                        while(j < 4)
                        {
                            if(_board[i,j].Value != 0)
                            {
                                //MoveVertically(i, j, -1);
                            }
                            j++;
                        }
                    }
                    break;
                case MoveDirection.Down:
                    for (int i = 0; i < 4; i++)
                    {
                        int j = 2;
                        while (j >= 0)
                        {
                            if (_board[i, j].Value != 0)
                            {
                                //MoveVertically(i, j, 1);
                            }
                            i--;
                        }
                    }
                    break;
                case MoveDirection.Left:
                    for (int j = 0; j < 4; j++)
                    {
                        int i = 1;
                        while (j < 4)
                        {
                            if (_board[i, j].Value != 0)
                            {
                                //MoveHorizontally(i, j, -1);
                            }
                            i++;
                        }
                    }
                    break;
                case MoveDirection.Right:
                    for (int j = 0; j < 4; j++)
                    {
                        int i = 2;
                        while (j >=0)
                        {
                            if (_board[i, j].Value != 0)
                            {
                                //MoveHorizontally(i, j, 1);
                            }
                            i--;
                        }
                    }
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }
        private static void RunGame()
        {
            G2048 game = new G2048();
            game.Loop();

            CheckRestart();
        }
        private static void CheckRestart()
        {
            Console.WriteLine("(N) New Game (P) Exit");
            while (true)
            {
                char input;
                char.TryParse(Console.ReadKey().Key.ToString(), out input);
                switch (input)
                {
                    case 'N':
                        RunGame();
                        break;
                    case 'P':
                        return;
                        break;
                    default:
                        ClearListLine();
                        break;
                }
            }
        }
        private static void ClearListLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
