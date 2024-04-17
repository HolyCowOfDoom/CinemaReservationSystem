public class Graphics
{
    public static void BoxText(string text, string header = "")
    {

        string upperHeader = header.ToUpper();
        string upperText = text.ToUpper();

        if (string.IsNullOrEmpty(upperText)) return;
                
        string[] lines = upperText.Split('\n');

        int maxLineLength = lines.Max(line => line.Length);
        int totalWidth = Math.Max(maxLineLength, upperHeader.Length) + 4;
        int RightPadding = (totalWidth - maxLineLength) / 2;
        int headerLeftPadding = (totalWidth - upperHeader.Length) / 2 - 1;

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
            Helper.WriteInCenter(middleBorder);
        }
        else
        {
            Helper.WriteInCenter("╔" + new string('═', totalWidth) + "╗");
        }

        foreach (string line in lines)
        {
            RightPadding = (totalWidth - line.Length - 2);
            Helper.WriteInCenter($"║ {line}{new string(' ', totalWidth - line.Length - RightPadding - 2)}{new string(' ', RightPadding)} ║");
        }

        Helper.WriteInCenter(bottomBorder);
    }

    public void AuditoriumView(string Auditorium)
    {
        Console.Clear();
        int indexPos = 0;
        var newmap = Helper.ReplaceAt(Auditorium, indexPos + 4, Convert.ToChar("X"));

        Helper.WriteColoredLetter(newmap);
    }

    public static void AudiVisual()
    {
        string Auditorium1 = @"
  U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U
  U U U U U U U U
";

        string Auditorium2 = @"
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U  U U U U U U  U U U U
  U U U U  U U U U U U  U U U U
  U U U U  U U U U U U  U U U U
  U U U  U U U U U U  U U U
  U U U  U U U U U U  U U U
";

    string Auditorium3 = @"
  U U U U U U U  U U U U U U U U  U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U

  U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U

  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U  U U U U U U U U  U U U U U U
  U U U U  U U U U U U U U  U U U U
  U U U  U U U U U U U U  U U U
";
    }
}