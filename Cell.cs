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
// Cell.cs
// Creation: 2017/04/14
// Last modification : 2017/04/14

namespace dotLife
{
    /// <summary>
    /// A single cell in the grid which can be dead or alive
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// State of the cell in the current generation
        /// </summary>
        public bool IsAlive {
            get; private set;
        }

        /// <summary>
        /// State that the cell will take in the next generation
        /// </summary>
        /// <returns></returns>
        public bool NextState {
            get; set;
        }

        /// <summary>
        /// Instanciates a dead cell
        /// </summary>
        public Cell() {
            IsAlive = false;
            NextState = false;
        }

        /// <summary>
        /// Instanciates a cell
        /// </summary>
        /// <param name="alive">The state of the newly born cell</param>
        public Cell(bool alive) {
            IsAlive = alive;
            NextState = alive;
        }

        public void ChangeState() {
            IsAlive = NextState;
        }

        public override string ToString() {
            return IsAlive ? "#" : " ";
        }
    }
}
