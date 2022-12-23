using System;
using System.Linq;
using System.Linq.Expressions;

namespace Morris.DomainDrivenDesign.MongoDb.Internal.Interceptors;

internal class AggregateRootQueryProviderInterceptor<TDbContext, TKey, TEntity> : IQueryProvider
	where TDbContext: DbContext<TDbContext, TKey>
	where TKey: IEquatable<TKey>
{
	private readonly IQueryProvider Source;
	private readonly TDbContext DbContext;
	private readonly string CollectionName;
	private readonly Func<object, object> InterceptValue;

	public AggregateRootQueryProviderInterceptor(
		IQueryProvider source,
		TDbContext dbContext,
		string collectionName,
		Func<object, object> interceptValue)
	{
		CollectionName = collectionName;
		DbContext = dbContext;
		Source = source;
		InterceptValue = interceptValue;
	}

	public IQueryable CreateQuery(Expression expression) => CreateQuery<TEntity>(expression);

	public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
	{
		IQueryable<TElement> result = Source.CreateQuery<TElement>(expression);

		// Don't intercept if not the source type
		if (!typeof(TEntity).IsAssignableFrom(typeof(TElement)))
			return result;

		return new AggregateRootQueryableInterceptor<TDbContext, TKey, TElement>(
			result,
			DbContext,
			CollectionName,
			InterceptValue);
	}

	public object Execute(Expression expression) => Execute<TEntity>(expression)!;

	public TResult Execute<TResult>(Expression expression)
	{
		TResult result = Source.Execute<TResult>(expression);
		if (result is TEntity)
			result = (TResult)DbContext.Attach(typeof(TEntity), CollectionName, result);
		return result;
	}
}
