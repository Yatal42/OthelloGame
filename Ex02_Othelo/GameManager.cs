using System;
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
        bool isComputer;
        public GameManager(int i_BoardSize, string i_Player1Name, string i_Player2Name)
        {
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, 'O');
            m_Player1.IsMyTurn = true;
            if (i_Player2Name == "Computer") isComputer = true;
            m_Player2 = new Player(i_Player2Name, 'X', isComputer);
        }
        public void SetGameArray(int i_BoardSize)
        {
            m_GameArraySize=i_BoardSize;
            m_GameArray = new char[i_BoardSize, i_BoardSize];
            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    m_GameArray[rowIndex, colIndex] = '*';
                }
            }
            int m_BoardMiddle = i_BoardSize / 2 - 1;
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = 'O';
            m_GameArray[m_BoardMiddle, m_BoardMiddle + 1] = 'X';
            m_BoardMiddle++;
            m_GameArray[m_BoardMiddle, m_BoardMiddle-1] = 'X';
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = 'O';
        }
        public void UpdateGameArray(int rowIndex, int colIndex, char m_PlayerDisc)
        {
            m_GameArray[rowIndex+1, colIndex+1] = m_PlayerDisc;
        }

        public void PrintBoard(int boardSize) //TODO: DELETE AT END
        {
            Console.WriteLine("This is the background array, don't forger to delete at end: ");
            for (int rowIndex = 0; rowIndex < m_GameArray.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_GameArray.GetLength(1); colIndex++)
                {
                    Console.Write(m_GameArray[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }
        }

        public void StartGame()
        {
            m_Board.PrintBoard();
            PrintBoard(m_GameArraySize); //TODO: DELETE AT END

            bool gameOver = false;

            while (!gameOver)
            {
                if (CheckGameOver())
                {
                    DeclareWinner(); 
                    Console.WriteLine("Game over!"); 
                    gameOver = true;
                    break;
                }

                Player m_CurrentPlayer = m_Player1.IsMyTurn ? m_Player1 : m_Player2;
                if (!PlayerHasValidMoves(m_CurrentPlayer))
                {
                    SwitchTurns(m_CurrentPlayer);
                    continue; 
                }

                Console.WriteLine($"It's {m_CurrentPlayer.PlayerName}'s turn.");

                //The option to type exit to end the game after each turn
                //TO DO: change to q 
                //Console.WriteLine("Type 'exit' to quit the game or press Enter to continue.");
                //string userInput = Console.ReadLine();

                //if (userInput.ToLower() == "exit")
                //{
                //    gameOver = true;
                //    Console.WriteLine("Exiting the game...");
                //    break;
                //}

                (int rowIndex, int colIndex) = m_CurrentPlayer.GetMove(m_GameArray);

                if (rowIndex!=-1&& colIndex!=-1&& IsValidMove(rowIndex, colIndex, m_CurrentPlayer))
                {
                    PerformFlips(rowIndex, colIndex, m_CurrentPlayer);
                    m_Board.PlaceDisc(rowIndex, colIndex, m_CurrentPlayer.PlayerDisc);
                    UpdateGameArray(rowIndex - 1, colIndex - 1, m_CurrentPlayer.PlayerDisc);
                    Screen.Clear();
                    SwitchTurns(m_CurrentPlayer);
                    m_Board.PrintBoard();
                    PrintBoard(m_GameArraySize); //TODO: DELETE AT END
                }
                else
                {
                    Console.WriteLine("Invalid move! Please choose a valid move.");
                }
            }
        }

        private bool PlayerHasValidMoves(Player i_Player)
        {
            bool hasValidMove = false;

            for (int row = 0; row < m_Board.GetBoardSize() && !hasValidMove; row++)
            {
                for (int col = 0; col < m_Board.GetBoardSize() && !hasValidMove; col++)
                {
                    if (m_GameArray[row, col] == '*' && IsValidMove(row, col, i_Player))
                    {
                        hasValidMove = true;
                    }
                }
            }

            return hasValidMove;
        }
        private void SwitchTurns(Player i_CurrentPlayer)
        {
            Player otherPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;

            if (!PlayerHasValidMoves(i_CurrentPlayer))
            {
                Console.WriteLine($"{i_CurrentPlayer.PlayerName} has no valid moves. Switching to {otherPlayer.PlayerName}.");
                i_CurrentPlayer.IsMyTurn = false;
                otherPlayer.IsMyTurn = true;

                if (!PlayerHasValidMoves(otherPlayer))
                {
                    Console.WriteLine("No valid moves for either player. Game Over.");
                    DeclareWinner();
                }
            }
            else
            {
                m_Player1.IsMyTurn = !m_Player1.IsMyTurn;
                m_Player2.IsMyTurn = !m_Player2.IsMyTurn;
                i_CurrentPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
                Console.WriteLine("Player switched");
            }
        }

        public bool IsValidMove(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            bool isValid = false;

            if (m_GameArray[i_Row, i_Col] != '*')
            {
                isValid = false;
            }
            else
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (!(i == 0 && j == 0)&& CheckAndFlipInDirection(i_CurrentPlayer.PlayerDisc, i_Row, i_Col, i, j, false))
                        {
                                isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }


        private bool CheckAndFlipInDirection(char i_PlayerDisc, int i_Row, int i_Col, int i_RowScanDirection, int i_ColScanDirection, bool shouldFlip)
        {
            int currentRow = i_Row + i_RowScanDirection;
            int currentCol = i_Col + i_ColScanDirection;
            char opponentDisc = i_PlayerDisc == 'O' ? 'X' : 'O';
            bool foundOpponent = false;

            while (currentRow >= 0 && currentRow < m_Board.GetBoardSize() &&
                   currentCol >= 0 && currentCol < m_Board.GetBoardSize())
            {
                if (m_GameArray[currentRow, currentCol] == opponentDisc)
                {
                    foundOpponent = true;
                }
                else if (m_GameArray[currentRow, currentCol] == i_PlayerDisc && foundOpponent)
                {
                    if (shouldFlip)
                    {
                        int flipRow = i_Row + i_RowScanDirection;
                        int flipCol = i_Col + i_ColScanDirection;

                        while (flipRow != currentRow || flipCol != currentCol)
                        {
                            m_Board.FlipDiscs(flipRow, flipCol, i_PlayerDisc); 
                            UpdateGameArray(flipRow-1, flipCol-1, i_PlayerDisc);   

                            flipRow += i_RowScanDirection;
                            flipCol += i_ColScanDirection;
                        }
                    }

                    return true; 
                }
                else
                {
                    break; 
                }

                currentRow += i_RowScanDirection;
                currentCol += i_ColScanDirection;
            }

            return false;
        }

        public void PerformFlips(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))  
                    {
                        CheckAndFlipInDirection(i_CurrentPlayer.PlayerDisc, i_Row, i_Col, i, j, true);
                    }
                }
            }
        }

        private bool CheckGameOver()
        {
            bool gameOver = true;

            for (int row = 0; row < m_Board.GetBoardSize(); row++)
            {
                for (int col = 0; col < m_Board.GetBoardSize(); col++)
                {
                    if (m_GameArray[row, col] == '*' && (IsValidMove(row, col, m_Player1) || IsValidMove(row, col, m_Player2)))
                    {
                        gameOver = false;  
                    }
                }
            }

            return gameOver; 
        }

        private void DeclareWinner()
        {
            int player1Score = 0;
            int player2Score = 0;

            for (int row = 0; row < m_Board.GetBoardSize(); row++)
            {
                for (int col = 0; col < m_Board.GetBoardSize(); col++)
                {
                    if (m_GameArray[row, col] == m_Player1.PlayerDisc)
                    {
                        player1Score++;
                    }
                    else if (m_GameArray[row, col] == m_Player2.PlayerDisc)
                    {
                        player2Score++;
                    }
                }
            }

            if (player1Score > player2Score)
            {
                Console.WriteLine($"{m_Player1.PlayerName} wins with {player1Score} points!");
            }
            else if (player2Score > player1Score)
            {
                Console.WriteLine($"{m_Player2.PlayerName} wins with {player2Score} points!");
            }
            else
            {
                Console.WriteLine("The game is a tie!");
            }
        }
    }
}
