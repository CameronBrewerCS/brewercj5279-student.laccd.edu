// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;

public class CarRecord
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{Year} {Make} {Model}, ${Price:F2}";
    }
}

public class CarRecordsProgram
{
    private static List<CarRecord> validCarRecords = new List<CarRecord>();
    private const string filePath = "car_records.txt";

    public static void Main()
    {
        int choice;
        do
        {
            Console.WriteLine("\nCar Records Management System");
            Console.WriteLine("1. Read Car Records from File");
            Console.WriteLine("2. Display Valid Car Records");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            choice = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (choice)
            {
                case 1:
                    ReadCarRecordsFromFile();
                    break;
                case 2:
                    DisplayValidCarRecords();
                    break;
                case 3:
                    Console.WriteLine("Exiting program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 3);
    }

    private static void ReadCarRecordsFromFile()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        validCarRecords.Clear();
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length == 4 &&
                int.TryParse(parts[2], out int year) &&
                decimal.TryParse(parts[3], out decimal price))
            {
                validCarRecords.Add(new CarRecord { Make = parts[0], Model = parts[1], Year = year, Price = price });
            }
            else
            {
                Console.WriteLine($"Invalid record: {line}");
            }
        }
        Console.WriteLine($"{validCarRecords.Count} valid records loaded.");
    }

    private static void DisplayValidCarRecords()
    {
        if (validCarRecords.Count == 0)
        {
            Console.WriteLine("No valid car records available.");
            return;
        }
        Console.WriteLine("\nValid Car Records:");
        foreach (var record in validCarRecords)
        {
            Console.WriteLine(record);
        }
    }
}
