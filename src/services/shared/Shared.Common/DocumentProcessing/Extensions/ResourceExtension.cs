using System.Reflection;

namespace ECommerce.Shared.Common.DocumentProcessing;

public static class ResourceExtension
{
    internal static string LookupResource(Type resourceManagerProvider, string? resourceKey)
    {
        foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
            {
                System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null)!;
                if (resourceKey != null)
                {
                    return resourceManager.GetString(resourceKey) ?? string.Empty;
                }
            }
        }

        return string.Empty; // Fallback with the key name
    }
}

