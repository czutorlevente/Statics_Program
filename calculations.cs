using System;

public class Calculator
{
    //Calculate support reactions on of columns/pillars/pins/rollers etc/) under a beam (or bridge, or other long object with weights on it).
    public static void SupportReactions(string weight_unit, string distance_unit)
    {
        Console.Write("How many weights does this bridge hold? ");
        int weightNumber = int.Parse(Console.ReadLine());


        double[] weights = new double[weightNumber];
        double[] distances = new double[weightNumber];
        double allweights = 0;

        for (int i = 0; i < weightNumber; i++)
        {
            Console.Write($"Enter weight {i + 1}: ");
            weights[i] = double.Parse(Console.ReadLine());
            Console.Write($"Enter distance of weight {i + 1} from pillar 'A' (negative to the right, positive to the left): ");
            distances[i] = double.Parse(Console.ReadLine());
            allweights += weights[i];
        }

        Console.Write($"Enter distance of pillar 'B' from pillar 'A': ");
        double pillar_distance = double.Parse(Console.ReadLine());

        double weight_moments = weights[0]*distances[0];
        string moments_A = $"B(y)*{pillar_distance} + {weights[0].ToString()}*({distances[0].ToString()})"; 

        for (int i = 1; i < weightNumber; i++)
        {
            string documentation = $" + {weights[i]}*{distances[i]}";
            moments_A += documentation;
            weight_moments += weights[i] * distances[i];
        }

        double support_B = Math.Abs(weight_moments / pillar_distance);
        double support_A = allweights - support_B;

        Console.WriteLine($"Moments on A = {moments_A} = 0. So based on that By = {support_B} {weight_unit}");
        Console.WriteLine($"and because A(y) + B(y) = sum of all weights ({allweights} {weight_unit}), A(y) = {support_A}) ");
        Console.WriteLine($"\nSo the support reaction on pillar A is {support_A} {weight_unit} and on B is {support_B} {weight_unit}");

    }

}