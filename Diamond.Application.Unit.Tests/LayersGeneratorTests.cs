namespace Diamond.Application.Unit.Tests;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LayersGeneratorTests
{
    private readonly LayersGenerator _sut = new LayersGenerator();

    [TestMethod]
    [DataRow('A',      "A")]      
    [DataRow('B',     "B B")]     
    [DataRow('C',    "C   C")]    
    [DataRow('D',   "D     D")]   
    [DataRow('E',  "E       E")]  
    [DataRow('F', "F         F")] 
    public void GivenAChar_WhenCalled_ShouldReturnTheRightSolidPart(char character, string expectedSolidPart)
    {
        // Arrange

        // Act
        var result = _sut.GenerateLayer(0, character);

        // Assert
        result.SolidPart.Should().Be(expectedSolidPart);
    }

    [TestMethod]
    [DataRow(0, "")]
    [DataRow(1, " ")]
    [DataRow(2, "  ")]
    [DataRow(-2, "  ")]
    [DataRow(21, "                     ")]
    [DataRow(-21, "                     ")]
    
    public void GivenALayer_WhenCalled_ShouldReturnTheRightPaddingPart(int layer, string expectedPaddingPart)
    {
        // Arrange

        // Act
        var result = _sut.GenerateLayer(layer, 'A');

        // Assert
        result.PaddingPart.Should().Be(expectedPaddingPart);
    }

    [TestMethod]
    [DataRow('A', 0, "A")]
    [DataRow('A', 1, " A ")]
    [DataRow('A', 2, "  A  ")]
    [DataRow('B', 0, "B B")]
    [DataRow('B', 1, " B B ")]
    [DataRow('B', 2, "  B B  ")]
    [DataRow('c', 2, "  c   c  ")]
    public void GivenCharacterAndALayer_WhenCalled_ShouldReturnTheRightLayer(char character, int layer, string expectedLayer)
    {
        // Arrange

        // Act
        var result = _sut.GenerateLayer(layer, character);

        // Assert
        result.ToString().Should().Be(expectedLayer);
    }

}