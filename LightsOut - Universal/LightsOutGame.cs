using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut___Universal
{
    public class LightsOutGame
    {
        private int gridSize;
        private bool[,] grid;           // Store the on/off state of the grid
        private Random rand;

        public const int MaxGridSize = 10;
        public const int MinGridSize = 3;
             
        public int GridSize
        {
            get
            {
                return gridSize;
            }
            set
            {
                if (value != gridSize && value >= MinGridSize && value <= MaxGridSize)
                {
                    gridSize = value;
                    grid = new bool[gridSize, gridSize];
                    NewGame();
                }
            }
        }
        public string Grid
        {
            get
            {
                string TF = "";
                // Write this method to convert the 2D grid array into "TFTTFFTTT"
                for(int i = 0; i<GridSize;i++)
                {
                    for(int j = 0; j<GridSize;j++)
                    {
                        if (grid[i,j])
                        {
                            TF += "T";
                        }
                        else
                        {
                            TF += "F";
                        }
                    }
                }
                return TF;
            }
            set
            {
                // Write this method to set the 2D grid array to values from "TFTTFFTTT"
                for(int i = 0; i<GridSize;i++)
                {
                    for(int j = 0; j<GridSize;j++)
                    {
                        if(value[i * gridSize + j] == 'T')
                        {
                            grid[i, j] = true;
                        }
                        else
                        {
                            grid[i, j] = false;

                        }
                        
                    }
                }

            }
        }

        public LightsOutGame()
        {
            rand = new Random();
            GridSize = MinGridSize;            
        }


        public bool GetGridValue(int row, int col)
        {
            return grid[row, col];
        }

        public void NewGame()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    grid[r, c] = rand.Next(2) == 1;
                }
            }
        }

        public void Move(int row, int col)
        {
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                throw new ArgumentException("Row or column is outside the legal range of 0 to " 
                    + (gridSize - 1));
            }

            // Invert selected box and all surrounding boxes
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    if (grid[r, c])
                    {
                        return false;
                    }
                }
            }

            // All values must be false (off)
            return true;
        }
        
    }
}
