using System;
using System.Collections.Generic;
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
            if (m_IsComputer) m_Name = "Computer";

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

        public (int rowIndex, int colIndex) GetMove(string i_UserCellChoice = "", List<(int, int)> i_ValidMoves = null)
        {
            if (this.IsComputer)
            {
                return GetComputerMove(i_ValidMoves);
            }
            else
            {
                if(i_UserCellChoice.Length!=2)
                {

                    return (-1, -1);
                }
                if (Char.IsLetter(i_UserCellChoice, 0))
                {            
                    if (Char.IsDigit(i_UserCellChoice, 1))
                    {
                        return GetHumanMove(i_UserCellChoice);
                    }
                }
            }
            return (-1, -1);
        }
        private (int rowIndex, int colIndex) GetHumanMove(string i_UserCellChoice)
        {
            char columnLetter = char.ToUpper(i_UserCellChoice[0]);
            Board.BoardColumn columnEnum;
            Enum.TryParse(columnLetter.ToString(), out columnEnum);
            int rowIndex = int.Parse(i_UserCellChoice.Substring(1)) - 1;
            int colIndex = (int)columnEnum;
            return (rowIndex, colIndex);
        }
        private (int rowIndex, int colIndex) GetComputerMove(List<(int, int)> i_ValidMoves)
        {
            if (i_ValidMoves.Count > 0)
            {
                Random random = new Random();
                var (row, col) = i_ValidMoves[random.Next(i_ValidMoves.Count)];
                return (row, col);
            }
            return (-1, -1);
        }

    }
}