public class Prices : ObjectHasID 
{
    public string ID { get; set; }
    public int Red;
    public int Yellow;
    public int Blue;

    public Prices(string id, int red, int yellow, int blue)
    {
        ID = id;
        Red = red;
        Yellow = yellow;
        Blue = blue;
    }
}