namespace TicTacToe
{
    class Player
    {
        // Properties
        private int score;
        private char symbol;

        // Default Construtor
        public Player()
        {
            score = 0;
            symbol = '!';
        }

        // Construtor
        public Player(int score, char symbol)
        {
            this.score = score;
            this.symbol = symbol;
        }

        // Getters-Setters
        public int GetScore()
        {
            return this.score;
        }

        public void SetScore(int score)
        {
            this.score = score;
        }

        public char GetSymbol()
        {
            return this.symbol;
        }

        public void SetSymbol(char symbol)
        {
            this.symbol = symbol;
        }

        // Methods
        public void IncreaseScore()
        {
            this.score += 1;
        }
    }
}