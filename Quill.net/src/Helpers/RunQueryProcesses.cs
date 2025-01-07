using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quill.Connection;
using Quill.Database;

namespace Quill.RunQueryProcesses
{
    public class TableSchemaInfo
    {
        public string? FieldType { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public bool? IsVisible { get; set; }
    }

    public class QueryHelper
    {
        public static QueryResult RemoveFields(QueryResult queryResults, List<string> fieldsToRemove)
        {
            var fields = queryResults.Fields.Where(field => !fieldsToRemove.Contains(field.Name)).ToList();
            foreach (var row in queryResults.Rows)
            {
                foreach (var field in fieldsToRemove)
                {
                    row.Remove(field);
                }
            }
            return new QueryResult { Fields = fields, Rows = queryResults.Rows };
        }

        public static async Task<List<List<Dictionary<string, object>>>> MapQueriesAsync(
            List<string> queries,
            CachedConnection targetConnection)
        {
            var mappedArray = new List<List<Dictionary<string, object>>>();
            foreach (var query in queries)
            {
                var queryResult = await targetConnection.QueryAsync(query);

                // Convert Rows to List<Dictionary<string, object>>
                var rows = queryResult.Rows
                    .Select(row => row.ToDictionary(entry => entry.Key, entry => entry.Value))
                    .ToList();

                mappedArray.Add(rows);
            }
            return mappedArray;
        }
    }
}