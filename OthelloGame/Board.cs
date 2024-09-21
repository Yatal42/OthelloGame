using System;

namespace Ex02_Othelo
{
    class Board
    {
        int m_BoardSize;
        private char[,] m_BoardArray;
        public enum eBoardColumn
        {
            A, B, C, D, E, F, G, H
        }

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            InitializeBoard(m_BoardSize);
        }

        public int BoardArray
        {
            set { InitializeBoard(value, false); }
        }

        public void InitializeBoard(int i_BoardSize, bool i_IsFirstBoard = true)
        {
            int rows = i_BoardSize * 2 + 2;
            int cols = i_BoardSize * 4 + 2;

            if (i_IsFirstBoard)
            {
                m_BoardArray = new char[rows, cols];
            }

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    m_BoardArray[rowIndex, colIndex] = ' ';
                }
            }

            for (int rowIndex = 0; rowIndex < i_BoardSize; rowIndex++)
            {
                m_BoardArray[0, 3 + rowIndex * 4] = (char)('A' + rowIndex);
            }

            for (int rowIndex = 1; rowIndex < rows; rowIndex += 2)
            {
                for (int colIndex = 1; colIndex < cols; colIndex++)
                {
                    m_BoardArray[rowIndex, colIndex] = '=';
                }

                if (rowIndex + 1 < rows)
                {
                    m_BoardArray[rowIndex + 1, 0] = (char)('1' + (rowIndex / 2));
                }
            }

            for (int rowIndex = 2; rowIndex < rows; rowIndex += 2)
            {
                for (int colIndex = 1; colIndex < cols; colIndex += 4)
                {
                    m_BoardArray[rowIndex, colIndex] = '|';
                }
            }

            SetBoardToStartingPoint(i_BoardSize);
        }

        public int GetBoardSize
        {
            get { return m_BoardSize; }
        }

        public void SetBoardToStartingPoint(int i_BoardSize)
        {
            int midBoard = i_BoardSize;
            m_BoardArray[midBoard, midBoard * 2 - 1] = 'O';
            m_BoardArray[midBoard, midBoard * 2 + 3] = 'X';
            midBoard += 2;
            m_BoardArray[midBoard, midBoard * 2 - 5] = 'X';
            m_BoardArray[midBoard, midBoard * 2 - 1] = 'O';
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
            int rowIndex = i_RowIndex * 2 + 2;
            int colIndex = i_ColIndex * 4 + 3;

            if (rowIndex >= 0 && rowIndex < m_BoardArray.GetLength(0) && colIndex >= 0 && colIndex < m_BoardArray.GetLength(1))
            {
                m_BoardArray[rowIndex, colIndex] = i_PlayerDisc;
            }
        }

        public void PlaceDisc(int i_RowIndex, int i_ColIndex, char i_PlayerDisc)
        {
            int rowIndex = i_RowIndex * 2 + 2;
            int colIndex = i_ColIndex * 4 + 3;

            if (rowIndex >= 0 && rowIndex < m_BoardArray.GetLength(0) && colIndex >= 0 && colIndex < m_BoardArray.GetLength(1))
            {
                m_BoardArray[rowIndex, colIndex] = i_PlayerDisc;
            }
            else
            {
                Console.WriteLine("Error: Index out of bounds.");
            }
        }
    }
}
