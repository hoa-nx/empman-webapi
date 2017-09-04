using Jil;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmpMan.Web.Infrastructure.Core
{
    public sealed class JilMediaTypeFormatter : MediaTypeFormatter
    {
        private static readonly MediaTypeHeaderValue _applicationJsonMediaType = new MediaTypeHeaderValue("application/json");
        private static readonly MediaTypeHeaderValue _textJsonMediaType = new MediaTypeHeaderValue("text/json");
        private static readonly Task<bool> _done = Task.FromResult(true);

        private static readonly Options _options;

        static JilMediaTypeFormatter()
        {
            _options = new Options(excludeNulls: true, serializationNameFormat: SerializationNameFormat.CamelCase,
                unspecifiedDateTimeKindBehavior: UnspecifiedDateTimeKindBehavior.IsLocal, includeInherited: true,
                dateFormat: DateTimeFormat.ISO8601);
        }

        public JilMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(_applicationJsonMediaType);
            SupportedMediaTypes.Add(_textJsonMediaType);

            SupportedEncodings.Add(new UTF8Encoding(false, true));
            SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream input, HttpContent content, IFormatterLogger formatterLogger)
        {
            var reader = new StreamReader(input);
            var deserialize = TypedDeserializers.GetTyped(type);
            var result = deserialize(reader, _options);
            return Task.FromResult(result);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream output, HttpContent content, TransportContext transportContext)
        {
            var writer = new StreamWriter(output);
            JSON.Serialize(value, writer, _options);
            writer.Flush();
            return _done;
        }
    }


    static class TypedDeserializers
    {
        private const string DeserializeMethodName = "Deserialize";
        private static readonly ConcurrentDictionary<Type, Func<TextReader, Options, object>> _methods;
        private static readonly MethodInfo _deserializeMethodInfo = typeof(JSON).GetMethod(DeserializeMethodName, new[] { typeof(TextReader), typeof(Options) });

        static TypedDeserializers()
        {
            _methods = new ConcurrentDictionary<Type, Func<TextReader, Options, object>>();
        }

        public static Func<TextReader, Options, object> GetTyped(Type type)
        {
            return _methods.GetOrAdd(type, CreateDelegate);
        }

        private static Func<TextReader, Options, object> CreateDelegate(Type type)
        {
            return (Func<TextReader, Options, object>)_deserializeMethodInfo
                .MakeGenericMethod(type)
                .CreateDelegate(typeof(Func<TextReader, Options, object>));
        }
    }
}