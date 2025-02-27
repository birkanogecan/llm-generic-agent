using BirkanAI.Domain;
using BirkanAI.Domain.AIModel;
using BirkanAI.Domain.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BirkanAI.Infra.AIClient
{
    public class AIClientGateway
    {
        private AIClientSettings _settings;

        public AIClientGateway(IConfiguration configuration)
        {
            _settings = configuration.GetSection("AIClient").Get<AIClientSettings>();
        }

      
        public async Task<string> CallModelAsync(string prompt)
        {
            Request request = PrepareRequestForTool(prompt);

            Response response = new Response();

            var url = $"{_settings.Url}?key={_settings.ApiKey}";
            var responseFromModel = await RestClient<Request, Response>.PostAsync(request, url);

            var func = responseFromModel?.Candidates?[0].Content?.Parts?[0].FunctionCall;
            if (func != null)
            {
                string result = AggregateFactory.ExecuteCommand(func.Name, responseFromModel);
                return result;
            }
            else
            {
                return responseFromModel.Candidates[0].Content.Parts[0].Text;
            }
        }

        private Request PrepareRequestForTool(string prompt)
        {
            Request request = new Request();
            request.Contents = new Content() { Role = "user", Parts = new Parts() { Text = prompt } };
            request.Tools = new List<Tool>();
            
            //get_weather
            FunctionDeclaration functionDeclaration = new FunctionDeclaration();
            functionDeclaration.Name = "get_weather";
            functionDeclaration.Description = "find current weather conditions based on the location";
            functionDeclaration.Parameters = new FunctionParameters() { Type = "object" };
            functionDeclaration.Parameters.Properties = new Dictionary<string, FunctionProperty>();
            functionDeclaration.Parameters.Properties.Add("location", new FunctionProperty() { Type = "string", Description = "city or location. example; istanbul, ankara, new york etc.." });

            functionDeclaration.Parameters.Required = new List<string>() { "location" };

            request.Tools.Add(new Tool() { FunctionDeclarations = new List<FunctionDeclaration>() { functionDeclaration } });
            //get_weather

            //get_weather_rainfall
            FunctionDeclaration functionDeclaration2 = new FunctionDeclaration();
            functionDeclaration2.Name = "get_weather_rainfall";
            functionDeclaration2.Description = "find current rainfall amount based on the location";
            functionDeclaration2.Parameters = new FunctionParameters() { Type = "object" };
            functionDeclaration2.Parameters.Properties = new Dictionary<string, FunctionProperty>();
            functionDeclaration2.Parameters.Properties.Add("location", new FunctionProperty() { Type = "string", Description = "city or location. example; istanbul, ankara, new york etc.." });

            functionDeclaration2.Parameters.Required = new List<string>() { "location" };

            request.Tools.Add(new Tool() { FunctionDeclarations = new List<FunctionDeclaration>() { functionDeclaration2 } });
            //get_weather_rainfall

            return request;
        }
    }

    public class AIClientSettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
