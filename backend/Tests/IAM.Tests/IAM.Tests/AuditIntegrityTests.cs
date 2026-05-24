using IAM.Shared.Extensions;
using Xunit;

namespace IAM.Tests;

public class AuditIntegrityTests
{
    [Fact]
    public void SHA256_Chaining_ShouldProduceDifferentHashes()
    {
        // Arrange
        var entry1 = new { Data = "Event 1" };
        var entry2 = new { Data = "Event 2" };

        // Act
        var hash1 = entry1.CalculateHash(string.Empty);
        var hash2 = entry2.CalculateHash(hash1);

        // Assert
        Assert.NotEqual(hash1, hash2);
        Assert.NotNull(hash1);
        Assert.NotNull(hash2);
    }

    [Fact]
    public void SHA256_Chaining_ShouldBeDeterministic()
    {
        // Arrange
        var entry = new { Data = "Event" };
        var prevHash = "previous-hash";

        // Act
        var hash1 = entry.CalculateHash(prevHash);
        var hash2 = entry.CalculateHash(prevHash);

        // Assert
        Assert.Equal(hash1, hash2);
    }
}
