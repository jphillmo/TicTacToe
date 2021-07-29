namespace TicTacToe
{
    class Board
    {
        // Properties
        private Cell[,] board;
        
        // Default Constructor
        public Board()
        {
            board = new Cell[3, 3];
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    board[x, y] = new Cell('#');
                }
            }
        }
        
        // Getters-Setters
        public char GetCell(int x, int y)
        {
            return board[x, y].GetValue();
        }

        public void SetCell(char c, int x, int y)
        {
            board[x, y].SetValue(c);
        }
    }
}