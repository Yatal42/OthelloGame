using System;

namespace Ex02_Othelo
{
    class OthelloGame
    {
        public static void InitializeGame()
        {
            Console.WriteLine("Welcome to Othello!" + "\n");
            Console.Write("Enter Player 1's name: ");
            string m_Player1Name = Console.ReadLine();
            string m_Player2Name;
            string isComputer = "2";
            Console.Write("Choose your opponent: type '1' to play against the computer or '2' to play against a player: ");
            isComputer = Console.ReadLine();
            if (isComputer == "2")
            {
                Console.Write("Enter Player 2's name: ");
                m_Player2Name = Console.ReadLine();
            }
            else
            {
                m_Player2Name = "Computer"; 
            }
            Console.Write("Choose board NxN size (6 or 8): ");
            int m_BoardSize = 0;
            while (m_BoardSize != 6 && m_BoardSize != 8)
            {
                Console.Write("Choose board NxN size (6 or 8): ");
                if (int.TryParse(Console.ReadLine(), out m_BoardSize))
                {
                    if (m_BoardSize != 6 && m_BoardSize != 8)
                    {
                        Console.WriteLine("Invalid board size. Please choose either 6 or 8."); 
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number."); 
                }
            }
            GameManager m_GameManager = new GameManager(m_BoardSize, m_Player1Name, m_Player2Name);
            m_GameManager.SetGameArray(m_BoardSize);
            Board m_GameBoard = new Board(m_BoardSize);
            m_GameManager.StartGame();
            Console.ReadLine();
        }
    }
}
