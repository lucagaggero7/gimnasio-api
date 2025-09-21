using Gimnasio.Server.Datos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Net.Http;
using System.Text.Json;

namespace Gimnasio.Server.Controllers
{

    [Route("codigos-area")]
    [ApiController]
    public class CodigosAreaControllers : ControllerBase
    {
        private readonly IClientesRepositorio clienteRepositorio;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CodigosAreaControllers> logger;

        private const string cacheKey = "CodigosArea";

        public CodigosAreaControllers(IClientesRepositorio clienteRepositorio, IOutputCacheStore outputCacheStore,
                                      IHttpClientFactory httpClientFactory, ILogger<CodigosAreaControllers> logger)
        {
            this.clienteRepositorio = clienteRepositorio;
            this.outputCacheStore = outputCacheStore;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        /// <summary>
        /// Obtiene todos los codigos de area registrados en la api del gobierno.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los prefijos numericos.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de prefijos.
        /// </returns>
        [HttpGet]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                string url = "https://sheets.googleapis.com/v4/spreadsheets/1idjUapSs3yrWUSZt7gO-PIjv7QjJ9yTrQcKQPdwjh7g/values/hoja%201?key=AIzaSyCq2wEEKL9-6RmX-TkW23qJsrmnFHFf5tY&alt=json";

                var response = await client.GetStringAsync(url);
                var json = JsonDocument.Parse(response);

                if (!json.RootElement.TryGetProperty("values", out var values))
                {
                    return BadRequest("No se encontraron datos en la hoja de Google Sheets.");
                }

                var codigos = values
                    .EnumerateArray()
                    .Skip(2)
                    .Select(row => row[2].GetString())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                return Ok(codigos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error obteniendo códigos de área");
                return StatusCode(500, "No se pudieron obtener los códigos de área.");
            }
        }
    }
}
