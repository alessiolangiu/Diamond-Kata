using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Diamond.Integration.Tests;

using FluentAssertions;
using FluentAssertions.Execution;

[TestClass]
public class DiamondTests
{
    [TestMethod]
    public void GivenNoInput_ShouldPrintExpectedError()
    {
        // Arrange

        // Act
        var result = TestUtil.RunDiamondWithoutArguments();

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(1);
            result[0].Should().Be("No input provided. Please pass a letter as parameter.");
        }
    }

    [TestMethod]
    public void GivenWrongCharacter_ShouldPrintExpectedError()
    {
        // Arrange

        // Act
        var result = TestUtil.RunDiamondWithArguments("=");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(1);
            result[0].Should().Be("Character not valid. Please pass only a letter as parameter.");
        }
    }

    [TestMethod]
    public void GivenLowerCaseA_AndAdditionalCharacters_ShouldPrintExpectedOneLineDiamondWithWarning()
    {
        // Arrange

        // Act
        var result = TestUtil.RunDiamondWithArguments("a-");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(1);
            result[0].Should().Be("Please pass one character at a time.");
        }
    }

    [TestMethod]
    public void GivenLowerCaseA_ShouldPrintExpectedOneLineDiamond()
    {
        // Arrange

        // Act
        var result = TestUtil.RunDiamondWithArguments("a");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(1);
            result[0].Should().Be("a");
        }
    }

    [TestMethod]
    public void GivenUpperCaseC_ShouldPrintExpectedDiamond()
    {
        // Arrange
        string[] expectedDiamond = new string[]
        {
            "  A  ", 
            " B B ", 
            "C   C", 
            " B B ", 
            "  A  "
        };

        // Act
        var result = TestUtil.RunDiamondWithArguments("C");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(5);
            result.Should().BeEquivalentTo(expectedDiamond);
        }
    }

    [TestMethod]
    public void GivenLowerCaseD_ShouldPrintExpectedDiamond()
    {
        // Arrange
        string[] expectedDiamond = new[]
        {
            "   a   ",
            "  b b  ",
            " c   c ",
            "d     d",
            " c   c ",
            "  b b  ",
            "   a   "
        };

        // Act
        var result = TestUtil.RunDiamondWithArguments("d");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(7);
            result.Should().BeEquivalentTo(expectedDiamond);
        }
    }

    [TestMethod]
    public void GivenUpperCaseZ_ShouldPrintFullDiamond()
    {
        // Arrange
        string[] expectedDiamond = new[]
        {
            "                         A                         ",
            "                        B B                        ",
            "                       C   C                       ",
            "                      D     D                      ",
            "                     E       E                     ",
            "                    F         F                    ",
            "                   G           G                   ",
            "                  H             H                  ",
            "                 I               I                 ",
            "                J                 J                ",
            "               K                   K               ",
            "              L                     L              ",
            "             M                       M             ",
            "            N                         N            ",
            "           O                           O           ",
            "          P                             P          ",
            "         Q                               Q         ",
            "        R                                 R        ",
            "       S                                   S       ",
            "      T                                     T      ",
            "     U                                       U     ",
            "    V                                         V    ",
            "   W                                           W   ",
            "  X                                             X  ",
            " Y                                               Y ",
            "Z                                                 Z",
            " Y                                               Y ",
            "  X                                             X  ",
            "   W                                           W   ",
            "    V                                         V    ",
            "     U                                       U     ",
            "      T                                     T      ",
            "       S                                   S       ",
            "        R                                 R        ",
            "         Q                               Q         ",
            "          P                             P          ",
            "           O                           O           ",
            "            N                         N            ",
            "             M                       M             ",
            "              L                     L              ",
            "               K                   K               ",
            "                J                 J                ",
            "                 I               I                 ",
            "                  H             H                  ",
            "                   G           G                   ",
            "                    F         F                    ",
            "                     E       E                     ",
            "                      D     D                      ",
            "                       C   C                       ",
            "                        B B                        ",
            "                         A                         "
 
        };

        // Act
        var result = TestUtil.RunDiamondWithArguments("Z");

        // Assert
        using (new AssertionScope())
        {
            result.Length.Should().Be(51);
            result.Should().BeEquivalentTo(expectedDiamond);
        }
    }

}