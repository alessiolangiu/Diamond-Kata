namespace Diamond.Application
{
    using FluentResults;

    public class Diamond
    {
        private const string NoInputError = "No input provided. Please pass a character as parameter.";
        private const string InvalidInputError = "Please pass one character at a time.";
        private const string CharacterNotValidError = "Character not valid. Please pass only a letter as parameter.";

        private readonly ILayersGenerator _layersGenerator;

        public Diamond(ILayersGenerator layersGenerator)
        {
            _layersGenerator = layersGenerator;
        }

        public Result<string[]> Create(string? input)
        {
            var validation = ValidateInput(input);
            if (validation.IsFailed)
            {
                return validation;
            }

            var character = input![0];
            if (character > 'Z')
            {
                return GetLowerCaseDiamond(character);
            }

            return GetUpperCaseDiamond(character);

        }

        private Result<string[]> GetLowerCaseDiamond(char character)
        {
            return this.GetDiamond(character, 'a');
        }

        private Result<string[]> GetUpperCaseDiamond(char character)
        {
            return this.GetDiamond(character, 'A');
        }

        private Result<string[]> GetDiamond(char character, char referenceCharacter)
        {
            int topLayer = character - referenceCharacter;
            var numberOfLayers = topLayer * 2 + 1;

            var result = new string[numberOfLayers];
            for (int layer = topLayer; layer >= 0; layer--)
            {
                char characterToRender = (char)(referenceCharacter + topLayer - layer);
                string line = _layersGenerator.GenerateLayer(
                    layer,
                    characterToRender).ToString();
                result[topLayer - layer] = line;
                result[topLayer + layer] = line;
            }

            return Result.Ok(result);
        }

        private Result ValidateInput(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Result.Fail(NoInputError);
            }

            if (input.Length >1)
            {
                return Result.Fail(InvalidInputError);
            }

            char character = input.ToUpper()[0];
            if (character is < 'A' or > 'Z')
            {
                return Result.Fail(CharacterNotValidError);
            }

            return Result.Ok();

        }
    }
}