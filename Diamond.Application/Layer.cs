namespace Diamond.Application;

public class Layer
{
    public string PaddingPart;
    public string SolidPart;

    public Layer(string paddingPart, string solidPart)
    {
        PaddingPart = paddingPart;
        SolidPart = solidPart;
    }

    public override string ToString()
    {
        return $"{PaddingPart}{SolidPart}{PaddingPart}";
    }
}