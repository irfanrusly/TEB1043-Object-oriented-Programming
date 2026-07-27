using System;

public class Homework
{
    struct SensorData
    {
        public double Temperature;
        public int time;
    }

    public static void Main()
    {
      
        bool keepRunning = true;
        while(keepRunning){
        SensorData[] readings = new SensorData[10];
        Console.WriteLine("Enter 10 temperature readings");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"Time {i + 1} - Temperature: ");
            readings[i].Temperature = Convert.ToDouble(Console.ReadLine());
            readings[i].time = i + 1;
        }
        bool InMenu = true;
        while (InMenu)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("a. Min Temperature");
                Console.WriteLine("b. Max Temperature");
                Console.WriteLine("c. Average Temperature");
                Console.WriteLine("d. Temperature greater than a threshold");
                Console.WriteLine("e. Reread the data");
                Console.WriteLine("f. Exit");
                Console.Write("Choose an option: ");
                char choice = Console.ReadLine()[0];
                switch(choice)
                {
                    case 'a':
                    double minTemp = readings[0].Temperature;
                    for (int i = 1; i < readings.Length; i++)
                        {
                            if (readings[i].Temperature < minTemp)
                            {
                                minTemp = readings[i].Temperature;
                            }
                        }
                    Console.WriteLine($"Minimum Temperature: {minTemp}");
                    break;
                    
                    case 'b':
                    double maxTemp = readings[0].Temperature;
                    for (int i = 1; i < readings.Length; i++)
                        {
                            if (readings[i].Temperature > maxTemp)
                            {
                                maxTemp = readings[i].Temperature;
                            }
                            
                        }
                        Console.WriteLine($"Max Temperature: {maxTemp}");
                        break;

                    case 'c':
                    double sumTemp = 0;
                    for (int i = 0; i <readings.Length; i++)
                        {
                            sumTemp += readings[i].Temperature;
                        }
                        double avgTemp = sumTemp / readings.Length;
                        Console.WriteLine($"Average Temperature is {avgTemp}");
                        break;

                    case 'd':
                    Console.WriteLine("Enter threshold:");
                    double threshold = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine($"Times with temp > {threshold}:");
                    foreach (var r in readings)
                        {
                            if(r.Temperature > threshold)
                            Console.WriteLine($"- Minute {r.time}: {r.Temperature}");
                        }
                        break;
                    case 'e':
                    InMenu = false;
                    break;
                    
                    case 'f':
                    InMenu = false;
                    keepRunning = false;
                    break;
                    
                    default:
                    Console.WriteLine("Invalid option. Please choose a letter from a to f.");
                    break;


                        
                      


                }
            }
        }
    }
}
