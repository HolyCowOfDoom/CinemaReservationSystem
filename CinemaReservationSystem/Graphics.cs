public class Graphics
{
    public static void BoxText(string text, string header = "")
    {
        string upperHeader = header.ToUpper();
        string upperText = text.ToUpper();

        string[] lines = upperText.Split('\n');

        int maxLineLength = lines.Max(line => line.Length);
        int totalWidth = Math.Max(maxLineLength, upperHeader.Length) + 4;
        int leftPadding = (totalWidth - maxLineLength) / 2;
        int headerLeftPadding = (totalWidth - upperHeader.Length) / 2;

        string topBorder = !string.IsNullOrEmpty(upperHeader) ? "╔" + new string('═', totalWidth) + "╗" : "";

        string middleBorder = "";
        if (!string.IsNullOrEmpty(upperHeader))
        {
            middleBorder = $"╔{new string('═', totalWidth)}╗\n║ {new string(' ', headerLeftPadding)}{upperHeader}{new string(' ', totalWidth - upperHeader.Length - headerLeftPadding - 2)} ║\n╠{new string('═', totalWidth)}╣";
        }
        else
        {
            middleBorder = "╠" + new string('═', totalWidth) + "╣";
        }

        string bottomBorder = "╚" + new string('═', totalWidth) + "╝";

        if (!string.IsNullOrEmpty(upperHeader))
        {
            WriteInCenter(middleBorder);
        }
        else
        {
            WriteInCenter("╔" + new string('═', totalWidth) + "╗");
        }

        foreach (string line in lines)
        {
            leftPadding = (totalWidth - line.Length - 2) / 2;
            WriteInCenter($"║ {new string(' ', leftPadding)}{line}{new string(' ', totalWidth - line.Length - leftPadding - 2)} ║");
        }

        WriteInCenter(bottomBorder);
    }

        public void Map()
    {
        Console.Clear();
        string auditorium = $@"
        ";
        int indexPos = 0;
        switch (ID)
        {
            case World.LOCATION_ID_HOME:
                indexPos = map.IndexOf("    |  ** ≈");
                break;

            case World.LOCATION_ID_TOWN_SQUARE:
                indexPos = map.IndexOf("  │   │█");
                break;

            case World.LOCATION_ID_ALCHEMIST_HUT:
                indexPos = map.IndexOf("│ ▄███▄ ");
                break;

            case World.LOCATION_ID_ALCHEMISTS_GARDEN:
                indexPos = map.IndexOf("   ...  ");
                break;

            case World.LOCATION_ID_BURROW:
                indexPos = map.IndexOf("*** +  ");
                break;

            case World.LOCATION_ID_FARMHOUSE:
                indexPos = map.IndexOf("===F___");
                break;

            case World.LOCATION_ID_FARM_FIELD:
                indexPos = map.IndexOf("  =====       │");
                break;

            case World.LOCATION_ID_SHACK:
                indexPos = map.IndexOf("     ▐      └");
                break;

            case World.LOCATION_ID_TOMB2:
                indexPos = map.IndexOf("   <=>        │");
                break;

            case World.LOCATION_ID_FIELD_SOUTH:
                indexPos = map.IndexOf("      ▄▌ *");
                break;

            case World.LOCATION_ID_GUARD_POST:
                indexPos = map.IndexOf(" │---G╠");
                break;

            case World.LOCATION_ID_TOMB:
                indexPos = map.IndexOf("   <=>≈");
                break;

            case World.LOCATION_ID_BRIDGE:
                indexPos = map.IndexOf("--G╠═B═");
                break;

            case World.LOCATION_ID_SPIDER_FIELD:
                indexPos = map.IndexOf(" **S***");
                break;
        }
        var newmap = ReplaceAt(map, indexPos + 4, Convert.ToChar("X"));

        WriteLineWithColoredLetter(newmap);
    }

    public static void WriteLineWithColoredLetter(string letters)
    {
        Char[] array = letters.ToCharArray();
        char[] yellow = {'S', 'B', 'G', 'P', 'A', 'V', 'F', 'T', 'H', '1', '2', '3', '4', '5' };

        foreach (Char c in array)
        {
            if (c == 'X')
            {
                Console.ForegroundColor = System.ConsoleColor.DarkRed;
                Console.Write(c);
            }
            else if (yellow.Contains(c))
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write(c);
            }
            else
            {
                Console.ForegroundColor = System.ConsoleColor.White;
                Console.Write(c);
            }
        }
        Console.WriteLine();
    }

    public static string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}