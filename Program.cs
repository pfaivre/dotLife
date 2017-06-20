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
// Program.cs
// Creation: 2017/04/14
// Last modification : 2017/06/20

using System;
using Microsoft.Extensions.CommandLineUtils;

namespace dotLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "dotLife";
            app.HelpOption("-?|--help");

            #region Options and Arguments
            var aOption = app.Option("-a|--auto <delay>",
                                     "Automatic mode with a specified delay",
                                     CommandOptionType.SingleValue);
            
            var iOption = app.Option("-i|--interactive",
                                     "Interactive mode",
                                     CommandOptionType.NoValue);
            
            var qOption = app.Option("-q|--quiet",
                                     "Quiet mode",
                                     CommandOptionType.NoValue);

            var gOption = app.Option("-g|--generations <number>",
                                     "Maximum number of generations (default 100)",
                                     CommandOptionType.SingleValue);

            var wOption = app.Option("-w|--width <number>",
                                     "Number of cells in the grid width (default 50)",
                                     CommandOptionType.SingleValue);
            
            var hOption = app.Option("-h|--height <number>",
                                     "Number of cells in the grid height (default 20)",
                                     CommandOptionType.SingleValue);

            var dOption = app.Option("-d|--density <number>",
                                     "Density of living cells (between 1 and 10)",
                                     CommandOptionType.SingleValue);

            var fArgument = app.Argument("[file]", "", false);
            #endregion

            app.OnExecute(() => {
                var options = new GameOptions();

                #region Parameters Parsing
                // Mode
                if (!aOption.HasValue() && !iOption.HasValue() && !qOption.HasValue())
                    options.Mode = GameMode.AUTO;
                else if (aOption.HasValue() && !iOption.HasValue() && !qOption.HasValue()) {
                    options.Mode = GameMode.AUTO;
                    bool success = Int32.TryParse(aOption.Value(), out options.Delay);
                    if (!success || options.Delay < 0)
                        throw new CommandParsingException(null, "A positive number must be specified with the option auto");
                }
                else if (!aOption.HasValue() && iOption.HasValue() && !qOption.HasValue())
                    options.Mode = GameMode.INTERACTIVE;
                else if (!aOption.HasValue() && !iOption.HasValue() && qOption.HasValue())
                    options.Mode = GameMode.QUIET;
                else
                    throw new CommandParsingException(null, "Only one mode can be selected between auto, interactive and quiet");

                // Max gen
                Program._parseUInt32(gOption, ref options.MaxGeneration, "generations");

                // Width
                Program._parseUInt32(wOption, ref options.Width, "width");

                // Height
                Program._parseUInt32(hOption, ref options.Height, "height");

                // Density
                Program._parseUInt32(dOption, ref options.Density, "density");

                // File
                options.File = fArgument.Value;
                
                #endregion

                Game game = new Game(options);
                game.Start();
                
                return 0;
            });

            try {
                app.Execute(args);
            }
            catch (CommandParsingException ex) {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Check if the given option is a valid positive integer and extract its value
        /// </summary>
        /// <param name="co">Command Line Option to parse</param>
        /// <param name="value">Reference of the variable store the value</param>
        /// <param name="name">Name of the option, displayed in case of an error</param>
        private static void _parseUInt32(CommandOption co, ref UInt32 value, string name) {
            UInt32 result = 0;
            if (co.HasValue()) {
                bool success = UInt32.TryParse(co.Value(), out result);
                if (!success || result <= 0)
                    throw new CommandParsingException(null, $"A positive number must be specified with the option {name}");
                value = result;
            }
        }
    }
}
