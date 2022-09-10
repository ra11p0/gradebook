using Dapper;
using MySql.Data.MySqlClient;

namespace Gradebook.Foundation.Common.Extensions;

public static class MyMySqlConnectionExtensions
{
    public static async Task<IPagedList<T>> QueryPagedAsync<T>(this MySqlConnection cn, string orderedQuery, object queryData, IPager pager){
        var countQuery = $@"
        SELECT COUNT(*) 
        FROM ({orderedQuery})
        ";
        var pagedQuery = $@"
        SELECT *
        FROM ({orderedQuery})
        LIMIT {pager.PageSize}
        OFFSET {pager.PageSize * pager.Page}
        ";
        var total = await cn.QueryFirstOrDefaultAsync<int>(countQuery, queryData);
        var resp = await cn.QueryAsync<T>(pagedQuery, queryData);
        return resp.ToPagedList(pager.Page, total, ((int)Math.Ceiling(((decimal)total/ (decimal)pager.PageSize))));
    }
}
