namespace TicTacToe
{
    class Cell
    {   
        // Properties
        private char value;

        // Default Constructor
        public Cell()
        {
            value = ' ';
        }

        // Constructor
        public Cell(char c)
        {
            value = c;
        }

        // Getters-Setters     
        public char GetValue()
        {
            return value;
        }

        public void SetValue(char c)
        {
            value = c;
        }
    }
}