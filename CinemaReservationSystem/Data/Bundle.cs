public class Bundle
{
    public readonly string BundleCode;
    public string BundleDescription;
    public double Price;
    // only to be used when adding it to a screening (as there is no db for just bundles)
    public Bundle(string bundleCode, string bundleDescription, int price)
    {
        BundleCode = bundleCode;
        BundleDescription = bundleDescription;
        Price = price;
    }

    // does as it says
    public void EditDescription(string newDescription) => BundleDescription = newDescription;

    // fyi i dont want the code to be editable for admins, that too difficult to handle (or too much work)
}