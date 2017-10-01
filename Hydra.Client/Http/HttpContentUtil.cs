using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ComponentAce.Compression.Libs.zlib;
using Hydra.Client.Models.Hydra;
using Newtonsoft.Json;

namespace Hydra.Client.Http
{
    internal static class HttpContentUtil
    {
        public static HttpContent CreateHydraContent<TRequestHead, TRequestData>(
            TRequestHead objHead, TRequestData objData)
        {
            var serialized = SerializeHydraMessage(objHead, objData);
            var content = new ByteArrayContent(serialized);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-hydra-binary");
            return content;
        }

        public static HttpContent CreateCompressedHydraContent<TRequestHead, TRequestData>(
            TRequestHead objHead, TRequestData objData)
        {
            var serialized = SerializeHydraMessage(objHead, objData);
            var content = CompressContent(serialized);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-hydra-binary");
            return content;
        }

        private static byte[] SerializeHydraMessage(object head, object data)
        {
            byte[] headBytes = SerializeHydraData(head);
            byte[] dataBytes = SerializeHydraData(data);

            MemoryStream stream = new MemoryStream();
            BinaryWriter streamWriter = new BinaryWriter(stream, Encoding.ASCII, true);
            streamWriter.Write((int)5);
            streamWriter.Write((int)(headBytes.Length));
            streamWriter.Write((int)(dataBytes.Length));
            streamWriter.Write(headBytes);
            streamWriter.Write(dataBytes);
            stream.Position = 0;

            return stream.ToArray();
        }

        private static byte[] SerializeHydraData(object data)
        {
            switch (data)
            {
                case NullHydraServiceData _:
                    return new byte[0];
                case RawHydraServiceData raw:
                    return raw.Data;
                default:
                    string dataJson = JsonConvert.SerializeObject(data);
                    return Encoding.UTF8.GetBytes(dataJson);
            }
        }

        private static T DeserializeHydraServiceData<T>(byte[] rawData)
        {
            T data = Activator.CreateInstance<T>();
            switch (data)
            {
                case NullHydraServiceData _:
                    break;
                case RawHydraServiceData raw:
                    raw.Data = rawData;
                    break;
                default:
                    var json = Encoding.UTF8.GetString(rawData);
                    JsonConvert.PopulateObject(json, data);
                    break;
            }

            return data;
        }
        
        internal static async Task<(TResponseHead, TResponseData)> ReadHydraContent<TResponseHead, TResponseData>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                return ReadHydraData<TResponseHead, TResponseData>(stream);
            }
        }
        
        internal static async Task<(TResponseHead, TResponseData)> ReadCompressedHydraContent<TResponseHead, TResponseData>(HttpContent content)
        {
            byte[] outputBytes = await DecompressContent(content);
            var stream = new MemoryStream(outputBytes);
            return ReadHydraData<TResponseHead, TResponseData>(stream);
        }

        private static (TResponseHead, TResponseData) ReadHydraData<TResponseHead, TResponseData>(Stream stream)
        {
            using (var streamReader = new BinaryReader(stream, Encoding.ASCII, true))
            {
                int version = streamReader.ReadInt32(); // 5
                int headSize = streamReader.ReadInt32();
                int dataSize = streamReader.ReadInt32();

                byte[] headBytes = streamReader.ReadBytes(headSize);
                byte[] dataBytes = streamReader.ReadBytes(dataSize);
                
                TResponseHead head = DeserializeHydraServiceData<TResponseHead>(headBytes);
                TResponseData data = DeserializeHydraServiceData<TResponseData>(dataBytes);

                return (head, data);
            }
        }


        public static async Task<T> ReadJsonContent<T>(HttpContent content)
        {
            var responseContent = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public static StringContent CreateJsonContent(object obj)
        {
            var objJson = JsonConvert.SerializeObject(obj);
            var content = new StringContent(objJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // no encoding
            return content;
        }

        public static HttpContent CreateCompressedJsonContent(object obj)
        {
            var objJson = JsonConvert.SerializeObject(obj);
            var objBytes = Encoding.UTF8.GetBytes(objJson);
            var content = CompressContent(objBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        public static async Task<T> ReadCompressedJsonContent<T>(HttpContent content)
        {
            byte[] outputBytes = await DecompressContent(content);
            string outputJsonString = Encoding.UTF8.GetString(outputBytes);
            T obj = JsonConvert.DeserializeObject<T>(outputJsonString);
            return obj;
        }

        private static HttpContent CompressContent(byte[] bytes)
        {
            var outputStream = new MemoryStream();
            using (var zstream = new ZOutputStream(outputStream, zlibConst.Z_BEST_COMPRESSION))
            {
                zstream.Write(bytes, 0, bytes.Length);
                zstream.Flush();
            }

            return new ByteArrayContent(outputStream.ToArray());
        }

        private static async Task<byte[]> DecompressContent(HttpContent content)
        {
            var outStream = new MemoryStream();
            using (var stream = await content.ReadAsStreamAsync())
            using (var input = new ZInputStream(stream))
            {
                int data;
                var stopByte = -1;
                while (stopByte != (data = input.Read()))
                {
                    var dataByte = (byte) data;
                    outStream.WriteByte(dataByte);
                }
            }
            byte[] outputBytes = outStream.ToArray();
            return outputBytes;
        }

    }
}