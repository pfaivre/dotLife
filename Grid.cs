// dotLife
// Copyright (C) 2017 Pierre Faivre
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
// 
// Grid.cs
// Creation: 2017/04/12
// Last modification : 2017/04/12

using System;

namespace dotLife
{
    /// <summary>
    /// Contains all the cells and the logic to run the game
    /// </summary>
    public class Grid
    {
        #region Attributes
        private Cell[] _cells;

        private bool _inert;

        /// <summary>
        /// Returns the cell at the index i
        /// </summary>
        public Cell this[uint i] {
            get { return _cells[i]; }
        }

        /// <summary>
        /// Returns the cell at the coordinates (x, y)
        /// </summary>
        public Cell this[uint x, uint y] {
            get { return _cells[x + Width * y]; }
        }

        /// <summary>
        /// Number of cells horizontally
        /// </summary>
        public uint Width { get; }

        /// <summary>
        /// Number of cells vertically
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Number of elapsed generations
        /// </summary>
        public uint Generation { get; private set; }
        
        /// <summary>
        /// Number of cells that are alive in the current generation
        /// </summary>
        /// <returns></returns>
        public uint Population { get; private set; }
        #endregion

        /// <summary>
        /// Creates an empty grid
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Grid(uint width, uint height) {
            Width = width;
            Height = height;
            Generation = 0;
            Population = 0;
            _inert = true;

            _cells = new Cell[Width * Height];
            for (int i = 0 ; i < Width * Height ; i++) {
                _cells[i] = new Cell();
            }
        }

        /// <summary>
        /// Creates a grid with randmly selected cell states
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="density"></param>
        public Grid(uint width, uint height, uint density) {
            Width = width;
            Height = height;
            Generation = 0;
            Population = 0;
            _inert = true;

            if (density < 1 || density > 10)
                throw new ArgumentOutOfRangeException("density", density, "density must be in the range [1;10]");

            _cells = new Cell[Width * Height];
            Random rand = new Random();
            bool state;
            for (int i = 0 ; i < Width * Height ; i++) {
                // 1 chance over "11 - density" that the cell is alive
                state = rand.Next(1, 11 - (int)density) == 1;
                if (state)
                    Population++;
                _cells[i] = new Cell(state);
            }
        }

        /// <summary>
        /// Instanciates a grid from a file
        /// </summary>
        /// <param name="file">A text file to construct the grid</param>
        /// <exception cref="FileNotFoundException">If the file cannot be read.</exception>
        public Grid(string file) {
            var reader = new GridFileReader(file);
            Width = reader.Width;
            Height = reader.Height;
            Population = reader.Population;
            _cells = reader.GetCells();
            Generation = 0;
            _inert = true;
        }

        /// <summary>
        /// Modify the grid to reach the next generation
        /// </summary>
        /// <returns>True if the grid is still active or false if no cell has changed since the last generation</returns>
        public bool NextGeneration() {
            _inert = true;    // Indique si la grille a changé depuis la dernière génération

            // First loop to decide which cell will live
            for (uint i = 0 ; i < Width * Height ; i++) {
                _decideFate(i);
            }

            // Then we apply the changes
            Population = 0;
            for (uint i = 0 ; i < Width * Height ; i++){
                _cells[i].ChangeState();
                if (_cells[i].IsAlive) {
                    Population++;
                }
            }

            Generation++;
            return Population > 0 && !_inert;
        }

        /// <summary>
        /// Heart of the game. Contains the laws that rule the universe
        /// </summary>
        /// <param name="i">Index of the juged cell</param>
        private void _decideFate(uint i) {
            byte livingNeighbors = 0;
            Cell c = _cells[i];
            livingNeighbors = _getLivingNeighbors((uint)i);
            
            // 2 neighbors => the cell keep its state
            if (livingNeighbors == 2) {
                c.NextState = c.IsAlive;
            }
            // 3 neighbors => the cell shall live
            else if (livingNeighbors == 3) {
                c.NextState = true;
            }
            // Else, if there is 0, 1 or more than 3 neighbors => the cell shall die
            else {
                c.NextState = false;
            }

            // Does it still move?
            if (c.IsAlive != c.NextState) {
                _inert = false;
            }
        }

        /// <summary>
        /// Gives the number of living cells around the given one.
        /// If the cell is on the edge, its neighbors are on the other side.
        /// </summary>
        /// <param name="i">Index of the requested cell</param>
        private byte _getLivingNeighbors(uint i) {
            byte count = 0; // Count of living neighbors
            uint x = 0;
            uint y = 0;
            uint xn = 0;
            uint yn = 0;

            // Get the coordinates of the cell given its index
            y = i / Width; // y = index / width (whole division)
            x = i % Width; // x = rest of this division

            // Order of interrogation of neighbors:
            // 1 2 3
            // 4 # 5
            // 6 7 8

            // Neighbor 1 (x-1;y-1)
            xn = x > 0 ? x-1 : Width-1;
            yn = y > 0 ? y-1 : Height-1;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 2 (x;y-1)
            xn = x;
            yn = y > 0 ? y-1 : Height-1;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 3 (x+1;y-1)
            xn = x < Width-1 ? x+1 : 0;
            yn = y > 0 ? y-1 : Height-1;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 4 (x-1;y)
            xn = x > 0 ? x-1 : Width-1;
            yn = y;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 5 (x+1;y)
            xn = x < Width-1 ? x+1 : 0;
            yn = y;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 6 (x-1;y+1)
            xn = x > 0 ? x-1 : Width-1;
            yn = y < Height-1 ? y+1 : 0;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 7 (x;y+1)
            xn = x;
            yn = y < Height-1 ? y+1 : 0;
            if (this[xn, yn].IsAlive) count++;

            // Neighbor 8 (x+1;y+1)
            xn = x < Width-1 ? x+1 : 0;
            yn = y < Height-1 ? y+1 : 0;
            if (this[xn, yn].IsAlive) count++;

            return count;
        }
    }
}
