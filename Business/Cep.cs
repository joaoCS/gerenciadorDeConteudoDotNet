using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Business
{
    public class Cep
    {
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }

        public static Cep Busca(string cep)
        {
            var cepObj = new Cep();

            var url = "http://api.postmon.com.br/v1/cep/" + cep;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string json = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            JsonCepObject cepJson = json_serializer.Deserialize<JsonCepObject>(json);


            cepObj.CEP = cepJson.cep;
            cepObj.Endereco = cepJson.endereco;
            cepObj.Bairro = cepJson.bairro;
            cepObj.Cidade = cepJson.cidade;
            cepObj.Estado = cepJson.estado;

            return cepObj;
        }
    }


    public class JsonCepObject
    {
        public string cep { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string endereco { get; set; }
    }
}

