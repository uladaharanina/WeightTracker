using TrackerService.Interfaces;
using Moq;
using Xunit;
using TrackerService.Models;
using System.Threading.Tasks;


namespace TrackerService.Tests;

public class UnitTest1
{
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
}
