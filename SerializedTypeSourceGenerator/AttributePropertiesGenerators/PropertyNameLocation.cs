using Microsoft.CodeAnalysis;

namespace SerializedTypeSourceGenerator
{
    internal class PropertyNameLocation
    {
        public PropertyNameLocation(string propertyName, Location location)
        {
            PropertyName = propertyName;
            Location = location;
        }

        public string PropertyName { get; }
        public Location Location { get; }
    }
}
