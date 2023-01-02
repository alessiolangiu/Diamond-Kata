
namespace Diamond.Application.Unit.Tests;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class DiamondTests
{
    private const string NoInputError = "No input provided. Please pass a character as parameter.";
    private const string InvalidInputError = "Please pass one character at a time.";
    private const string CharacterNotValidError = "Character not valid. Please pass only a letter as parameter.";

    private readonly Fixture _fixture = new();
    private readonly Diamond _sut;

    private readonly Mock<ILayersGenerator> _layersGeneratorMock = new();

    public DiamondTests()
    {
        _sut = new Diamond(_layersGeneratorMock.Object);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    public void GivenNoInput_ShouldReturnError(string? input)
    {
        // Arrange

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsFailed.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Message.Should().Be(NoInputError);
            _layersGeneratorMock.VerifyAll();
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetNotAllowedCharacter), DynamicDataSourceType.Method)]
    public void GivenNotAllowedCharacter_ShouldReturnError(string? input)
    {
        // Arrange

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsFailed.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Message.Should().Be(CharacterNotValidError);
            _layersGeneratorMock.VerifyAll();
        }
    }

    [TestMethod]
    public void GivenLongStringAsInput_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<string>();

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsFailed.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().Message.Should().Be(InvalidInputError);
            _layersGeneratorMock.VerifyAll();
        }
    }

    [TestMethod]
    public void GivenCharacterA_ShouldReturnOneLine()
    {
        // Arrange
        var input = "a";
        Layer layer = new Layer("", "a");
        _layersGeneratorMock.Setup(m => m.GenerateLayer(0, 'a')).Returns(layer);

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Length.Should().Be(1);
            result.Value.First().Should().Be("a");
            _layersGeneratorMock.VerifyAll();
        }
    }

    [TestMethod]
    public void GivenCharacterB_ShouldReturnThreeLine()
    {
        // Arrange
        var input = "b";
        var layer1 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(1, 'a')).Returns(layer1);
        var layer0 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(0, 'b')).Returns(layer0);

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Length.Should().Be(3);
            result.Value[0].Should().Be(layer1.ToString());
            result.Value[1].Should().Be(layer0.ToString());
            result.Value[2].Should().Be(layer1.ToString());
            _layersGeneratorMock.VerifyAll();
        }
    }

    [TestMethod]
    public void GivenUpperCaseCharacterD_ShouldReturnSixLine()
    {
        // Arrange
        var input = "D";
        var layer3 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(3, 'A')).Returns(layer3);
        var layer2 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(2, 'B')).Returns(layer2);
        var layer1 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(1, 'C')).Returns(layer1);
        var layer0 = _fixture.Create<Layer>();
        _layersGeneratorMock.Setup(m => m.GenerateLayer(0, 'D')).Returns(layer0);

        // Act
        var result = _sut.Create(input);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Length.Should().Be(7);
            result.Value[0].Should().Be(layer3.ToString());
            result.Value[1].Should().Be(layer2.ToString());
            result.Value[2].Should().Be(layer1.ToString());
            result.Value[3].Should().Be(layer0.ToString());
            result.Value[4].Should().Be(layer1.ToString());
            result.Value[5].Should().Be(layer2.ToString());
            result.Value[6].Should().Be(layer3.ToString());
            _layersGeneratorMock.VerifyAll();
        }
    }
    private static IEnumerable<object?[]> GetNotAllowedCharacter()
    {
        for (int i = 0; i < 256; i++)
        {
            char character = (char)i;
            if (IsValidLetter(character))
            {
                continue;
            }

            yield return new object?[] { new string(((char)i).ToString()) };
        }
    }

    private static bool IsValidLetter(char character)
    {
        return character is >= 'A' and <= 'Z' || character is >= 'a' and <= 'z';
    }
}