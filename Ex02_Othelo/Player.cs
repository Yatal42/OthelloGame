using System;

namespace Ex02_Othelo
{
    class Player
    {
        private bool m_IsMyTurn = false;
        private string m_Name;
        private char m_Char;
        private int m_Score = 2;
        private bool m_IsComputer = false;

        public Player(string i_PlayeName, char i_PlayerDisc, bool i_IsComputer = false)
        {
            m_Name = i_PlayeName;
            m_Char = i_PlayerDisc;
            m_IsComputer = i_IsComputer;
        }

        public string PlayerName
        {
            get { return m_Name; }
        }
        public char PlayerDisc
        {
            get { return m_Char; }
        }
        public int PlayerScore
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
        public bool IsComputer
        {
            get { return m_IsComputer; }
        }
        public bool IsMyTurn
        {
            get { return m_IsMyTurn; }
            set { m_IsMyTurn = value; }
        }

        public (int rowIndex, int colIndex) GetMove(char[,] i_GameArray)
        {
            int m_RowRequest;
            int m_ColRequest;
            //TODO: add if (!this.IsComputer), I removed it because the GetComputerMove is not done
            (m_RowRequest, m_ColRequest) = GetHumanMove();
            return (m_RowRequest, m_ColRequest);
            //TODO: add logic for computer move - I took the method from the chat but didn't check it yet
            //else{  GetComputerMove(board)....}
        }
        private (int rowIndex, int colIndex) GetHumanMove()
        {
            Console.Write(this.PlayerName + ", Please choose a cell (e.g E3) to place your disc" + " '" + this.PlayerDisc + "' : ");
            string m_CellInput = Console.ReadLine();
            //TODO: VALIDATION (within range of array, char int input only)
            char columnLetter = char.ToUpper(m_CellInput[0]);
            Board.BoardColumn columnEnum;
            Enum.TryParse(columnLetter.ToString(), out columnEnum);
            int rowIndex = int.Parse(m_CellInput.Substring(1)) - 1;
            int colIndex = (int)columnEnum;
            return (rowIndex, colIndex);
        }

        /*private (int, int) GetComputerMove(Board.Board board)
        {
            List<(int, int)> validMoves = new List<(int, int)>();

            for (int row = 0; row < board.GetBoardSize; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    if (board.IsCellEmpty(row, col)) // נניח שזה בודק גם את חוקיות המהלך
                    {
                        validMoves.Add((row, col));
                    }
                }
            }

            if (validMoves.Count > 0)
            {
                var (row, col) = validMoves[_random.Next(validMoves.Count)];
                Console.WriteLine($"Computer placed a disc at ({row + 1}, {col + 1}).");
                return (row, col);
            }

            return (-1, -1); // אין מהלך חוקי
        }*/
    }
}



//using System;
//using System.Collections.Generic;

//namespace OthelloGame
//{
//    class Player
//    {
//        private bool m_IsMyTurn = false;
//        private string m_Name;
//        private char m_Char;
//        private int m_Score = 2;
//        private bool m_IsComputer = false;
//        private static Random _random = new Random();

//        public Player(string i_PlayeName, char i_PlayerDisc, GameManager i_GameManager, bool i_IsComputer = false)
//        {
//            m_Name = i_PlayeName;
//            m_Char = i_PlayerDisc;
//            m_IsComputer = i_IsComputer;
//        }

//        public string PlayerName
//        {
//            get { return m_Name; }
//        }
//        public char PlayerDisc
//        {
//            get { return m_Char; }
//        }
//        public int PlayerScore
//        {
//            get { return m_Score; }
//            set { m_Score = value; }
//        }
//        public bool IsComputer
//        {
//            get { return m_IsComputer; }
//        }
//        public bool IsMyTurn
//        {
//            get { return m_IsMyTurn; }
//            set { m_IsMyTurn = value; }
//        }

//        // GetMove decides between human and computer moves
//        public (int rowIndex, int colIndex) GetMove(Board board, GameManager gameManager)
//        {
//            if (!this.IsComputer)
//            {
//                return GetHumanMove();
//            }
//            else
//            {
//                return GetComputerMove(board, gameManager);
//            }
//        }

//        // GetHumanMove gets input from the human player
//        private (int rowIndex, int colIndex) GetHumanMove()
//        {
//            Console.Write(this.PlayerName + ", Please choose a cell (e.g E3) to place your disc" + " '" + this.PlayerDisc + "' : ");
//            string m_CellInput = Console.ReadLine(); // TODO: VALIDATION (within range of array, char int input only)
//            char columnLetter = char.ToUpper(m_CellInput[0]);
//            Board.BoardColumn columnEnum;
//            Enum.TryParse(columnLetter.ToString(), out columnEnum);
//            int rowIndex = int.Parse(m_CellInput.Substring(1)) - 1;
//            int colIndex = (int)columnEnum;
//            return (rowIndex, colIndex);
//        }

//        // GetComputerMove decides the move for the computer player
//        private (int rowIndex, int colIndex) GetComputerMove(Board board, GameManager gameManager)
//        {
//            List<(int row, int col)> validMoves = new List<(int row, int col)>();

//            Console.WriteLine($"{PlayerName} (computer) is making a move...");

//            for (int row = 0; row < board.GetBoardSize(); row++)
//            {
//                for (int col = 0; col < board.GetBoardSize(); col++)
//                {
//                    if (gameManager.IsValidMove(row, col, this))
//                    {
//                        validMoves.Add((row, col));
//                    }
//                }
//            }

//            if (validMoves.Count > 0)
//            {
//                var (row, col) = validMoves[_random.Next(validMoves.Count)];
//                Console.WriteLine($"Computer placed a disc at ({row + 1}, {(char)('A' + col)}).");

//                gameManager.UpdateGameArray(row, col, this.PlayerDisc); 
//                board.PlaceDisc(row, col, this.PlayerDisc); 

//                gameManager.PerformFlips(row, col, this);

//                Console.WriteLine("Press Enter to continue to your turn...");
//                Console.ReadLine();

//                return (row, col);
//            }
//            return (-1, -1);
//        }
//    }
//}

