dotLife
=======

## Name
dotLife - An implementation of the Conway's game of life using .NET Core.

## Synopsis
	dotnet dotLife.dll [option]... [file]

## Description
dotLife is a command line program that simulates a two-dimensional space where the rules of game of life apply. [Game of life](http://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) is a cellular automaton invented by John Horton Conway in 1970.

## Modes
By default, dotLife starts in automatic mode. It means that each generation will be shown regularly, the delay is configurable. It can also be interactive or quiet.

`JLife -a=50` Starts the automatic mode with a delay of 50 milliseconds.

`JLife -i` Starts the interactive mode. The user can press Enter to display the next generation.

`JLife -q` Quiet mode. Shows only the last generation.

Note that only one of these three options can be called at once.

## Options
	-a, --auto[=delay]
Starts the automatic mode. `delay` is the time in milliseconds between each generation. By default it is 100.

	-g, --generations=max_gens
Specifies the maximum number of generations to compute.

	-?, --help
Prints short help and exit.

	-i, --interactive
Starts the interactive mode. The user must press Enter to display the next generation.

	-q, --quiet
Computes quickly and show only the last generation.

## Grid Options
The following options configure the random creation of the grid if no file is specified.

	-w, --width=number
Sets the number of cells in the grid width.

	-h, --height=number
Sets the number of cells in the grid height.

	-d, --density=number
Sets the density of living cells. Number from 1 to 10.

## License
Copyright 2017 Pierre Faivre. This is free software, and may be redistributed under the terms specified in the LICENSE file.
