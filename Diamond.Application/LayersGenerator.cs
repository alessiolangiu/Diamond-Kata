namespace Diamond.Application;

public class LayersGenerator : ILayersGenerator
{
    public Layer GenerateLayer(int layer, char character)
    {
        string padding = GeneratePadding(layer);
        string solidPart = GenerateSolidPart(character);
        return new Layer(padding, solidPart);
    }

    private string GenerateSolidPart(char character)
    {
        if (character.ToString().ToUpper()[0] == 'A')
        {
            return character.ToString();
        }
        int characterPosition = character.ToString().ToUpper()[0] - 'A';
        string corePadding = GenerateCorePadding(characterPosition);
        return $"{character}{corePadding}{character}";
    }

    private string GenerateCorePadding(int characterPosition)
    {
        return new string(' ', (characterPosition-1) * 2 + 1);
    }

    private string GeneratePadding(int layer)
    {
        return new string(' ', Math.Abs(layer));
    }
}