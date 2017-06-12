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
// Display.cs
// Creation: 2017/06/12
// Last modification : 2017/06/12

using System;
using System.Text;

namespace dotLife
{
    /// <summary>
    /// Handles the visual output
    /// </summary>
    public class Display
    {
        public static void DrawGrid(Grid grid) {
            int currentColumn = 0;
            StringBuilder output = new StringBuilder((int)(grid.Width * grid.Height) + 64);

            // Upper line
            for (int i = 0; i < grid.Width; i++) {
                output.Append('_');
            }
            output.Append(Environment.NewLine);

            // Grid content
            for (uint i = 0; i < grid.Width * grid.Height; i++) {
                output.Append(grid[i]);

                currentColumn++;

                // End of line
                if (currentColumn == grid.Width) {
                    output.Append('|').Append(Environment.NewLine);
                    currentColumn = 0;
                }
            }
            output.Append("Generation : ").Append(grid.Generation).Append(" ; population : ").Append(grid.Population);

            Console.WriteLine(output.ToString());
        }
    }
}
