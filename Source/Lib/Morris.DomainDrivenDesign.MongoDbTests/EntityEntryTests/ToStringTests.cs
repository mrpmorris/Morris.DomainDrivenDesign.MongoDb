using Morris.DomainDrivenDesign.MongoDb;
using Morris.DomainDrivenDesign.MongoDbTests.TestDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityEntryTests;

public class ToStringTests
{
	[Fact]
	public void WhenExecuted_ThenShouldReturnAHumanReadibleString()
	{
		var entity = new SimpleEntity();
		var entry = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);
		string toStringValue = entry.ToString();
		string expectedId = entity.Id.ToString();

		Assert.Equal(
			$"Morris.DomainDrivenDesign.MongoDbTests.TestDomain.SimpleEntity:{expectedId}=Created",
			entry.ToString());
	}
}
