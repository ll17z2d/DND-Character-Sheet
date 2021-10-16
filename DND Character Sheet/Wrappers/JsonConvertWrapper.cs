using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DND_Character_Sheet.Annotations;
using Newtonsoft.Json;

namespace DND_Character_Sheet.Wrappers
{
    public interface IJsonConvertWrapper
    {
        public string SerializeObject([CanBeNull] object objectToSerialize);

        public T DeserializeObject<T>(string objectToDeserialize);
    }

    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public string SerializeObject(object objectToSerialize) 
            => JsonConvert.SerializeObject(objectToSerialize);

        public T DeserializeObject<T>(string objectToDeserialize) 
            => JsonConvert.DeserializeObject<T>(objectToDeserialize);
    }
}
