using Dapper;
using MySql.Data.MySqlClient;

namespace Gradebook.Foundation.Common.Extensions;

public static class MyMySqlConnectionExtensions
{
    public static async Task<IPagedList<T>> QueryPagedAsync<T>(this MySqlConnection cn, string orderedQuery, object queryData, IPager pager)
    {
        var countQuery = $@"
        SELECT COUNT(*) 
        FROM ({orderedQuery}) AS _;
        ";

        var pagedQuery = pager.Page <= 0 ?
        $@"
            SELECT *
            FROM ({orderedQuery}) AS _
        " :
        $@"
            SELECT *
            FROM ({orderedQuery}) AS _
            LIMIT {pager.PageSize}
            OFFSET {pager.PageSize * (pager.Page - 1)};
        ";

        using var query = await cn.QueryMultipleAsync(countQuery + pagedQuery, queryData);
        var total = await query.ReadFirstOrDefaultAsync<int>();
        var resp = await query.ReadAsync<T>();
        return resp.ToPagedList(pager.Page, total, pager.Page <= 0 ? -1 : (int)Math.Ceiling(total / (decimal)pager.PageSize));
    }
}
