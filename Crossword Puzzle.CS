using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'crosswordPuzzle' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. STRING_ARRAY crossword
     *  2. STRING words
     */

    public static List<string> crosswordPuzzle(List<string> crossword, string words)
    {
        char[][] grid = crossword.Select(r => r.ToCharArray()).ToArray();
        string[] wordList = words.Split(';');
        Solve(grid, wordList, 0);
        return grid.Select(r => new string(r)).ToList();
    }

    private static bool Solve(char[][] grid, string[] words, int index)
    {
        if (index == words.Length)
            return true;

        string word = words[index];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (CanPlaceHorizontal(grid, word, i, j))
                {
                    char[] original = PlaceHorizontal(grid, word, i, j);
                    if (Solve(grid, words, index + 1)) return true;
                    RemoveHorizontal(grid, original, i, j);
                }
                if (CanPlaceVertical(grid, word, i, j))
                {
                    char[] original = PlaceVertical(grid, word, i, j);
                    if (Solve(grid, words, index + 1)) return true;
                    RemoveVertical(grid, original, i, j);
                }
            }
        }
        return false;
    }

    private static bool CanPlaceHorizontal(char[][] grid, string word, int row, int col)
    {
        if (col + word.Length > 10) return false;
        if (col > 0 && grid[row][col - 1] != '+') return false;
        if (col + word.Length < 10 && grid[row][col + word.Length] != '+') return false;

        for (int i = 0; i < word.Length; i++)
        {
            if (grid[row][col + i] != '-' && grid[row][col + i] != word[i])
                return false;
        }
        return true;
    }

    private static bool CanPlaceVertical(char[][] grid, string word, int row, int col)
    {
        if (row + word.Length > 10) return false;
        if (row > 0 && grid[row - 1][col] != '+') return false;
        if (row + word.Length < 10 && grid[row + word.Length][col] != '+') return false;

        for (int i = 0; i < word.Length; i++)
        {
            if (grid[row + i][col] != '-' && grid[row + i][col] != word[i])
                return false;
        }
        return true;
    }

    private static char[] PlaceHorizontal(char[][] grid, string word, int row, int col)
    {
        char[] original = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            original[i] = grid[row][col + i];
            grid[row][col + i] = word[i];
        }
        return original;
    }

    private static void RemoveHorizontal(char[][] grid, char[] original, int row, int col)
    {
        for (int i = 0; i < original.Length; i++)
            grid[row][col + i] = original[i];
    }

    private static char[] PlaceVertical(char[][] grid, string word, int row, int col)
    {
        char[] original = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            original[i] = grid[row + i][col];
            grid[row + i][col] = word[i];
        }
        return original;
    }

    private static void RemoveVertical(char[][] grid, char[] original, int row, int col)
    {
        for (int i = 0; i < original.Length; i++)
            grid[row + i][col] = original[i];
    }
}


    
class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        List<string> crossword = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            string crosswordItem = Console.ReadLine();
            crossword.Add(crosswordItem);
        }

        string words = Console.ReadLine();

        List<string> result = Result.crosswordPuzzle(crossword, words);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
