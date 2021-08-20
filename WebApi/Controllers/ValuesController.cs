using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace OtelPropagation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private static readonly TextMapPropagator Propagator = Propagators.DefaultTextMapPropagator;

        private readonly ILogger<ValuesController> _logger;
        private readonly IConfiguration _configuration;

        public ValuesController(ILogger<ValuesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public string [] Get()
        {
            var activity = Activity.Current;

            //Propagator.Extract(new PropagationContext(activity.Context, Baggage.Current), Request.Headers, InjectContextIntoHeader);
            
            return new string[] { "sensor1", "sensor2", "sensor3" };
           
        }

        

        private IEnumerable<string> InjectContextIntoHeader(IHeaderDictionary arg1, string arg2)
        {
            return null;
        }

       

      




    }
}
