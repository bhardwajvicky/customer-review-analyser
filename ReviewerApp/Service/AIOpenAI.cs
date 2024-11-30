using OpenAI.Chat;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ReviewerApp.Service
{
    public class AIOpenAI
    {
        ChatClient client;
        public AIOpenAI(IConfiguration configuration)
        {
            string model = configuration["OpenAI:Model"];
            string apiKey = configuration["OpenAI:ApiKey"];
            client = new(model: model, apiKey: apiKey);
        }

        public string GetReviewAnalysis(string review)
        {
            ChatCompletion completion = client.CompleteChat(review);
            string result = completion.Content[0].Text;
            return CleanJsonString(result); // Clean the JSON string before returning
        }

        private string CleanJsonString(string jsonString)
        {
            jsonString = jsonString.Replace("\\u0022", "\"").Replace("\\n", "").Replace("\\r", "").Trim();
            if (jsonString.StartsWith("'''json") && jsonString.EndsWith("'''"))
            {
                jsonString = jsonString.Substring(7, jsonString.Length - 10).Trim();
            }
            return jsonString;
        }
    }
}
