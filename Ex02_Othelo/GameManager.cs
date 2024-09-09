using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;

namespace Ex02_Othelo
{
    class GameManager
    {
<<<<<<< HEAD
        private char[,] gameArray;
        private Board board;
        private Player player1;
        private Player player2;
        private int gameArraySize;
        bool m_IsComputer;
        int boardGameSize;

        public GameManager(int i_BoardSize, string i_Player1Name, string i_Player2Name)
        {
            boardGameSize = i_BoardSize;
            board = new Board(i_BoardSize);
            player1 = new Player(i_Player1Name, 'O');
            player1.IsMyTurn = true;
            if (string.IsNullOrEmpty(i_Player2Name)) m_IsComputer = true;
            player2 = new Player(i_Player2Name, 'X', m_IsComputer);
        }
        public void SetGameArray(int i_BoardSize)
        {
            gameArraySize = i_BoardSize;
            gameArray = new char[i_BoardSize, i_BoardSize];

=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < i_BoardSize; colIndex++)
                {
                    gameArray[rowIndex, colIndex] = '*';
                }
            }

            int m_BoardMiddle = i_BoardSize / 2 - 1;
<<<<<<< HEAD
            gameArray[m_BoardMiddle, m_BoardMiddle] = player1.PlayerDisc;
            gameArray[m_BoardMiddle, m_BoardMiddle + 1] = player2.PlayerDisc;
            m_BoardMiddle++;
            gameArray[m_BoardMiddle, m_BoardMiddle - 1] = player2.PlayerDisc;
            gameArray[m_BoardMiddle, m_BoardMiddle] = player1.PlayerDisc;
=======
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = m_Player1.PlayerDisc;
            m_GameArray[m_BoardMiddle, m_BoardMiddle + 1] = m_Player2.PlayerDisc;
            m_BoardMiddle++;
            m_GameArray[m_BoardMiddle, m_BoardMiddle - 1] = m_Player2.PlayerDisc;
            m_GameArray[m_BoardMiddle, m_BoardMiddle] = m_Player1.PlayerDisc;
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
        }

        public void UpdateGameArray(int rowIndex, int colIndex, Player i_CurrentPlayer, Player i_OtherPlayer)
        {
<<<<<<< HEAD
            if(gameArray[rowIndex + 1, colIndex + 1] == i_OtherPlayer.PlayerDisc)
            {
                i_OtherPlayer.PlayerScore--;
            }

            i_CurrentPlayer.PlayerScore++;
            gameArray[rowIndex + 1, colIndex + 1] = i_CurrentPlayer.PlayerDisc;
        }

        public static bool CheckIfBoardSizeValid(string i_UserInput)
        {
            int boardSize;
            bool isValid = int.TryParse(i_UserInput, out boardSize) && (boardSize == 6 || boardSize == 8);
            
            return isValid;
=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
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
<<<<<<< HEAD
            Player currentPlayer = player1;
            Player otherPlayer;
            string userInput="";
            board.PrintBoard();
=======
            Player m_CurrentPlayer = m_Player1;
            Player m_OtherPlayer;
            string userInput="";
            m_Board.PrintBoard();
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
            bool gameOver = false;
            while (!gameOver)
            {
                if (checkGameOver())
                {
<<<<<<< HEAD
                    declareWinner();
=======
                    DeclareWinner();
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
                    gameOver = true;
                    break;
                }

<<<<<<< HEAD
                currentPlayer = player1.IsMyTurn ? player1 : player2;
                if (!playerHasValidMoves(currentPlayer))
                {
                    switchTurns(currentPlayer);
                    continue;
                }

                Console.WriteLine($"It's {currentPlayer.PlayerName}'s turn.");
                if (!currentPlayer.IsComputer)
                {
                    bool playerChoseToLeave=false;
                    (playerChoseToLeave,userInput)= OthelloGame.RequestForMoveOrExit(currentPlayer);
=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
                    if (playerChoseToLeave)
                    {
                        gameOver = true;    
                        continue;
                    }
                    else
                    {
<<<<<<< HEAD
                        (int rowIndex, int colIndex) = currentPlayer.GetMove(userInput);
                        if (rowIndex != -1 && colIndex != -1 && isValidMove(rowIndex, colIndex, currentPlayer))
                        {
                            PerformFlips(rowIndex, colIndex, currentPlayer);
                            board.PlaceDisc(rowIndex, colIndex, currentPlayer.PlayerDisc);

                            if(currentPlayer==player1)
                            {
                                otherPlayer = player2;
                            }
                            else
                            {
                                otherPlayer = player1;
                            }

                            UpdateGameArray(rowIndex - 1, colIndex - 1,currentPlayer,otherPlayer);
                            Screen.Clear();
                            switchTurns(currentPlayer);
                            board.PrintBoard();
=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
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
<<<<<<< HEAD
                    validMoves = validMovesGenerator(currentPlayer);
                    (int rowIndex, int colIndex) = currentPlayer.GetMove(userInput,validMoves);
                    if (rowIndex != -1 && colIndex != -1 && isValidMove(rowIndex, colIndex, currentPlayer))
                    {
                        PerformFlips(rowIndex, colIndex, currentPlayer);
                        board.PlaceDisc(rowIndex, colIndex, currentPlayer.PlayerDisc);

                        if (currentPlayer == player1)
                        {
                            otherPlayer = player2;
                        }
                        else
                        {
                            otherPlayer = player1;
                        }

                        UpdateGameArray(rowIndex - 1, colIndex - 1, currentPlayer,otherPlayer);
                        Screen.Clear();
                        switchTurns(currentPlayer);
                        board.PrintBoard();
                        Console.WriteLine($"Computer placed a disc at ({(char)('A' + colIndex)}{rowIndex + 1}).");
                    }

=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
                }

            }
            return validMoves;
        }

        private List<(int rowIndex, int colIndex)> validMovesGenerator(Player i_CurrentPlayer)
        {
            List<(int, int)> validMoves = new List<(int, int)>();
            for (int row = 0; row < gameArraySize; row++)
            {
                for (int col = 0; col < gameArraySize; col++)
                {
                    if (isValidMove(row, col, i_CurrentPlayer))
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

            for (int row = 0; row < board.GetBoardSize() && !hasValidMove; row++)
            {
                for (int col = 0; col < board.GetBoardSize() && !hasValidMove; col++)
                {
                    if (gameArray[row, col] == '*' && isValidMove(row, col, i_Player))
                    {
                        hasValidMove = true;
                    }
                }
            }

            return hasValidMove;
        }
        private void switchTurns(Player i_CurrentPlayer)
        {
            Player otherPlayer = i_CurrentPlayer == player1 ? player2 : player1;

            if (!playerHasValidMoves(i_CurrentPlayer))
            {
                Console.WriteLine($"{i_CurrentPlayer.PlayerName} has no valid moves. Switching to {otherPlayer.PlayerName}.");
                i_CurrentPlayer.IsMyTurn = false;
                otherPlayer.IsMyTurn = true;

                if (!playerHasValidMoves(otherPlayer))
                {
                    Console.WriteLine("No valid moves for either players. Game Over.");
<<<<<<< HEAD
                    board.PrintBoard();
                    declareWinner();
=======
                    m_Board.PrintBoard();
                    DeclareWinner();

>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
                }
            }
            else
            {
<<<<<<< HEAD
                player1.IsMyTurn = !player1.IsMyTurn;
                player2.IsMyTurn = !player2.IsMyTurn;
                i_CurrentPlayer = i_CurrentPlayer == player1 ? player2 : player1;
            }
        }

        public bool isValidMove(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            bool isValid = false;

            if (i_Row != -1 && i_Col != -1 && gameArray[i_Row, i_Col] == '*')
=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
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

            while (currentRow >= 0 && currentRow < board.GetBoardSize() &&
                   currentCol >= 0 && currentCol < board.GetBoardSize())
            {
                if (gameArray[currentRow, currentCol] == opponentDisc)
                {
                    foundOpponent = true;
                }
                else if (gameArray[currentRow, currentCol] == i_PlayerDisc && foundOpponent)
                {
                    isValid = true;
                    if (shouldFlip)
                    {
                        int flipRow = i_Row + i_RowScanDirection;
                        int flipCol = i_Col + i_ColScanDirection;

                        while (flipRow != currentRow || flipCol != currentCol)
                        {
<<<<<<< HEAD
                            board.FlipDiscs(flipRow, flipCol, i_PlayerDisc);
                            Player currentPlayer;
                            Player otherPlayer;
                            if (i_PlayerDisc == 'O')
                            {
                                otherPlayer = player2;
                                currentPlayer = player1;
                            }
                            else
                            {
                                otherPlayer = player1;
                                currentPlayer = player2;
                            }
                            UpdateGameArray(flipRow - 1, flipCol - 1, currentPlayer, otherPlayer);
=======
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

>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5

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

            for (int row = 0; row < board.GetBoardSize(); row++)
            {
                for (int col = 0; col < board.GetBoardSize(); col++)
                {
                    if (gameArray[row, col] == '*' && (isValidMove(row, col, player1) || isValidMove(row, col, player2)))
                    {
                        gameOver = false;  
                    }
                }
            }

            return gameOver; 
        }

        private void declareWinner()
        {
            Console.WriteLine("Game Over!");
<<<<<<< HEAD
            if (player1.PlayerScore > player2.PlayerScore)
            {
                Console.WriteLine($"{player1.PlayerName} wins with {player1.PlayerScore} points! {player2.PlayerName} has {player2.PlayerScore} points.");
            }
            else if (player2.PlayerScore > player1.PlayerScore)
            {
                Console.WriteLine($"{player2.PlayerName} wins with {player2.PlayerScore} points! {player1.PlayerName} has {player1.PlayerScore} points.");
            }
            else
            {
                Console.WriteLine($"The game is a tie! Both players have {player2.PlayerScore} points.");
            }

            bool userChoseRematch;
            userChoseRematch = OthelloGame.AskUserForRematchOrExit(player1,player2, boardGameSize);

            if (userChoseRematch)
            {
                GameManager gameManager = new GameManager(boardGameSize, player1.PlayerName, player2.PlayerName);
                player1.PlayerScore = 2;
                player2.PlayerScore = 2;
                gameManager.SetGameArray(boardGameSize);
                Board gameBoard = new Board(boardGameSize);
                gameManager.StartGame();
=======
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
>>>>>>> 1e3254c480e15a08097542fcccdc2c63b840eff5
            }
        }
    }
}
