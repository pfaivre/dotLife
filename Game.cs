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
// Game.cs
// Creation: 2017/06/15
// Last modification : 2017/06/15

using System;
using System.Threading;

namespace dotLife
{
    /// <summary>
    /// A standalone Game of Life instance featuring a grid
    /// </summary>
    public class Game
    {
        private GameOptions _o;
        private Grid _grid;

        /// <summary>
        /// Instanciates a new game
        /// </summary>
        /// <param name="options">An instance of GameOptions giving the game parameters</param>
        /// <exception cref="FileNotFoundException">If a file is specified in the options and cannot be read.</exception>
        public Game(GameOptions options) {
            _o = options;

            if (options.File != null) {
                _grid = new Grid(_o.File);
            }
            else {
                _grid = new Grid(_o.Width, _o.Height, _o.Density);
            }
        }

        public void Start() {
            switch (_o.Mode) {
                case GameMode.AUTO:
                    _processDelay();
                    break;
                case GameMode.INTERACTIVE:
                    _processManual();
                    break;
                case GameMode.QUIET:
                    _processQuiet();
                    break;
            }
        }

        /// <summary>
        /// Executes the grid automatically with a delay
        /// </summary>
        private void _processDelay() {
            int i = 0;
            bool isInert = false;

            Display.DrawGrid(_grid);
            
            i = 0;
            while (i < _o.MaxGeneration && !isInert) {
                isInert = !_grid.NextGeneration();
                Display.DrawGrid(_grid);
                Thread.Sleep(_o.Delay);
                i++;
            }
        }

        /// <summary>
        /// Executes the grid waiting for the user to pass each generation
        /// </summary>
        private void _processManual() {
            int i = 0;
            bool isInert = false;

            Display.DrawGrid(_grid);
            
            i = 0;
            while (i < _o.MaxGeneration && !isInert) {
                isInert = !_grid.NextGeneration();
                Display.DrawGrid(_grid);
                Console.ReadLine();
                i++;
            }
        }

        /// <summary>
        /// Executes the grid as fast as possible and only displays the final result
        /// </summary>
        private void _processQuiet() {
            int i = 0;
            bool isInert = false;

            Display.DrawGrid(_grid);
            
            i = 0;
            while (i < _o.MaxGeneration && !isInert) {
                isInert = !_grid.NextGeneration();
                i++;
            }

            Display.DrawGrid(_grid);
        }
    }
}
