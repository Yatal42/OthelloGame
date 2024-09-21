using System;

namespace Ex02_Othelo
{
    class OthelloGame
    {
        public static GameManager m_GameManager;

        public static void InitializeGame()
        {
            Console.WriteLine("Welcome to Othello!" + "\n");
            Console.Write("Enter Player 1's name: ");
            string player1Name = Console.ReadLine();

            while (!NameIsValid(player1Name))
            {
                Console.Write("Invalid Name, please enter player 1's name: ");
                player1Name = Console.ReadLine();
            }

            string isComputer = "2";
            Console.Write("Choose your opponent: type '1' to play against the computer or '2' to play against a human: ");
            isComputer = Console.ReadLine();
            string player2Name;

            if (isComputer == "2")
            {
                Console.Write("Enter player 2's name: ");
                player2Name = Console.ReadLine();
                while (!NameIsValid(player2Name))
                {
                    Console.Write("Invalid Name, please enter player 2's name: ");
                    player2Name = Console.ReadLine();
                }
            }
            else
            {
                player2Name = "";
            }

            Console.Write("Choose m_Board NxN size (6 or 8): ");
            string userInput = Console.ReadLine();
            bool isBoardSizeValid = GameManager.CheckIfBoardSizeValid(userInput);

            while (!isBoardSizeValid)
            {
                Console.Write("Invalid m_Board Size. Choose m_Board NxN size (6 or 8): ");
                userInput = Console.ReadLine();
                isBoardSizeValid = GameManager.CheckIfBoardSizeValid(userInput);
            }

            int boardSize;
            int.TryParse(userInput, out boardSize);
            m_GameManager = new GameManager(boardSize, player1Name, player2Name);
            m_GameManager.StartGame(boardSize);
        }

        public static (bool, string) RequestForMoveOrExit(Player i_CurrentPlayer)
        {
            Console.Write($"Type 'Q' to quit the game or choose a cell (e.g E3) to place your disc: '{i_CurrentPlayer.PlayerDisc}': ");
            string userInput = Console.ReadLine();
            bool isExit = userInput.ToUpper() == "Q";

            if (isExit)
            {
                Console.WriteLine("You chose to exit the game, Bye Bye...");
            }

            return (isExit, userInput.ToUpper());
        }

        public static void AskUserForRematchOrExit(Player i_Player1, Player i_Player2, int i_BoardSize, ref bool gameOver)
        {
            Console.WriteLine("Click anything but 'Q' if you would like a rematch. Press 'Q' to Exit");
            string userChose = Console.ReadLine();
            bool isExit = userChose.ToUpper() == "Q";

            if (isExit)
            {
                Console.WriteLine("Exiting the game...");
            }
            else
            {
                gameOver = false;
                Console.WriteLine("Restarting the game...");
                m_GameManager.GetBoard.BoardArray = i_BoardSize;
                m_GameManager.SetGameArray(i_BoardSize);
                i_Player1.PlayerScore = 2;
                i_Player2.PlayerScore = 2;
                i_Player1.IsMyTurn = true;
                i_Player2.IsMyTurn = false;
                m_GameManager.StartGame(i_BoardSize);
            }
        }

        public static bool NameIsValid(string i_NameString)
        {
            bool isValid = !string.IsNullOrEmpty(i_NameString);

            return isValid;
        }
    }
}
