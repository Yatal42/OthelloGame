using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;

namespace Ex02_Othelo
{
    class GameManager
    {
        private char[,] m_GameArray;
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private int m_GameArraySize;
        private bool m_IsComputer;
        private int m_BoardGameSize;
        private bool m_GameOver = false;

        public GameManager(int i_BoardSize, string i_Player1Name, string i_Player2Name)
        {
            m_BoardGameSize = i_BoardSize;
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, 'O');
            m_Player1.IsMyTurn = true;

            if (string.IsNullOrEmpty(i_Player2Name))
            {
                m_IsComputer = true;
            }

            m_Player2 = new Player(i_Player2Name, 'X', m_IsComputer);
        }

        public void SetGameArray(int i_BoardSize)
        {
            m_GameArraySize = i_BoardSize;
            m_GameArray = new char[i_BoardSize, i_BoardSize];

            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    m_GameArray[rowIndex, colIndex] = '*';
                }
            }

            int boardMiddle = i_BoardSize / 2 - 1;
            m_GameArray[boardMiddle, boardMiddle] = m_Player1.PlayerDisc;
            m_GameArray[boardMiddle, boardMiddle + 1] = m_Player2.PlayerDisc;
            boardMiddle++;
            m_GameArray[boardMiddle, boardMiddle - 1] = m_Player2.PlayerDisc;
            m_GameArray[boardMiddle, boardMiddle] = m_Player1.PlayerDisc;
        }

        public Board GetBoard
        {
            get { return m_Board; }
        }

        public void UpdateGameArray(int rowIndex, int colIndex, Player i_CurrentPlayer, Player i_OtherPlayer)
        {
            if (m_GameArray[rowIndex + 1, colIndex + 1] == i_OtherPlayer.PlayerDisc)
            {
                i_OtherPlayer.PlayerScore--;
            }

            i_CurrentPlayer.PlayerScore++;
            m_GameArray[rowIndex + 1, colIndex + 1] = i_CurrentPlayer.PlayerDisc;
        }

        public static bool CheckIfBoardSizeValid(string i_UserInput)
        {
            int boardSize;
            bool isValid = int.TryParse(i_UserInput, out boardSize) && (boardSize == 6 || boardSize == 8);

            return isValid;
        }

        public void StartGame(int boardSize)
        {
            SetGameArray(boardSize);
            m_Board = new Board(boardSize);
            Screen.Clear();
            Player currentPlayer = m_Player1;
            Player otherPlayer;
            string userInput = "";
            m_Board.PrintBoard();

            while (!m_GameOver)
            {
                if (checkGameOver())
                {
                    declareWinner();
                    m_GameOver = true;
                    OthelloGame.AskUserForRematchOrExit(m_Player1, m_Player2, m_BoardGameSize, ref m_GameOver);
                    break;
                }

                currentPlayer = m_Player1.IsMyTurn ? m_Player1 : m_Player2;
                if (!playerHasValidMoves(currentPlayer))
                {
                    switchTurns(currentPlayer, ref m_GameOver);
                    continue;
                }

                Console.WriteLine($"It's {currentPlayer.PlayerName}'s turn.");
                if (!m_GameOver)
                {
                    int rowIndex = -1;
                    int colIndex = -1;

                    if (!currentPlayer.IsComputer)
                    {

                        bool playerChoseToLeave = false;
                        (playerChoseToLeave, userInput) = OthelloGame.RequestForMoveOrExit(currentPlayer);
                        if (playerChoseToLeave && userInput == "Q")
                        {
                            m_GameOver = true;
                            break;
                        }
                        else
                        {
                            (rowIndex, colIndex) = currentPlayer.GetMove(userInput);

                        }
                    }
                    else
                    {
                        List<(int rowIndex, int colIndex)> validMoves;
                        validMoves = validMovesGenerator(currentPlayer);
                        (rowIndex, colIndex) = currentPlayer.GetMove(userInput, validMoves);
                    }

                    if (rowIndex != -1 && colIndex != -1 && IsValidMove(rowIndex, colIndex, currentPlayer))
                    {
                        PerformFlips(rowIndex, colIndex, currentPlayer);
                        m_Board.PlaceDisc(rowIndex, colIndex, currentPlayer.PlayerDisc);

                        if (currentPlayer == m_Player1)
                        {
                            otherPlayer = m_Player2;
                        }
                        else
                        {
                            otherPlayer = m_Player1;
                        }

                        UpdateGameArray(rowIndex - 1, colIndex - 1, currentPlayer, otherPlayer);
                        Screen.Clear();
                        m_Board.PrintBoard();

                        if (currentPlayer.IsComputer)
                        {
                            Console.WriteLine($"Computer placed a disc at ({(char)('A' + colIndex)}{rowIndex + 1}).");
                        }

                        switchTurns(currentPlayer, ref m_GameOver);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move! Please choose a valid move.");
                    }
                }
            }
        }

        private List<(int rowIndex, int colIndex)> validMovesGenerator(Player i_CurrentPlayer)
        {
            List<(int, int)> validMoves = new List<(int, int)>();

            for (int row = 0; row < m_GameArraySize; row++)
            {
                for (int col = 0; col < m_GameArraySize; col++)
                {
                    if (IsValidMove(row, col, i_CurrentPlayer))
                    {
                        validMoves.Add((row, col));
                    }
                }
            }

            return validMoves;
        }

        private bool playerHasValidMoves(Player i_Player)
        {
            bool hasValidMove = false;

            for (int row = 0; row < m_Board.GetBoardSize && !hasValidMove; row++)
            {
                for (int col = 0; col < m_Board.GetBoardSize && !hasValidMove; col++)
                {
                    if (m_GameArray[row, col] == '*' && IsValidMove(row, col, i_Player))
                    {
                        hasValidMove = true;
                    }
                }
            }

            return hasValidMove;
        }

        private void switchTurns(Player i_CurrentPlayer, ref bool gameOver)
        {
            Player otherPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;

            if (!playerHasValidMoves(i_CurrentPlayer))
            {
                Console.WriteLine($"{i_CurrentPlayer.PlayerName} has no valid moves. Switching to {otherPlayer.PlayerName}.");
                i_CurrentPlayer.IsMyTurn = false;
                otherPlayer.IsMyTurn = true;

                if (!playerHasValidMoves(otherPlayer))
                {
                    Console.WriteLine("No valid moves for either players. Game Over.");
                    declareWinner();
                    gameOver = true;
                    OthelloGame.AskUserForRematchOrExit(m_Player1, m_Player2, m_BoardGameSize, ref gameOver);
                }
            }
            else
            {
                m_Player1.IsMyTurn = !m_Player1.IsMyTurn;
                m_Player2.IsMyTurn = !m_Player2.IsMyTurn;
                i_CurrentPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
            }
        }

        public bool IsValidMove(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            bool isValid = false;

            if (i_Row != -1 && i_Col != -1 && m_GameArray[i_Row, i_Col] == '*')
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (!(i == 0 && j == 0) && checkAndFlipInDirection(i_CurrentPlayer.PlayerDisc, i_Row, i_Col, i, j, false))
                        {
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }

        private bool checkAndFlipInDirection(char i_PlayerDisc, int i_Row, int i_Col,
                                             int i_RowScanDirection, int i_ColScanDirection,
                                             bool shouldFlip)
        {
            int currentRow = i_Row + i_RowScanDirection;
            int currentCol = i_Col + i_ColScanDirection;
            char opponentDisc = i_PlayerDisc == 'O' ? 'X' : 'O';
            bool foundOpponent = false;
            bool isValid = false;

            while (currentRow >= 0 && currentRow < m_Board.GetBoardSize &&
                   currentCol >= 0 && currentCol < m_Board.GetBoardSize)
            {
                if (m_GameArray[currentRow, currentCol] == opponentDisc)
                {
                    foundOpponent = true;
                }
                else if (m_GameArray[currentRow, currentCol] == i_PlayerDisc && foundOpponent)
                {
                    isValid = true;
                    if (shouldFlip)
                    {
                        int flipRow = i_Row + i_RowScanDirection;
                        int flipCol = i_Col + i_ColScanDirection;

                        while (flipRow != currentRow || flipCol != currentCol)
                        {
                            m_Board.FlipDiscs(flipRow, flipCol, i_PlayerDisc);
                            Player currentPlayer;
                            Player otherPlayer;

                            if (i_PlayerDisc == 'O')
                            {
                                otherPlayer = m_Player2;
                                currentPlayer = m_Player1;
                            }
                            else
                            {
                                otherPlayer = m_Player1;
                                currentPlayer = m_Player2;
                            }

                            UpdateGameArray(flipRow - 1, flipCol - 1, currentPlayer, otherPlayer);
                            flipRow += i_RowScanDirection;
                            flipCol += i_ColScanDirection;
                        }
                    }

                    break;
                }
                else
                {
                    break;
                }

                currentRow += i_RowScanDirection;
                currentCol += i_ColScanDirection;
            }

            return isValid;
        }

        public void PerformFlips(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        checkAndFlipInDirection(i_CurrentPlayer.PlayerDisc, i_Row, i_Col, i, j, true);
                    }
                }
            }
        }

        private bool checkGameOver()
        {
            bool gameOver = true;

            for (int row = 0; row < m_Board.GetBoardSize; row++)
            {
                for (int col = 0; col < m_Board.GetBoardSize; col++)
                {
                    if (m_GameArray[row, col] == '*' && (IsValidMove(row, col, m_Player1) || IsValidMove(row, col, m_Player2)))
                    {
                        gameOver = false;
                    }
                }
            }

            return gameOver;
        }

        private void declareWinner()
        {
            if (m_Player1.PlayerScore > m_Player2.PlayerScore)
            {
                Console.WriteLine($"{m_Player1.PlayerName} wins with {m_Player1.PlayerScore} points! {m_Player2.PlayerName} has {m_Player2.PlayerScore} points.");
            }
            else if (m_Player2.PlayerScore > m_Player1.PlayerScore)
            {
                Console.WriteLine($"{m_Player2.PlayerName} wins with {m_Player2.PlayerScore} points! {m_Player1.PlayerName} has {m_Player1.PlayerScore} points.");
            }
            else
            {
                Console.WriteLine($"The game is a tie! Both players have {m_Player2.PlayerScore} points.");
            }
        }
    }
}
