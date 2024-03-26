public class Bundle
{
    public readonly string BundleCode;
    public string BundleDescription;
    public double Price;
    public Bundle(string bundleCode, string bundleDescription, int price)
    {
        BundleCode = bundleCode;
        BundleDescription = bundleDescription;
        Price = price;
    }

    public void EditDescription(string newDescription) => BundleDescription = newDescription;
}