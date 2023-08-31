using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.JsonEntities
{
    public class YaGeocode
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }

    public class GeoObjectCollection
    {        
        public FeatureMember[] FeatureMember { get; set; }
    }       

    public class FeatureMember
    {
        public GeoObject GeoObject { get; set; }
    }

    public class GeoObject
    {
        public MetaDataProperty MetaDataProperty { get; set; }               
        public Point Point { get; set; }
    }

    public class MetaDataProperty
    {
        public GeocoderMetaData GeocoderMetaData { get; set; }
    }

    public class GeocoderMetaData
    {       
        public Address Address { get; set; }        
    }

    public class Address
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }       
        public Component[] Components { get; set; }
    }

    public class Component
    {
        public string Kind { get; set; }
        public string Name { get; set; }
    }

    public class Point
    {
        public string Pos { get; set; }
    }

}
