public class Graphics
{
    public static void Box(string text, string header='')
    {
        if (header == '')
        {
            WriteInCenter(text);
        }
        else
        {
            WriteInCenter("╔═══════════════════════════════════╗");
            WriteInCenter($"║              {header}            ║");
            WriteInCenter("╠═══════════════════════════════════╣");
            WriteInCenter(text);
        }
    }

    public static void WriteInCenter(string data)
    {
        foreach (var model in data.Split('\n'))
        {
            Console.SetCursorPosition((Console.WindowWidth - model.Length) / 2, Console.CursorTop);
            Console.WriteLine($"║{model}║");
        }
    }
}