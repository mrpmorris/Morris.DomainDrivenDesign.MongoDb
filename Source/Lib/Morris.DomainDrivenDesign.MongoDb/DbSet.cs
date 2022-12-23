using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morris.DomainDrivenDesign.MongoDb;

internal interface IDbSet<TKey>
	where TKey: IEquatable<TKey>
{
	Task SaveCollectionChangesAsync(IEnumerable<EntityEntry<TKey>> entityEntries);
}