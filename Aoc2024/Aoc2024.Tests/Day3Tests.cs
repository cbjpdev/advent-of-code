namespace Aoc2024.Tests;

public class Day3Tests
{

    [Fact]
    public void Day3_Test1()
    {
        // arrange
        string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        // act
        var result = Day3.ProcessString(input);

        // assert
        result.Should().Be(161);
    }

    [Fact]
    public void Day3_Test2()
    {
        string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        
        var result = Day3.ProcessDoDont(input);
        
        result.Should().Be(48);
    }
}
