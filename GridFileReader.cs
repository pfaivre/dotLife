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
// GridFileReader.cs
// Creation: 2017/06/01
// Last modification : 2017/06/01

using System.IO;

namespace dotLife
{
    /// <summary>
    /// This class is used to read a grid from a text file.
    /// The reading system is relatively permissive:
    ///  - All the '#' charachters a considered as living cells;
    ///  - All the other ones are considered as dead cells;
    ///  - The length of the longest line determines the width of the grid;
    ///  - The number of lines determines the height of the grid.
    /// </summary>
    public class GridFileReader
    {
        /// <summary>
        /// The length of the longest line of the file gives the width of the grid
        /// </summary>
        public uint Width {
            get; private set;
        }

        /// <summary>
        /// The number of lines in the file give the height of the grid
        /// </summary>
        public uint Height {
            get; private set;
        }

        /// <summary>
        /// The number of '#' characters in the file gives the population
        /// </summary>
        public uint Population {
            get; private set;
        }

        /// <summary>
        /// File name as given in the constructor
        /// </summary>
        public string FileName {
            get; private set;
        }
        
        /// <summary>
        /// Instanciates a GridFileReader. It will immediatelly read the file to exctract the dimensions
        /// </summary>
        /// <param name="fileName">File to read</param>
        /// <exception cref="FileNotFoundException">If the file cannot be read.</exception>
        public GridFileReader(string fileName) {
            FileName = fileName;
            Width = 0;
            Height = 0;
            Population = 0;
            
            uint lineWidth = 0;

            // *** Computes the dimensions of the grid ***
            var fs = new FileStream(fileName, FileMode.Open);
            using (var stream = new StreamReader(fs)) {
                int c = stream.Read();
                while (c != -1) {  // Loop on the file
                    while (c != '\n' && c != -1) { // Loop on the line
                        if (c == '#')
                            Population++;
                        lineWidth++;
                        c = stream.Read();
                    }
                    // At the end of the line, we check if it is the longest one
                    if (lineWidth > Width) {
                        Width = lineWidth;
                    }
                    lineWidth = 0;
                    Height++;
                    
                    c = stream.Read();
                }
            }
        }
        
        /// <summary>
        /// Instanciates an array of cells by reading the file
        /// </summary>
        /// <exception cref="Exception">If the file cannot be read.</exception>
        public Cell[] GetCells() {
            Cell[] cells = new Cell[Width * Height];
            
            uint posLine = 0; // Position of the cursor on the ligne.
            uint i = 0;

            var fs = new FileStream(FileName, FileMode.Open);
            using (var stream = new StreamReader(fs)) {
                int c = stream.Read();
                while (c != -1) { // Loop on the file
                    while (c != '\n' && c != -1) { // Loop on the line
                        cells[i++] = new Cell(c == '#');
                        posLine++;
                        c = stream.Read();
                    }
                    // À la fin de la ligne du fichier, on rajoute des cases pour compléter la ligne de la grille.
                    for (;posLine < Width ; posLine++) {
                        cells[i++] = new Cell();
                    }
                    posLine = 0;
                    
                    c = stream.Read();
                }
            }

            return cells;
        }
    }
}
