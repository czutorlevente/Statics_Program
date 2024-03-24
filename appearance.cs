using System;

public class Appearance
{

    //Highlight (make yellow) any selected word in a string sentence.
    public static void Highlight(string sentence, string word_1)
    {
        string[] words = sentence.Split(' ');

        foreach (var word in words)
        {
            if (word == word_1)
            {
                ConsoleColor previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(word + " ");
                Console.ForegroundColor = previousColor;
                
            }
            else
            {
                Console.Write(word + " ");
            }
        }
    }
}