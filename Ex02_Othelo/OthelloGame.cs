using Ex02.ConsoleUtils;
using Ex02_Othelo;
using System;
using System.Collections.Generic;

namespace Ex02_Othelo
{
    class OthelloGame
    {
        public static void InitializeGame()
        {
            Console.WriteLine("Welcome to Othello!" + "\n");
            Console.Write("Enter Player 1's name: ");
            string m_Player1Name = Console.ReadLine();
            while (!NameIsValid(m_Player1Name))
            {
                Console.Write("Invalid Name, please enter player 1's name: ");
                m_Player1Name = Console.ReadLine();
            }
            string isComputer = "2";
            Console.Write("Choose your opponent: type '1' to play against the computer or '2' to play against a human: ");
            isComputer = Console.ReadLine();
            string m_Player2Name;
            if (isComputer == "2")
            {
                Console.Write("Enter player 2's name: ");
                m_Player2Name = Console.ReadLine();
                while (!NameIsValid(m_Player2Name))
                {
                    Console.Write("Invalid Name, please enter player 2's name: ");
                    m_Player2Name = Console.ReadLine();
                }
            }
            else
            {
                m_Player2Name = ""; 
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
            int m_BoardSize;
            int.TryParse(userInput, out m_BoardSize);
            GameManager m_GameManager = new GameManager(m_BoardSize, m_Player1Name, m_Player2Name);
            m_GameManager.SetGameArray(m_BoardSize);
            Board m_GameBoard = new Board(m_BoardSize);
            m_GameManager.StartGame();
        }

        public static (bool, string) RequestForMoveOrExit(Player i_CurrentPlayer)
        {
            Console.Write($"Type 'Q' to quit the game or choose a cell (e.g E3) to place your disc: '{i_CurrentPlayer.PlayerDisc}': ");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper() == "Q")
            {
                Console.WriteLine("You chose to exit the game, Bye Bye...");
                return (true,userInput);
            }
            return (false,userInput);
        }
        
        public static bool AskUserForRematchOrExit(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Console.WriteLine("Click anything but 'Q' if you would like a rematch. press 'Q' to Exit");
            string userChose = Console.ReadLine();
            if (userChose.ToUpper() == "Q")
            {
                Console.WriteLine("Exiting the game...");
                return false;
            }
            else
            {
                Console.WriteLine("Restarting the game...");
                return true;
                
            }
        }

            
public static bool NameIsValid(string i_NameString)
        {
            if(string.IsNullOrEmpty(i_NameString)) return false;
            return true;
        }
        
        
    }
}
