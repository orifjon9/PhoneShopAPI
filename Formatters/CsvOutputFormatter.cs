using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using PhoneShopAPI.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace PhoneShopAPI.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(System.Type type)
        {
            if (typeof(Phone).IsAssignableFrom(type)
                || typeof(IEnumerable<Phone>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            const string separator = ";";
            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            buffer.AppendLine($"Id{separator}Name{separator}Description");
            if (context.Object is IEnumerable<Phone>)
            {
                ((IEnumerable<Phone>)context.Object)
                    .ToList()
                    .ForEach(item =>
                    {
                        buffer.AppendLine($"{item.Id}{separator}{item.Name}{separator}{item.Description}");
                    });
            }
            else
            {
                var item = (Phone)context.Object;
                buffer.AppendLine($"{item.Id}{separator}{item.Name}{separator}{item.Description}");
            }
            return response.WriteAsync(buffer.ToString());
        }
    }
}