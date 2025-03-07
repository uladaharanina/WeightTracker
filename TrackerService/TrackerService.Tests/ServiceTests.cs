using TrackerService.Interfaces;
using Moq;
using Xunit;
using TrackerService.Models;
using System.Threading.Tasks;


namespace TrackerService.Tests;

public class ServiceTests
{

    //Tests retrieval of weights entry
    [Fact]
    public async Task TestGetAllWeightEntry()
    {
        //Arrange
        Mock<IWeightRepo> mockRepo = new();

        List<WeightEntry> entries = [
            new WeightEntry { Id = 1, Weight = 56 },
            new WeightEntry { Id = 2, Weight = 55 },
            new WeightEntry { Id = 3, Weight = 58 }
        ];

        mockRepo.Setup(rep => rep.GetWeightEntries())
        .ReturnsAsync(entries);
        IWeightService myService = new WeightService(mockRepo.Object);

        //Act
        List<WeightEntry>? result = await myService.GetWeights();
        //Assert
        Assert.Equal(entries, result);
    }

    //Tests adding a new weight

    [Theory]
    [InlineData(57, "")]
    [InlineData(59, "Some Note")]

    public async Task TestAddWeightSuccesful(double weight, string comment)
    {
        //Arrange
        Mock<IWeightRepo> mockRepo = new();
        WeightEntry newEntry = new WeightEntry { Id = 2, Weight = weight, Comment = comment };

        mockRepo.Setup(rep => rep.AddNewWeightEntry(newEntry)).ReturnsAsync(newEntry);

        IWeightService myService = new WeightService(mockRepo.Object);
        //Act
        WeightEntry result = await myService.AddWeight(newEntry);
        //Assert
        Assert.Equal(newEntry, result);

    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(0, "Some Note")]

    public async Task TestAddWeightWrongInput(double weight, string comment)
    {
        //Arrange
        Mock<IWeightRepo> mockRepo = new();
        WeightEntry newEntry = new WeightEntry { Id = 2, Weight = weight, Comment = comment };

        mockRepo.Setup(rep => rep.AddNewWeightEntry(newEntry)).ReturnsAsync(newEntry);

        IWeightService myService = new WeightService(mockRepo.Object);
        //Act
        Func<Task> result = async () => await myService.AddWeight(newEntry);

        var exception = await Assert.ThrowsAsync<Exception>(result);

        //Assert
        Assert.Equal("Weight is required.", exception.Message);

    }
}
