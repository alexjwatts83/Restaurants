namespace Restaurants.Application.Restaurants.Commands.Tests;

public class UpdateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange
        var command = new UpdateRestaurantCommand()
        {
            Name = "Test"
        };

        var validator = new UpdateRestaurantCommandValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory()]
    [InlineData("ab")]
    [InlineData("xxzzhddttzesmdsyeczrxhjcnjyxmcuhhhweipwdrqlcwhjfffwbabgccmfhuhkwugngnjjhjfajepnpdgbghommtyatkctgauvbg")]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors(string name)
    {
        // arrange
        var command = new UpdateRestaurantCommand()
        {
            Name = name
        };

        var validator = new UpdateRestaurantCommandValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
}