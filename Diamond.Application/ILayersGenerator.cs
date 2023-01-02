namespace Diamond.Application
{
    public interface ILayersGenerator
    {
        Layer GenerateLayer(int layer, char solid);
    }
}
