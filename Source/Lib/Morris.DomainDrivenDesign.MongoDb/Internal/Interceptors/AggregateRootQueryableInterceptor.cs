using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Morris.DomainDrivenDesign.MongoDb.Internal.Interceptors;

internal class AggregateRootQueryableInterceptor<TDbContext, TKey, TEntity> : IQueryable<TEntity>, IOrderedQueryable<TEntity>
	where TDbContext: DbContext<TDbContext, TKey>
	where TKey: IEquatable<TKey>
{
	private readonly IQueryable<TEntity> Source;
	private readonly Func<object, object> InterceptValue;
	private readonly Lazy<IQueryProvider> QueryProvider;

	public AggregateRootQueryableInterceptor(
		IQueryable<TEntity> source,
		TDbContext dbContext,
		string collectionName,
		Func<object, object> interceptValue)
	{
		Source = source;
		InterceptValue = interceptValue;
		QueryProvider = new Lazy<IQueryProvider>(() =>
			new AggregateRootQueryProviderInterceptor<TDbContext, TKey, TEntity>(
				source.Provider,
				dbContext,
				collectionName,
				interceptValue));
	}

	public Type ElementType => Source.ElementType;

	public Expression Expression => Source.Expression;

	public IQueryProvider Provider => QueryProvider.Value;

	public IEnumerator<TEntity> GetEnumerator() =>
		new AggregateRootEnumeratorInterceptor<TEntity>(Source.GetEnumerator(), InterceptValue);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
