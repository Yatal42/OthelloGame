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
        bool isComputer;
        int m_BoardGameSize;
        public GameManager(int i_BoardSize, string i_Player1Name, string i_Player2Name)
        {
            m_BoardGameSize = i_BoardSize;
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, 'O');
            m_Player1.IsMyTurn = true;
            if (string.IsNullOrEmpty(i_Player2Name)) isComputer = true;
            m_Player2 = new Player(i_Player2Name, 'X', isComputer);
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
            int m_BoardMiddle = i_BoardSize / 2 - 1;
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = m_Player1.PlayerDisc;
            m_GameArray[m_BoardMiddle, m_BoardMiddle + 1] = m_Player2.PlayerDisc;
            m_BoardMiddle++;
            m_GameArray[m_BoardMiddle, m_BoardMiddle - 1] = m_Player2.PlayerDisc;
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = m_Player1.PlayerDisc;
        }

        public void UpdateGameArray(int rowIndex, int colIndex, Player i_CurrentPlayer, Player i_OtherPlayer)
        {
            if(m_GameArray[rowIndex + 1, colIndex + 1] == i_OtherPlayer.PlayerDisc)
            {
                i_OtherPlayer.PlayerScore--;
                i_CurrentPlayer.PlayerScore++;
            }
            else
            {
                i_CurrentPlayer.PlayerScore++;
            }
            m_GameArray[rowIndex + 1, colIndex + 1] = i_CurrentPlayer.PlayerDisc;
        }

        public static bool CheckIfBoardSizeValid(string i_UserInput)
        {
            int m_BoardSize;
            bool m_ParseSuccess = int.TryParse(i_UserInput, out m_BoardSize);
            if ((m_BoardSize != 6 && m_BoardSize != 8) || !m_ParseSuccess)
            {
                return false;
            }
            return true;
        }


        public void StartGame()
        {
            Screen.Clear();
            Player m_CurrentPlayer = m_Player1;
            Player m_OtherPlayer;
            string userInput="";
            m_Board.PrintBoard();
            bool gameOver = false;
            while (!gameOver)
            {
                if (CheckGameOver())
                {
                    DeclareWinner();
                    gameOver = true;
                    break;
                }

                m_CurrentPlayer = m_Player1.IsMyTurn ? m_Player1 : m_Player2;
                if (!PlayerHasValidMoves(m_CurrentPlayer))
                {
                    SwitchTurns(m_CurrentPlayer);
                    continue;
                }

                Console.WriteLine($"It's {m_CurrentPlayer.PlayerName}'s turn.");
                if (!m_CurrentPlayer.IsComputer)
                {
                    bool playerChoseToLeave=false;
                    (playerChoseToLeave,userInput)= OthelloGame.RequestForMoveOrExit(m_CurrentPlayer);
                    if (playerChoseToLeave)
                    {
                        gameOver = true;    
                        continue;
                    }
                    else
                    {
                        (int rowIndex, int colIndex) = m_CurrentPlayer.GetMove(userInput);
                        if (rowIndex != -1 && colIndex != -1 && IsValidMove(rowIndex, colIndex, m_CurrentPlayer))
                        {
                            PerformFlips(rowIndex, colIndex, m_CurrentPlayer);
                            m_Board.PlaceDisc(rowIndex, colIndex, m_CurrentPlayer.PlayerDisc);
                            if(m_CurrentPlayer==m_Player1)
                            {
                                m_OtherPlayer = m_Player2;
                            }
                            else
                            {
                                m_OtherPlayer = m_Player1;
                            }
                            UpdateGameArray(rowIndex - 1, colIndex - 1,m_CurrentPlayer,m_OtherPlayer);
                            Screen.Clear();
                            SwitchTurns(m_CurrentPlayer);
                            m_Board.PrintBoard();
                        }
                        else
                        {
                            Console.WriteLine("Invalid move! Please choose a valid move.");
                        }
                    }
                }
                else
                {
                    List<(int rowIndex, int colIndex)> validMoves;
                    validMoves = validMovesGenerator(m_CurrentPlayer);

                    (int rowIndex, int colIndex) = m_CurrentPlayer.GetMove(userInput,validMoves);
                    if (rowIndex != -1 && colIndex != -1 && IsValidMove(rowIndex, colIndex, m_CurrentPlayer))
                    {
                        PerformFlips(rowIndex, colIndex, m_CurrentPlayer);
                        m_Board.PlaceDisc(rowIndex, colIndex, m_CurrentPlayer.PlayerDisc);
                        if (m_CurrentPlayer == m_Player1)
                        {
                            m_OtherPlayer = m_Player2;
                        }
                        else
                        {
                            m_OtherPlayer = m_Player1;
                        }
                        UpdateGameArray(rowIndex - 1, colIndex - 1, m_CurrentPlayer,m_OtherPlayer);
                        Screen.Clear();
                        SwitchTurns(m_CurrentPlayer);
                        m_Board.PrintBoard();
                        Console.WriteLine($"Computer placed a disc at ({(char)('A' + colIndex)}{rowIndex + 1}).");
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
                    Console.WriteLine("No valid moves for either players. Game Over.");
                    m_Board.PrintBoard();
                    DeclareWinner();

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
            if (i_Row==-1 || i_Col==-1)
            {
                return isValid;
            }
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
                            Player m_CurrentPlayer;
                            Player m_OtherPlayer;
                            if (i_PlayerDisc == 'O')
                            {
                                m_OtherPlayer = m_Player2;
                                m_CurrentPlayer= m_Player1;
                            }
                            else
                            {
                                m_OtherPlayer = m_Player1;
                                m_CurrentPlayer = m_Player2;
                            }
                            UpdateGameArray(flipRow-1, flipCol-1,m_CurrentPlayer, m_OtherPlayer );


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
            Console.WriteLine("Game Over!");
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

            bool userChoseRematch;
            userChoseRematch = OthelloGame.AskUserForRematchOrExit(m_Player1,m_Player2, m_BoardGameSize);
            if (userChoseRematch)
            {
                GameManager m_GameManager = new GameManager(m_BoardGameSize, m_Player1.PlayerName, m_Player2.PlayerName);
                m_Player1.PlayerScore = 2;
                m_Player2.PlayerScore = 2;
                m_GameManager.SetGameArray(m_BoardGameSize);
                Board m_GameBoard = new Board(m_BoardGameSize);
                m_GameManager.StartGame();
            }
        }
    }
}
