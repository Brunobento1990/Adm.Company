namespace Adm.Company.Infrastructure.Extensions;

public static class PaginacaoExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> querable, int skip, int take)
    {
        return querable
            .Skip((skip - 1) * take)
            .Take(take);
    }
}
