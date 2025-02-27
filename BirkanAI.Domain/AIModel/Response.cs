using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BirkanAI.Domain.AIModel
{
    public class Response
    {
        [JsonPropertyName("candidates")]
        public List<Candidate> Candidates { get; set; }

        [JsonPropertyName("usageMetadata")]
        public UsageMetadata UsageMetadata { get; set; }

        [JsonPropertyName("modelVersion")]
        public string ModelVersion { get; set; }
    }
    public class Candidate
    {
        [JsonPropertyName("content")]
        public Contents Content { get; set; }

        [JsonPropertyName("finishReason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("avgLogprobs")]
        public double AvgLogprobs { get; set; }
    }
    public class Contents
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }
    }
    public class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("functionCall")]
        public FunctionCall FunctionCall { get; set; }
    }

    public class FunctionCall
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("args")]
        public FunctionArgs Args { get; set; }
    }
    public class FunctionArgs
    {
        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("movie")]
        public string Movie { get; set; }
    }
    public class UsageMetadata
    {
        [JsonPropertyName("promptTokenCount")]
        public int PromptTokenCount { get; set; }

        [JsonPropertyName("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }

        [JsonPropertyName("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        [JsonPropertyName("promptTokensDetails")]
        public List<TokenDetail> PromptTokensDetails { get; set; }

        [JsonPropertyName("candidatesTokensDetails")]
        public List<TokenDetail> CandidatesTokensDetails { get; set; }
    }

    public class TokenDetail
    {
        [JsonPropertyName("modality")]
        public string Modality { get; set; }

        [JsonPropertyName("tokenCount")]
        public int TokenCount { get; set; }
    }
}
