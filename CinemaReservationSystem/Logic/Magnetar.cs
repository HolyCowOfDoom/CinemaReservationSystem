public class Magnetar
{
    private int credits;
    private int counter;
    private bool playMagnetar = true; 

    public Magnetar(int credits)
    {
        this.credits = credits;
    }

    public void Play()
    {
        if (credits < 20) return;
        while (playMagnetar)
        {
            Console.Clear();
            Console.WriteLine($"{credits} Credits detected.");
            Console.WriteLine("Takes 20 credits to play. Y/N");

            ConsoleKeyInfo key = Console.ReadKey(true);
            string? input = Convert.ToString(key.Key);
            switch (input)
            {
                case "Y":
                    credits -= 20;
                    counter = 1;
                    while (counter < 20)
                    {
                        Console.Clear();
                        Console.WriteLine($"Counter: {counter}");

                        int winAmount = counter switch
                        {
                            _ when counter == 16 => 10,
                            _ when counter == 17 => 15,
                            _ when counter == 18 => 20,
                            _ when counter == 19 => 25,
                            _ => 0
                        };

                        if (winAmount > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Counter: {counter}");
                            Console.ResetColor();
                            Console.WriteLine($"Enter 'q' to quit and win {winAmount} Credits or enter any other key to continue playing");
                            key = Console.ReadKey(true);
                            string? winrar = Convert.ToString(key.Key);
                            if (winrar == "Q")
                            {
                                credits += winAmount;
                                break;
                            }
                        }

                        Console.WriteLine("Enter L or R\n1-8  4-7");
                        key = Console.ReadKey(true);
                        string? addCounter = Convert.ToString(key.Key);

                        if (addCounter == "L" || addCounter == "R")
                        {
                            List<int> listL = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
                            List<int> listR = new List<int> { 4, 5, 6, 7 };
                            int L = listL[new Random().Next(listL.Count)];
                            int R = listR[new Random().Next(listR.Count)];

                            counter += addCounter switch
                            {
                                "L" => L,
                                "R" => R,
                                _ => 0
                            };
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (counter >= 20 && counter < 21)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Counter: {counter}");
                        Console.WriteLine($"You're a winner!\n+50 Credits");
                        Console.ResetColor();
                        credits += 50;
                        Thread.Sleep(4000);
                    }
                    else if (counter > 20)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Counter: {counter}");
                        Console.ResetColor();
                        Console.WriteLine("You lose!");
                        Thread.Sleep(2000);
                    }
                    break;

                case "N":
                    playMagnetar = false;
                    break;

                default:
                    continue;
            }
        }

        Console.Clear();
        Console.WriteLine("Game over!");
        Thread.Sleep(3000);
    }
}