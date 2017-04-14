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
// Last modification : 2017/04/14

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
            
            #endregion

            app.OnExecute(() => {
                
                #region Parameters Parsing
                
                #endregion
                
                return 0;
            });

            try {
                app.Execute(args);
            }
            catch (CommandParsingException ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
