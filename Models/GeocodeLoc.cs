namespace PDBG.CRM.WEB.Models
{
    public class GeocodeLoc
    {
        private string address;
        private string apiKey = "D9TYChY0Ys9_oRnmL4f0P0yEnH8W1nzmc3KT3LZ_Q4U";
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public GeocodeLoc(string address)
        {
            this.address = address.Replace(" ", "+");
        }
    }
}
