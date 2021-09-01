using ApiGatewayOcelot.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiGatewayOcelot.Aggregators
{
    public class mesaSalonAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var salonesResponseContent = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var mesasResponseContent = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            

            var salones = JsonConvert.DeserializeObject<List<Salones>>(salonesResponseContent);
            var mesas = JsonConvert.DeserializeObject<List<Mesa>>(mesasResponseContent);

            

            foreach (var mesa in mesas)
            {
                var mesaSalon = salones.Where(sa => sa.Id == mesa.Salon).ToList();
                mesa.Salonesss.AddRange(mesaSalon);
            }

            var postByUserString = JsonConvert.SerializeObject(mesas);

            var stringContent = new StringContent(postByUserString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
