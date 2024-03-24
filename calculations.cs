using System;
using System.IO;

public class Calculator
{
    //Calculate support reactions on of columns/pillars/pins/rollers etc/) under a beam (or bridge, or other long object with weights on it).
    public static void SupportReactions(string weight_unit, string distance_unit)
    {
        Console.ForegroundColor = ConsoleColor.White;
        //Console.Write("How many point loads does this bridge hold? ");
        string sentence_1 = "How many point loads does this bridge hold? ";
        Appearance.Highlight(sentence_1, "point");

        int weightNumber = int.Parse(Console.ReadLine());

        //Console.Write("How many distributed loads does this bridge hold? ");
        string sentence_2 = "How many distributed loads does this bridge hold? ";
        Appearance.Highlight(sentence_2, "distributed");

        int distributed_weightNumber = int.Parse(Console.ReadLine());


        double[] weights = new double[weightNumber + distributed_weightNumber];
        double[] distances = new double[weightNumber + distributed_weightNumber];
        double allweights = 0;

        //Add normal point loads
        for (int i = 0; i < weightNumber; i++)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            //Console.Write($"Weight of point load {i + 1} in {weight_unit}: ");
            string sentence_3 = $"Weight of point load {i + 1} in {weight_unit}: ";
            Appearance.Highlight(sentence_3, "Weight");

            weights[i] = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Red;

            //Console.Write($"Distance of point load {i + 1} from pillar 'A' in {distance_unit}\n(positive to the right, negative to the left): ");
            string sentence_4 = $"Distance of point load {i + 1} from pillar 'A' in {distance_unit}\n(positive to the right, negative to the left): ";
            Appearance.Highlight(sentence_4, "Distance");
            distances[i] = double.Parse(Console.ReadLine());
            allweights += weights[i];

            Console.ForegroundColor = ConsoleColor.White;
        }

        //Add distributed loads and convert them to point loads.
        for (int i = 0; i < distributed_weightNumber; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            //Console.Write($"Width of distributed load {i + 1} in {distance_unit}: ");
            string sentence_5 = $"Width of distributed load {i + 1} in {distance_unit}: ";
            Appearance.Highlight(sentence_5, "Width");

            double width_d = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Green;

            //Console.Write($"Distance of closest end of distributed load {i + 1} from pillar 'A'\nin {distance_unit} (positive to the right, negative to the left): ");
            string sentence_6 = $"Distance of closest end of distributed load {i + 1} from pillar 'A'\nin {distance_unit} (positive to the right, negative to the left): ";
            Appearance.Highlight(sentence_6, "Distance");

            double distance_d = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write($"Load amount of distributed load {i + 1} (");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{weight_unit}/{distance_unit}): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"): ");
            
            double amount_d = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.White;

            var distributed_results = DistributedLoads(width_d, amount_d, distance_d);
            weights[weightNumber + i] = distributed_results.Item1;
            distances[weightNumber + i] = distributed_results.Item2;
            allweights += weights[weightNumber + i];

        }

        Console.Write($"Distance of pillar 'B' from pillar 'A' in {distance_unit} \n(positive to the right, negative to the left): ");
        double pillar_distance = double.Parse(Console.ReadLine());

        double weight_moments = weights[0]*distances[0];
        string moments_A = $"B(y)*{pillar_distance} + {weights[0].ToString()}*({distances[0].ToString()})"; 

        for (int i = 1; i < (weightNumber + distributed_weightNumber); i++)
        {
            string documentation = $" + {weights[i]}*{distances[i]}";
            moments_A += documentation;
            weight_moments += weights[i] * distances[i];
        }

        double support_B = (weight_moments / pillar_distance);
        double support_A = allweights - support_B;

        //Console.WriteLine($"\nMoments on A = {moments_A} = 0. So based on that By = {support_B} {weight_unit}");
        //Console.WriteLine($"and because A(y) + B(y) = sum of all weights ({allweights} {weight_unit}), A(y) = {support_A}) ");
        //Console.WriteLine($"\nSo the support reaction on pillar A is {support_A} {weight_unit} and on B is {support_B} {weight_unit}");

        string endLine_1 = $"\nMoments on A = {moments_A} = 0. So based on that By = {support_B} {weight_unit}";
        string endLine_2 = $"and because A(y) + B(y) = sum of all weights ({allweights} {weight_unit}), A(y) = {support_A}) ";
        string endLine_3 = $"\nSo the support reaction on pillar A is {support_A} {weight_unit} and on B is {support_B} {weight_unit}";
        string endline = endLine_1 + endLine_2 + endLine_3;
        Console.WriteLine(endline);
        Manager.Save_Calculation(endline);

        Console.ReadLine();

    }

    //Calculate effect of distributed loads.
    private static (double, double) DistributedLoads(double width, double amount, double distance)
    {
        double load_amount = width * amount;

        if (distance < 0)
        {
            width = -width;
        }
        double load_distance = (width / 2) + distance;

        return (load_amount, load_distance);
    }

}