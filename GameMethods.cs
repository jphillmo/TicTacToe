using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class GameMethods
    {
        // This function prints the board and asks the player to choose two numbers that correspond to (x, y) coordinates on the game board.
        // If user enters "quit", the tuple (-1, -1) will be returned and indicates the user wants to end the game.
        // Otherwise, user must enter two numbers between 1 and 3.  If correct values are entered, the function will 
        // Error checking prevents other types of input from being accepted.
        public static (int, int) GetMove(Player player, Board board)
        {
            bool goodInput = false;         // Make flag to check if cell has been chosen already
            (int, int) coordinates = (-1, -1);    // Make coordinate variable.  If user enters quit, (-1, -1) will be passed to return statement

            while (!goodInput)
            {
                PrintBoard(board);      // Print the board

                string input = GetUserInput(player);  // Get user input from player
                if (input.ToUpper() == "QUIT")  // If user entered "quit", break out of while loop
                    break;
              
                List<int> numList = GetNumbersFromInput(input);  // Get numbers from user input

                if (NumListIsValid(numList))    // If numbers are valid
                    SetCoordinatesAndFlag(numList, ref coordinates.Item1, ref coordinates.Item2, ref goodInput);  // Set coordinates and flag
                else
                    BadUserInput();     // Tell user the input was invalid
            }
            return coordinates; // Return coordinates
        }

        // This function gets a potential move from the user.  It will return a string value.
        public static string GetUserInput(Player player)
        {
            Console.WriteLine($"Player \"{player.GetSymbol()}\", it is your turn to make a move.");  // Prompt user
            Console.Write("Enter two numbers between 1 and 3 separated by spaces: ");                // Tell them to make a move
            return Console.ReadLine();          // Get input from user
        }

        // This function splits the string input and parses it for numbers.  It puts these numbers in a list and returns the list.
        public static List<int> GetNumbersFromInput(string input)
        {
            List<int> numList = new List<int>();        // Make list to hold coordinates
            foreach (string s in input.Split())         // Read line and split the user input into separate strings
            {
                if (int.TryParse(s, out int number))    // If value is a number, add to the coordinate list
                    numList.Add(number);
            }
            return numList;
        }

        // This function checks if the number list is valid or not.  It is considered valid if:
        // - the list contains exactly two numbers
        // - the value of each number is between 1 and 3
        // The function will return a true or false value based these properties.
        public static bool NumListIsValid(List<int> coordinates)
        {
            if (coordinates.Count == 2 && coordinates[0] > 0 && coordinates[1] > 0 && coordinates[0] < 4 && coordinates[1] < 4)
                return true;
            else
                return false;
        }

        // This function is used to assign coordinates to the move that the user chose as well as indicating the user input was valid.
        public static void SetCoordinatesAndFlag(List<int> numList, ref int x, ref int y, ref bool inputFlag)
        {
            Console.WriteLine("You entered: " + Convert.ToString(numList[0]) + " " + Convert.ToString(numList[1]));  // Output move to user
            x = numList[0] - 1;      // Set x coordinate
            y = numList[1] - 1;      // Set y coordinate
            inputFlag = true;        // Set input flag
        }

        // This function tells the user that the input was bad, waits for a key to be pressed, then clears the screen.
        public static void BadUserInput()
        {
            Console.Write("Bad input...press any key to cotinue.");   // Tell user input was bad
            Console.ReadKey();     // Pause and wait for user to hit a key
            Console.Clear();       // Clear the screen
        }

        // This function checks if the move that the player chose is valid (i.e. is the chosen cell empty).
        public static bool CheckMove(Board board, (int, int) coordinates)
        {
            if (coordinates.Item1 == -1 || coordinates.Item2 == -1)     // If the player wishes to quit
                return true;
            else if (board.GetCell(coordinates.Item1, coordinates.Item2) == '#')    // If the cell is empty
                return true;
            else        // Else, cell is not empty
            {
                Console.WriteLine("That space is already occupied, please pick another one...press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

        }

        // This function sets the chosen cell on the board with a symbol associated with the player.
        public static void MakeMove(Player player, ref Board board, (int, int) coordinates)
        {
            board.SetCell(player.GetSymbol(), coordinates.Item1, coordinates.Item2);
        }

        public static bool CheckIfPlayerWins(Player player, Board board)
        {
            char [] cellTriplet = new char[3];
            
            // Check for horizontal wins
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    cellTriplet[x] = board.GetCell(x, y);
                }
                if (cellTriplet[0] == player.GetSymbol() && cellTriplet[1] == player.GetSymbol() && cellTriplet[2] == player.GetSymbol())
                    return true;
            }

            // Check for vertical wins
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    cellTriplet[y] = board.GetCell(x, y);
                }
                if (cellTriplet[0] == player.GetSymbol() && cellTriplet[1] == player.GetSymbol() && cellTriplet[2] == player.GetSymbol())
                    return true;
            }

            // Check for diagonal wins
            cellTriplet[0] = board.GetCell(0, 0);
            cellTriplet[1] = board.GetCell(1, 1);
            cellTriplet[2] = board.GetCell(2, 2);
            if (cellTriplet[0] == player.GetSymbol() && cellTriplet[1] == player.GetSymbol() && cellTriplet[2] == player.GetSymbol())
                return true;
            
            cellTriplet[0] = board.GetCell(2, 0);
            cellTriplet[2] = board.GetCell(0, 2);
            if (cellTriplet[0] == player.GetSymbol() && cellTriplet[1] == player.GetSymbol() && cellTriplet[2] == player.GetSymbol())
                return true;

            return false;
        }

        // This function is allows a player to take a turn in TicTacToe.  If the player takes a turn and there is no winner, return 0.
        // If the player takes a turn and wins, return 1.  If the player chooses to "quit", return -1.
        public static int PlayerTakeTurn(ref Player player, ref Board board, ref int turn)
        {
            (int, int) move;      // Hold player move
            do
            {
                move = GameMethods.GetMove(player, board);  // Get move from player.  If coordinates = (-1, -1), quit the game, else continue
            } while (!GameMethods.CheckMove(board, move));   // If cell is not empty, tell player and ask for another move.
            
            if (move.Item1 == -1 || move.Item2 == -1)           // If the player wants to quit
                return -1;                                      // Return 0              
            else                                                // Else
                GameMethods.MakeMove(player, ref board, move);  // Player makes a move

            if (GameMethods.CheckIfPlayerWins(player, board))   // Check if the player won
            {
                Console.WriteLine($"Player {player.GetSymbol()} wins!");    // If player wins, tell the player they won and
                player.IncreaseScore();                                     // increase the player score
                return 1;           // If player wins, return 1
            }

            Console.Clear();        
            turn++;                 
            return 0;        // Player took a move, but there is no winner yet, return 0
        }
        // This function prints the game board.
        public static void PrintBoard(Board board)
        {
            for (int y = 0; y < 3; y++) 
            {
                for (int x = 0; x < 3; x++)
                {
                    if (x < 2)
                        Console.Write(" " + board.GetCell(x, y) + " |");
                    else
                        Console.WriteLine(" " + board.GetCell(x, y));
                }
                if (y < 2)
                    Console.WriteLine("-----------");
            }
        }
    }
}