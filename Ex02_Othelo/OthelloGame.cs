using System;

namespace Ex02_Othelo
{
    class OthelloGame
    {
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

            Console.Write("Choose board NxN size (6 or 8): ");
            string userInput = Console.ReadLine();
            bool isBoardSizeValid = GameManager.CheckIfBoardSizeValid(userInput);

            while (!isBoardSizeValid)
            {
                Console.Write("Invalid board Size. Choose board NxN size (6 or 8): ");
                userInput= Console.ReadLine();
                isBoardSizeValid = GameManager.CheckIfBoardSizeValid(userInput);
            }

            int boardSize;
            int.TryParse(userInput, out boardSize);
            GameManager gameManager = new GameManager(boardSize, player1Name, player2Name);
            gameManager.SetGameArray(boardSize);
            Board m_GameBoard = new Board(boardSize);
            gameManager.StartGame();
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

            return (isExit, userInput);
        }

        public static bool AskUserForRematchOrExit(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Console.WriteLine("Click anything but 'Q' if you would like a rematch. Press 'Q' to Exit");
            string userChose = Console.ReadLine();
            bool isExit = userChose.ToUpper() == "Q";
            Console.WriteLine(isExit ? "Exiting the game..." : "Restarting the game...");

            return !isExit;
        }

        public static bool NameIsValid(string i_NameString)
        {
            bool isValid = !string.IsNullOrEmpty(i_NameString);

            return isValid;
        }
    }
}
