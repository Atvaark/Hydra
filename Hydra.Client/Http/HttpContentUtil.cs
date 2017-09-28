using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ComponentAce.Compression.Libs.zlib;
using Newtonsoft.Json;

namespace Hydra.Client.Http
{
    internal static class HttpContentUtil
    {
        public static StringContent CreateJsonContent(object obj)
        {
            var objJson = JsonConvert.SerializeObject(obj);
            var content = new StringContent(objJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // no encoding
            return content;
        }

        public static async Task<T> ReadJsonContent<T>(HttpContent contnet)
        {
            var responseContent = await contnet.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public static ByteArrayContent CreateCompressedJsonContent(object obj)
        {
            var objJson = JsonConvert.SerializeObject(obj);
            var objBytes = Encoding.UTF8.GetBytes(objJson);

            var outputStream = new MemoryStream();
            using (var zstream = new ZOutputStream(outputStream, zlibConst.Z_BEST_COMPRESSION))
            {
                zstream.Write(objBytes, 0, objBytes.Length);
                zstream.Flush();
            }
            var compressedBytes = outputStream.ToArray();

            var content = new ByteArrayContent(compressedBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        public static async Task<T> ReadCompressedJsonContent<T>(HttpContent content)
        {
            var outStream = new MemoryStream();
            using (var stream = await content.ReadAsStreamAsync())
            using (var input = new ZInputStream(stream))
            {
                int data;
                var stopByte = -1;
                while (stopByte != (data = input.Read()))
                {
                    var dataByte = (byte)data;
                    outStream.WriteByte(dataByte);
                }
            }
            byte[] outputBytes = outStream.ToArray();
            string outputJsonString = Encoding.UTF8.GetString(outputBytes);
            T obj = JsonConvert.DeserializeObject<T>(outputJsonString);
            return obj;
        }
    }
}