using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace BirkanAI.Domain.AIModel
{
    public class Request
    {
        [JsonPropertyName("contents")]
        public Content Contents { get; set; }

        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; }
    }
    public class Content
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("parts")]
        public Parts Parts { get; set; }
    }

    public class Parts
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Tool
    {
        [JsonPropertyName("function_declarations")]
        public List<FunctionDeclaration> FunctionDeclarations { get; set; }
    }

    public class FunctionDeclaration
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("parameters")]
        public FunctionParameters Parameters { get; set; }
    }

    public class FunctionParameters
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, FunctionProperty> Properties { get; set; }

        [JsonPropertyName("required")]
        public List<string> Required { get; set; }
    }

    public class FunctionProperty
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
