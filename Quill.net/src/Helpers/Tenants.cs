using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quill.Tenants
{
    public record Tenant
    {
        [JsonPropertyName("tenantField")]
        public string TenantField { get; init; }
        [JsonPropertyName("tenantIds")]
        public List<object> TenantIds { get; init; }

        public Tenant(string tenantField, List<object> tenantIds)
        {
            TenantField = tenantField;
            TenantIds = tenantIds;
        }
    }

    public record TenantFlags
    {
        [JsonPropertyName("tenantField")]
        public string TenantField { get; init; }
        [JsonPropertyName("flags")]
        public List<object> Flags { get; init; }

        public TenantFlags(string tenantField, List<object> flags)
        {
            TenantField = tenantField;
            Flags = flags;
        }
    }
    public class TenantUtils
    {

        public const string SingleTenant = "QUILL_SINGLE_TENANT";
        public const string AllTenants = "QUILL_ALL_TENANTS";
        public static ICollection<object> ExtractTenantIds(List<object> tenants)
        {
            if (tenants.Count > 0)
            {
                var firstItem = tenants[0];

                if (firstItem is string || firstItem is int)
                {
                    return tenants;
                }
                else if (firstItem is Tenant tenantObject)
                {
                    return tenantObject.TenantIds;
                }
                else
                {
                    throw new ArgumentException("Invalid format for tenants");
                }
            }
            return new List<object>();
        }


        public static string ExtractTenantField(IEnumerable<object> tenants, string dashboardOwner)
        {

            if (tenants is List<object> tenantList)
            {
                if (tenantList.Count > 0 && (tenantList[0] is string || tenantList[0] is int))
                {
                    return dashboardOwner;
                }
                else if (tenantList[0] is Tenant tenantObject)
                {
                    return tenantObject.TenantField;
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
        }
    }
}