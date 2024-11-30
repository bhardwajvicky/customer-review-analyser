using ReviewerApp.Service;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NewConsoleProject
{
    public class Program
    {
        // ***Review & Prompts for Fleet Management Company
        private const string ReviewFile = "review-fleet.txt";
        private const string PromptsPath = "PROMPTS-fleet";
        
        //***Review & Prompts for Online Insurance Company
        //private const string ReviewFile = "review-insurance.txt";
        //private const string PromptsPath = "PROMPTS-insurance";

        private const string ReviewsPath = "reviews";

        // Prompt file names
        private const string BasePromptFile = "base-prompt.txt";
        private const string EmployeePromptFile = "employee-prompt.txt";
        private const string SummaryPromptFile = "summary-prompt.txt";
        private const string SentimentPromptFile = "sentiment-prompt.txt";
        private const string LifecyclePromptFile = "lifecycle-prompt.txt";
        private const string DepartmentPromptFile = "department-prompt.txt";
        private const string FinalReviewPromptFile = "final-review-prompt.txt";
        private const string FixFormattingPromptFile = "fix-formatting-prompt.txt";
        private const string CorrectiveActionsPromptFile = "corrective-actions.txt";

        // Output file names
        private const string ReviewResultsFile = "reviewResults.json";
        private const string ResultFile = "result.json";

        static readonly string _basePrompt = LoadPrompt(BasePromptFile);
        static readonly string _promptEmployee = LoadPrompt(EmployeePromptFile);
        static readonly string _promptSummary = LoadPrompt(SummaryPromptFile);
        static readonly string _promptSentiment = LoadPrompt(SentimentPromptFile);
        static readonly string _promptLifecycle = LoadPrompt(LifecyclePromptFile);
        static readonly string _promptDepartment = LoadPrompt(DepartmentPromptFile);
        static readonly string _promptFinalReview = LoadPrompt(FinalReviewPromptFile);
        static readonly string _promptFixFormatting = LoadPrompt(FixFormattingPromptFile);
        static readonly string _promptCorrectiveActions = LoadPrompt(CorrectiveActionsPromptFile);

        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<AIOpenAI>(sp =>
                        new AIOpenAI(context.Configuration));
                })
                .Build();

            var aiOpenAI = host.Services.GetRequiredService<AIOpenAI>();

            // Delete the JSON files
            File.Delete(ReviewResultsFile);
            File.Delete(ResultFile);

            // Read the review from the reviews folder
            string review = File.ReadAllText(Path.Combine(ReviewsPath, ReviewFile));

            // Use the injected instance instead of creating new
            string[] reviewResults = new string[5];

            Console.WriteLine("Analyzing sentiment...");
            reviewResults[0] = getSentiment(review, aiOpenAI);
            //Console.WriteLine(reviewResults[0]);

            Console.WriteLine("Analyzing lifecycle ratings...");
            reviewResults[1] = getLifecycleRating(review, aiOpenAI);

            Console.WriteLine("Analyzing department ratings...");
            reviewResults[2] = getDepartmentRating(review, aiOpenAI);

            Console.WriteLine("Analyzing employee mentions...");
            reviewResults[3] = getEmployeeRatings(review, aiOpenAI);

            Console.WriteLine("Generating summary...");
            reviewResults[4] = getSummary(review, aiOpenAI);
            //Console.WriteLine(reviewResults[4]);

            Console.WriteLine("Analysis complete! Generating final review...");

            // Store all the responses in a JSON file on local drive
            string json = JsonSerializer.Serialize(reviewResults, new JsonSerializerOptions { WriteIndented = true });
            json = CleanJsonString(json); // Clean the JSON string before writing to file
            File.WriteAllText(ReviewResultsFile, json);

            var finalReview = getFinalReview(aiOpenAI);
            Console.WriteLine(finalReview);

            // Generate corrective actions
            var correctiveActions = getCorrectiveActions(finalReview, aiOpenAI);
            Console.WriteLine("Corrective Actions:");
            Console.WriteLine(correctiveActions);

            // Combine final review and corrective actions into a single JSON object
            var result = new
            {
                FinalReview = finalReview,
                CorrectiveActions = correctiveActions
            };

            // Store the combined result in a JSON file on local drive
            string resultJson = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ResultFile, resultJson);
        }

        public static string getSentiment(string review, AIOpenAI ai)
        {
            var prompt = $"{_basePrompt}{_promptSentiment}. \r\n Customer Review: {review} '''";
            var outcome = ai.GetReviewAnalysis(prompt);
            return CleanJsonString(outcome);
        }

        public static string getLifecycleRating(string review, AIOpenAI ai)
        {
            var prompt = $"{_basePrompt}{_promptLifecycle}. \r\n Customer Review: {review} '''";
            var outcome = ai.GetReviewAnalysis(prompt);
            return CleanJsonString(outcome);
        }

        public static string getDepartmentRating(string review, AIOpenAI ai)
        {
            var prompt = $"{_basePrompt}{_promptDepartment}. \r\n Customer Review: {review} '''";
            var outcome = ai.GetReviewAnalysis(prompt);
            return CleanJsonString(outcome);
        }

        public static string getEmployeeRatings(string review, AIOpenAI ai)
        {
            var outcome = ai.GetReviewAnalysis(_basePrompt + _promptEmployee + "\r\n Customer Review: " + review);
            return CleanJsonString(outcome);
        }

        public static string getSummary(string review, AIOpenAI ai)
        {
            var outcome = ai.GetReviewAnalysis(_basePrompt + _promptSummary + "\r\n Customer Review:  " + review);
            return CleanJsonString(outcome);
        }

        public static string getFinalReview(AIOpenAI ai)
        {
            // Read the review results from the JSON file
            string json = File.ReadAllText(ReviewResultsFile);

            // Generate the initial outcome
            var prompt = $"{_basePrompt}{_promptFinalReview}''' {json} '''";
            var outcome = ai.GetReviewAnalysis(prompt);
            outcome = CleanJsonString(outcome);

            // Use the fix-formatting prompt
            var fixFormattingPrompt = $"{_promptFixFormatting}\n\n'''{outcome}'''";
            var cleanOutput = ai.GetReviewAnalysis(fixFormattingPrompt);

            return CleanJsonString(cleanOutput);
        }

        public static string getCorrectiveActions(string finalReview, AIOpenAI ai)
        {
            var prompt = $"{_basePrompt}{_promptCorrectiveActions}. \r\n Final Review: {finalReview} '''";
            var outcome = ai.GetReviewAnalysis(prompt);
            return CleanJsonString(outcome);
        }

        // Helper method to clean JSON strings
        private static string CleanJsonString(string jsonString)
        {
            jsonString = jsonString.Replace("\\u0022", "\"").Replace("\\n", "").Replace("\\r", "").Trim();
            if ((jsonString.StartsWith("'''json") && jsonString.EndsWith("'''")) ||
                (jsonString.StartsWith("\\u0060\\u0060\\u0060json") && jsonString.EndsWith("\\u0060\\u0060\\u0060")))
            {
                jsonString = jsonString.Substring(7, jsonString.Length - 10).Trim();
            }
            return jsonString;
        }

        private static string LoadPrompt(string fileName)
        {
            return File.ReadAllText(Path.Combine(PromptsPath, fileName));
        }
    }
}

/*
 * Create a plugin to try to identify the customer using data in customer records, mails (around the time rage when the review was posted), and other sources.
 * Create a plugin to that can log technical recommendations in Jira or any other project management tool.
        * This plug can use a Vector database of existing Jira tickets summary to identify if same issue is already logged & add a vote in the existing ticke or create a new ticket.
*/

/*
 * Corrective Action prompt - Provide details of existing client applications/portals details with brief detail about them. This will help recommendations to be mapped to correct product.
 * 
 * 
*/