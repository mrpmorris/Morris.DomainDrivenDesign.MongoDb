using Morris.DomainDrivenDesign.MongoDb;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityIdAndCollectionNameTests;

public class ToStringTests
{
	[Fact]
	public void WhenExecuted_ThenReturnAsHumanReadibleString()
	{
		var id = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		string toStringValue = id.ToString();
		string expectedGuidString = id.EntityId.ToString();

		Assert.Equal(
			$"X:{expectedGuidString}",
			id.ToString());
	}
}
