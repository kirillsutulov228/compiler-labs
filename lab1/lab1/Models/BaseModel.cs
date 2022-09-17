using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;
 
namespace lab1.Models
{
    public class BaseModel
    {
        private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            //Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public BaseModel() { }

        public virtual string ToJSON<T>(JsonSerializerOptions? options = null) where T : BaseModel
        {
            return JsonSerializer.Serialize((T) this, options ?? _serializerOptions);
        }
        public virtual string ToFlatJSON<T>() where T : BaseModel
        {
            return JsonSerializer.Serialize((T) this, new JsonSerializerOptions() {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
        public virtual T Clone<T>() where T : BaseModel
        {
            string json = this.ToJSON<T>();
            T? result = JsonSerializer.Deserialize<T>(json, _serializerOptions);

            if (result != null)
            {
                return result;
            }

            throw new SerializationException("Failed to serialize object");
        }
    }
}
