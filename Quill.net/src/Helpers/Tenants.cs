using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quill.Tenants
{
    public record Tenant<T>
    {
        [JsonPropertyName("tenantField")]
        public string TenantField { get; init; }
        [JsonPropertyName("tenantIds")]
        public List<T> TenantIds { get; init; }

        public Tenant(string tenantField, List<T> tenantIds)
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
        public List<string> Flags { get; init; }

        public TenantFlags(string tenantField, List<string> flags)
        {
            TenantField = tenantField;
            Flags = flags;
        }
    }
    public class TenantUtils
    {

        public const string SingleTenant = "QUILL_SINGLE_TENANT";
        public const string AllTenants = "QUILL_ALL_TENANTS";
        public static ICollection<T> ExtractTenantIds<T>(List<dynamic> tenants)
        {
            if (tenants.Count > 0)
            {
                var firstItem = tenants[0];

                if (firstItem is T) // Check if the first item is directly of type TItem
                {
                    return tenants.Cast<T>().ToList(); // Cast the entire list to TItem
                }
                else if (firstItem is Tenant<T> tenantObject) // Check if the first item is a Tenant<TItem>
                {
                    return tenantObject.TenantIds; // Return the TenantIds from the first tenant
                }
                else
                {
                    throw new ArgumentException("Invalid format for tenants");
                }
            }

            return new List<T>(); // Return an empty list if there are no tenants
        }


        public static string ExtractTenantField<T>(IEnumerable<dynamic> tenants, string dashboardOwner)
        {
            if (tenants is List<T> tenantList)
            {
                if (tenantList.Count > 0)
                {
                    var firstItem = tenantList[0];

                    if (firstItem is Tenant<T>) // If the first item is of type TItem (string or int)
                    {
                        return dashboardOwner;
                    }
                    else if (firstItem is Tenant<T> tenantObject) // If the first item is a Tenant<TItem>
                    {
                        return tenantObject.TenantField;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid format for tenants");
                    }
                }
            }

            throw new ArgumentException("Invalid format for tenants");
        }
    }
}