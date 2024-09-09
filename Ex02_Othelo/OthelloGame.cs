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
            int m_BoardSize;
            bool m_ParseSuccess = int.TryParse(Console.ReadLine(), out m_BoardSize);           
            while ((m_BoardSize != 6 && m_BoardSize != 8) || !m_ParseSuccess)
            {
                Console.Write("Invalid board Size. Choose board NxN size (6 or 8): ");
                m_ParseSuccess = int.TryParse(Console.ReadLine(), out m_BoardSize);
            }
            GameManager m_GameManager = new GameManager(m_BoardSize, m_Player1Name, m_Player2Name);
            m_GameManager.SetGameArray(m_BoardSize);
            Board m_GameBoard = new Board(m_BoardSize);
            m_GameManager.StartGame();
            //Console.ReadLine();
        }

        public static bool NameIsValid(string i_NameString)
        {
            if(string.IsNullOrEmpty(i_NameString)) return false;
            return true;
        }
        
        
    }
}
