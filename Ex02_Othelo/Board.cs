using System;

namespace Ex02_Othelo
{
    class Board
    {
        int m_BoardSize;
        public enum BoardColumn
        {
            A, B, C, D, E, F, G, H
        }
        private char[,] m_BoardArray;

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            //m_BoardArray = new char[m_BoardSize, m_BoardSize];
            InitializeBoard(m_BoardSize);
        }
        
        public void InitializeBoard(int i_BoardSize)
        {
            int m_Rows = i_BoardSize * 2 + 2; 
            int m_Cols = i_BoardSize * 4 + 2; 

            m_BoardArray = new char[m_Rows, m_Cols];
            for (int rowIndex = 0; rowIndex < m_Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_Cols; colIndex++)
                {
                    m_BoardArray[rowIndex, colIndex] = ' ';
                }
            }
            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                m_BoardArray[0, 3 + rowIndex * 4] = (char)('A' + rowIndex);
            }
            for (int rowIndex = 1; rowIndex < m_Rows; rowIndex += 2)
            {
                for (int colIndex = 1; colIndex < m_Cols; colIndex++)
                {
                    m_BoardArray[rowIndex, colIndex] = '=';
                }
                if (rowIndex + 1 < m_Rows)
                {
                    m_BoardArray[rowIndex + 1, 0] = (char)('1' + (rowIndex / 2)); 
                }
            }
            for (int rowIndex = 2; rowIndex < m_Rows; rowIndex += 2)
            {
                for (int colIndex = 1; colIndex < m_Cols; colIndex += 4)
                {
                    m_BoardArray[rowIndex, colIndex] = '|';         
                }
            }
            SetBoardToStartingPoint(i_BoardSize);
        }
        public char[,] GetBoard()
        {
            return m_BoardArray;
        }

        public int GetBoardSize()
        {
            return m_BoardSize;
        }

        public void SetBoardToStartingPoint(int i_BoardSize)
        {
            int m_MidBoard = i_BoardSize;
            m_BoardArray[m_MidBoard, m_MidBoard * 2 - 1] = 'O';
            m_BoardArray[m_MidBoard, m_MidBoard * 2 + 3] = 'X';
            m_MidBoard += 2;
            m_BoardArray[m_MidBoard, m_MidBoard * 2 - 5] = 'X';
            m_BoardArray[m_MidBoard, m_MidBoard * 2 - 1] = 'O';
        }

        public void PrintBoard()
        {
            for (int rowIndex = 0; rowIndex < m_BoardArray.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < m_BoardArray.GetLength(1); colIndex++)
                {
                    Console.Write(m_BoardArray[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }
        }
        public void FlipDiscs(int i_RowIndex, int i_ColIndex, char i_PlayerDisc)
        {
            int m_RowIndex = i_RowIndex * 2 + 2; 
            int m_ColIndex = i_ColIndex * 4 + 3; 

            if (m_RowIndex >= 0 && m_RowIndex < m_BoardArray.GetLength(0) && m_ColIndex >= 0 && m_ColIndex < m_BoardArray.GetLength(1))
            {
                m_BoardArray[m_RowIndex, m_ColIndex] = i_PlayerDisc;  
            }
        }

        public void PlaceDisc(int i_RowIndex, int i_ColIndex, char i_PlayerDisc)
        {
            int m_RowIndex = i_RowIndex * 2 + 2;  
            int m_ColIndex = i_ColIndex * 4 + 3;  

            if (m_RowIndex >= 0 && m_RowIndex < m_BoardArray.GetLength(0) && m_ColIndex >= 0 && m_ColIndex < m_BoardArray.GetLength(1))
            {
                m_BoardArray[m_RowIndex, m_ColIndex] = i_PlayerDisc;  
            }
            else
            {
                Console.WriteLine("Error: Index out of bounds.");
            }
        }
    }
}
