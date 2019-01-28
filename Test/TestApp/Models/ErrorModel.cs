using Newtonsoft.Json;

namespace TestApp.Models
{
    public class ErrorModel
    {
        public string Type { get; set; }
        public int? Status { get; set; }
        public string Details { get; set; }
        public string Instance { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
