using System;
using static System.Console;
using static System.Math;

namespace Bme121
{
	static class Program
	{
		static bool useBoxDrawingChars = true;
		static string[ ] letters = { "a", "b", "c", "d", "e", "f", "g",
		  "h", "i", "j", "k", "l" };
		static int boardSize = 8;
		static double cellMarkProb = 0.2;
		static Random rGen = new Random( );

		static void Main( )
		{
			//Game initialization - creating the arrays for the hidden and user board
			bool[ , ] userBoard = new bool [boardSize,boardSize];
			bool[ , ] hiddenBoard = new bool [boardSize,boardSize];

			//creating 1D arrays to store the Row and Column sums for the hidden and user board
			int[] userRowSums = new int[boardSize];
			int[] hiddenRowSums = new int[boardSize];
			int[] userColSums = new int[boardSize];
			int[] hiddenColSums = new int[boardSize];

			//Hidden board generator
			for(int i = 0; i < boardSize; i ++)
			{
				for(int j = 0; j < boardSize; j ++)
				{
					if(rGen.NextDouble( ) < cellMarkProb )
					{
						hiddenBoard[i,j] = true;
						hiddenRowSums[i] += j + 1; //adds up the row & col values
						hiddenColSums[j] += i + 1;
					}
					else
					{
						hiddenBoard[i,j] = false;
					}
				}
			}

			//Main game loop
			bool gameNotQuit = true;
			bool gameNotWon = true;
			
			while( gameNotQuit && gameNotWon )
			{
				Console.Clear( );
				WriteLine( );
				WriteLine( "    Play Kakurasu!" );
				WriteLine( );

				if( useBoxDrawingChars )
				{
					for( int row = 0; row < boardSize; row ++ )
					{
						if( row == 0 )
						{
							Write( "        " );
							for( int col = 0; col < boardSize; col ++ )
							Write( "  {0} ", letters[ col ] );
							WriteLine( );

							Write( "        " );
							for( int col = 0; col < boardSize; col ++ )
							Write( " {0,2} ", col + 1 );
							WriteLine( );

							Write( "        \u250c" );
							for( int col = 0; col < boardSize - 1; col ++ )
							Write( "\u2500\u2500\u2500\u252c" );
							WriteLine( "\u2500\u2500\u2500\u2510" );
						}

						Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );

						for( int col = 0; col < boardSize; col ++ )
						{
							if(userBoard[row,col] == true)  Write( " X \u2502" );
							else Write( "   \u2502" );
						}
						Write( "{0,3}", hiddenRowSums[row]);
						Write( "{0,3}", userRowSums[row]);

						WriteLine(  );

						if( row < boardSize - 1 )
						{
							Write( "        \u251c" );
							for( int col = 0; col < boardSize - 1; col ++ )
							Write( "\u2500\u2500\u2500\u253c" );
							WriteLine( "\u2500\u2500\u2500\u2524" );
						}
						else
						{
							Write( "        \u2514" );
							for( int col = 0; col < boardSize - 1; col ++ )
							Write( "\u2500\u2500\u2500\u2534" );
							WriteLine( "\u2500\u2500\u2500\u2518" );

							Write( "       " );
							for( int col = 0; col < boardSize; col ++ )
							Write("{0,4}",hiddenColSums[col]);
							WriteLine( );

							Write( "       " );
							for( int col = 0; col < boardSize; col ++ )
							Write( "{0,4}", userColSums[col]);
							WriteLine( );
						}
					}
				}

				WriteLine( );
				WriteLine( "   Toggle cells to match the row and column sums." );
				Write(     "   Enter a row-column letter pair or 'quit': " );
				string response = ReadLine( );

				if( response == "quit" ) gameNotQuit = false;
				else
				{
					if(response.Length == 2)
					{
						string rowPick = response.Substring(0,1);
						string colPick = response.Substring(1,1);

						int rowNum = Array.IndexOf(letters,rowPick);
						int colNum = Array.IndexOf(letters,colPick);

						if( rowNum >= 0 && rowNum < boardSize && colNum >= 0 && colNum < boardSize)
						{
							if(userBoard[rowNum,colNum] == true)
							{
								userBoard[rowNum,colNum] = false;
								userRowSums[rowNum] -= colNum + 1;
								userColSums[colNum] -= rowNum + 1;
							}
							else
							{
								userBoard[rowNum,colNum] = true;
								userRowSums[rowNum] += colNum + 1;
								userColSums[colNum] += rowNum + 1;
							}
						}
            
						int solved = 0;
						
						//loop checks for equal values with solved counter
						for( int row = 0; row < boardSize; row ++ )
						{
							if( userRowSums[row] == hiddenRowSums[row] && userColSums[row] == hiddenColSums[row])
							{
								solved++;
							}
						}
			
						//to ensure its checking for all the equal values on board 
						if(solved == boardSize)
						{
							gameNotWon = false;
              
							//Clears Screen and displays winning statement
							Console.Clear( );
							WriteLine(" ");
							WriteLine("         Congrats! ");
							WriteLine(" ");
							
							
							if( useBoxDrawingChars )
							{
								for( int row = 0; row < boardSize; row ++ )
								{
									if( row == 0 )
									{
										Write( "        " );
										for( int col = 0; col < boardSize; col ++ )
										Write( "  {0} ", letters[ col ] );
										WriteLine( );

										Write( "        " );
										for( int col = 0; col < boardSize; col ++ )
										Write( " {0,2} ", col + 1 );
										WriteLine( );

										Write( "        \u250c" );
										for( int col = 0; col < boardSize - 1; col ++ )
										Write( "\u2500\u2500\u2500\u252c" );
										WriteLine( "\u2500\u2500\u2500\u2510" );
									}

									Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );

									for( int col = 0; col < boardSize; col ++ )
									{
										if(userBoard[row,col] == true)  Write( " X \u2502" );
										else Write( "   \u2502" );
									}
									Write( "{0,3}", hiddenRowSums[row]);
									Write( "{0,3}", userRowSums[row]);

									WriteLine(  );

									if( row < boardSize - 1 )
									{
										Write( "        \u251c" );
										for( int col = 0; col < boardSize - 1; col ++ )
										Write( "\u2500\u2500\u2500\u253c" );
										WriteLine( "\u2500\u2500\u2500\u2524" );
									}
									else
									{
										Write( "        \u2514" );
										for( int col = 0; col < boardSize - 1; col ++ )
										Write( "\u2500\u2500\u2500\u2534" );
										WriteLine( "\u2500\u2500\u2500\u2518" );

										Write( "       " );
										for( int col = 0; col < boardSize; col ++ )
										Write("{0,4}",hiddenColSums[col]);
										WriteLine( );

										Write( "       " );
										for( int col = 0; col < boardSize; col ++ )
										Write( "{0,4}", userColSums[col]);
										WriteLine( );
									}
								}
							}
							//Displays happy message after winning board
							WriteLine( );
							WriteLine( );
							WriteLine("    (✿◠‿◠)  			     	(づ｡◕‿‿◕｡)づ");
							WriteLine("                    ≧◡≦  ");
							WriteLine( );
							WriteLine("                   You Won!"	);
							WriteLine( );
							WriteLine("  (づ｡◕‿‿◕｡)づ                      (ﾉ◕ヮ◕)ﾉ*:･ﾟ✧ ");
							WriteLine( );
							WriteLine("                 ≧◡≦");
							WriteLine( );
						}
					}
				}
			}
		}
	}
}
		
