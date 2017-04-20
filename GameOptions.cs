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
// GameOptions.cs
// Creation: 2017/04/20
// Last modification : 2017/04/20

namespace dotLife
{
    /// <summary>
    /// Stores all the paramters of a game
    /// </summary>
    public class GameOptions
    {
        /// <summary>
        /// Delay to wait between each generation in automatic mode in milliseconds (default 100)
        /// </summary>
        public int Delay = 100;

        /// <summary>
        /// Game mode (default AUTO)
        /// </summary>
        public GameMode Mode = GameMode.AUTO;

        /// <summary>
        /// Number of generations to compute before stopping (default 100)
        /// </summary>
        public uint MaxGeneration = 100;

        /// <summary>
        /// Width of the grid if no file is specified (default 50)
        /// </summary>
        public uint Width = 50;

        /// <summary>
        /// Height of the grid if no file is specified (default 20)
        /// </summary>
        public uint Height = 20;

        /// <summary>
        /// Density of living cell in the grid if no file is specified in the range [1;10] (default 5) 
        /// </summary>
        public uint Density = 5;

        /// <summary>
        /// Path to the faile to load (default null)
        /// </summary>
        public string File = null;
    }
}
