public class Bundle
{
    public readonly string BundleCode;
    public string BundleDescription;
    public Bundle(string bundleCode, string bundleDescription)
    {
        BundleCode = bundleCode;
        BundleDescription = bundleDescription;
    }

    public void EditDescription(string newDescription) => BundleDescription = newDescription;
}