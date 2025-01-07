namespace Quill.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public enum FieldFormat
    {
        whole_number,
        one_decimal_place,
        two_decimal_places,
        dollar_amount,
        MMM_yyyy,
        MMM_dd_yyyy,
        MMM_dd_MMM_dd,
        MMM_dd_hh_mm_ap_pm,
        hh_ap_pm,
        percent
    }

    public class DateField
    {
        [JsonPropertyName("table")]
        public string? Table { get; set; }

        [JsonPropertyName("field")]
        public string? Field { get; set; }
    }

    public class FormattedColumn
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("format")]
        public FieldFormat? Format { get; set; }
    }

    public class Tenant
    {
        [JsonPropertyName("tenantField")]
        public string? TenantField { get; set; }

        [JsonPropertyName("tenantIds")]
        public List<string>? TenantIds { get; set; }
    }

    public class QueryFilter
    {
        [JsonPropertyName("filterType")]
        public FilterType? FilterType { get; set; }

        [JsonPropertyName("operator")]
        public object? Operator { get; set; }

        [JsonPropertyName("value")]
        public object? Value { get; set; }

        [JsonPropertyName("field")]
        public string? Field { get; set; }

        [JsonPropertyName("table")]
        public string? Table { get; set; }
    }

    public class QueryParams
    {
        [JsonPropertyName("tenants")]
        public IEnumerable<object>? Tenants { get; set; }

        [JsonPropertyName("flags")]
        public IEnumerable<object>? Flags { get; set; }

        [JsonPropertyName("metadata")]
        public IDictionary<string, object>? Metadata { get; set; }

        [JsonPropertyName("filters")]
        public List<QueryFilter>? Filters { get; set; }
    }

    public class AdditionalProcessing
    {
        [JsonPropertyName("getSchema")]
        public bool? GetSchema { get; set; }

        [JsonPropertyName("getColumns")]
        public bool? GetColumns { get; set; }

        [JsonPropertyName("getColumnsForSchema")]
        public bool? GetColumnsForSchema { get; set; }

        [JsonPropertyName("getTables")]
        public bool? GetTables { get; set; }

        [JsonPropertyName("schema")]
        public string? Schema { get; set; }

        [JsonPropertyName("schemaNames")]
        public List<string>? SchemaNames { get; set; }

        [JsonPropertyName("table")]
        public string? Table { get; set; }

        [JsonPropertyName("fieldsToRemove")]
        public List<string>? FieldsToRemove { get; set; }

        [JsonPropertyName("arrayToMap")]
        public ArrayToMap? ArrayToMap { get; set; }

        [JsonPropertyName("overridePost")]
        public bool? OverridePost { get; set; }

        [JsonPropertyName("convertDatatypes")]
        public bool? ConvertDatatypes { get; set; }

        [JsonPropertyName("limitThousand")]
        public bool? LimitThousand { get; set; }

        [JsonPropertyName("limitBy")]
        public int? LimitBy { get; set; }
    }

    public class ArrayToMap
    {
        [JsonPropertyName("arrayName")]
        public string? ArrayName { get; set; }

        [JsonPropertyName("field")]
        public string? Field { get; set; }
    }
}
