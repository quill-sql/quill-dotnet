using System.Text.Json;

namespace Quill.Tenants
{
    public class TenantUtils
    {

        public const string SingleTenant = "QUILL_SINGLE_TENANT";
        public const string AllTenants = "QUILL_ALL_TENANTS";
        public static List<object> ExtractTenantIds(IEnumerable<object> tenants)
        {
            List<object> tenantIds = new List<object>() ?? new List<object>();

            if (tenants is List<object> tenantList)
            {
                if (tenantList.Count > 0)
                {
                    var firstItem = tenantList[0];

                    if (firstItem is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            var tenantObject = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonElement.GetRawText());
                            if (tenantObject != null && tenantObject.ContainsKey("tenantIds"))
                            {
                                var tenantIdsString = tenantObject["tenantIds"]?.ToString();
                                var deserializedTenantIds = tenantIdsString != null ? JsonSerializer.Deserialize<List<object>>(tenantIdsString) : null;
                                if (deserializedTenantIds != null)
                                {
                                    tenantIds = deserializedTenantIds;
                                }
                            }
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.String || jsonElement.ValueKind == JsonValueKind.Number)
                        {
                            tenantIds = tenantList.Select(t => JsonSerializer.Deserialize<object>(((JsonElement)t).GetRawText()) ?? new object()).ToList();
                        }
                        else
                        {
                            throw new ArgumentException("Invalid format for tenants");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Unsupported item type in tenants");
                    }
                }
                else
                {
                    throw new ArgumentException("Tenant list is empty");
                }
            }
            else
            {
                throw new ArgumentException("Invalid format for tenants");
            }

            return tenantIds ?? new List<object>();
        }


        public static string ExtractTenantField(IEnumerable<object> tenants, string dashboardOwner)
        {
            string tenantField = string.Empty;

            if (tenants is List<object> tenantList)
            {
                if (tenantList.Count > 0 && (tenantList[0] is string || tenantList[0] is int))
                {
                    return dashboardOwner;
                }
                else if (tenantList[0] is Dictionary<string, object> tenantObject && tenantObject.ContainsKey("tenantField"))
                {
                    tenantField = tenantObject["tenantField"]?.ToString() ?? string.Empty;
                }
                else
                {
                    throw new ArgumentException("Invalid format for tenants");
                }
            }
            else
            {
                throw new ArgumentException("Invalid format for tenants");
            }

            return tenantField ?? string.Empty;
        }
    }
}