/* Author: Justin Phillmon
   Date: 7/28/21
   Description: This program simulates the well-known game TicTacToe.  
*/

using System;

namespace TicTacToe
{
    class TicTacToe
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TicTacToe!\n");
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey();

            string playTicTacToe = "YES";               // Set variable to check if users want to keep playing
            Player player1 = new Player(0, 'X');        // Make player 1 
            Player player2 = new Player(0, 'O');        // Make player 2
            
            do 
            {
                Console.Clear();            // Clear the screens
                Board board = new Board();  // Make a new board
                int gameOver = 0;  // Checks if player took their turn.  If player types "quit", then this will be true.
                int turn = 0;       // Keeps track of turns

                do      // Take turns until (1) someone wins, (2) tie game, or (3) someone quits
                {
                    switch (turn % 2)
                    {
                        // Player 1's turn.  
                        case 0: gameOver = GameMethods.PlayerTakeTurn(ref player1, ref board, ref turn);  // If player 1 didn't take their turn, they quit
                                break;
                        // Player 2's turn
                        case 1: gameOver = GameMethods.PlayerTakeTurn(ref player2, ref board, ref turn);  // If player 2 didn't take their turn, they quit
                                break;
                    }
                } while (turn < 9 && gameOver == 0);

                if (turn == 9 && gameOver == 0)  // If the game is a tie
                {
                    GameMethods.PrintBoard(board);
                    Console.WriteLine("This game is a tie.");
                }
                else if (turn < 9 && gameOver == -1)  // If someone quit the game
                    Console.WriteLine("Current game was suspended.");

                do  // Ask if users want to play again.  Get a "yes" or "no" answer
                {
                    Console.WriteLine($"Score:  Player X = {player1.GetScore()}   Player O = {player2.GetScore()}");    // Print score
                    Console.WriteLine("Continue playing? ==> Enter Y for yes or N for no"); // Ask player to enter yes or no
                    playTicTacToe = Console.ReadLine().ToUpper();
                    if (playTicTacToe != "YES" && playTicTacToe != "NO" && playTicTacToe != "Y" && playTicTacToe != "N")
                    {
                        Console.Clear();
                    }
                } while (playTicTacToe != "YES" && playTicTacToe != "NO" && playTicTacToe != "Y" && playTicTacToe != "N");

            } while (playTicTacToe == "YES" || playTicTacToe == "Y");
            Console.WriteLine("Thank you for playing TicTacToe!");
        }
    }
}
