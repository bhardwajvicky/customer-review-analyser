# Customer Review Analyzer

This project analyzes customer reviews using OpenAI's GPT model to generate insights such as sentiment analysis, lifecycle ratings, department ratings, employee mentions, and summaries. It also generates final reviews and corrective actions based on the analysis.

## Setup

1. Clone the repository.
2. Rename `appsettings.json.template` to `appsettings.json` and add your OpenAI model and API keys in it.
3. Build the project using your preferred IDE or command line tools.

## Usage

You are provided with two ready examples to test with:

1. **Online Insurance Company Example**:
   - Prompts are in the `PROMPTS-insurance` folder.
   - One customer review from Google is stored in `reviews/review-insurance.txt`.

2. **Fleet Management Company Example**:
   - Prompts are in the `PROMPTS-fleet` folder.
   - One customer review from Google Reviews is stored in `reviews/review-fleet.txt` file.
- You can test by enabling relevant code section in the Program.cs
- ![image](https://github.com/user-attachments/assets/0287c17d-2043-45c4-8505-dd289ce616d9)

### Reviews Folder

- `review-fleet.txt`: Review file for the fleet management company.
- `review-insurance.txt`: Review file for the online insurance company.

### Prompts Folders

- `PROMPTS-fleet`: Contains prompt files specific to the fleet management industry.
- `PROMPTS-insurance`: Contains prompt files specific to the online insurance industry.

## Running the Application

Run the application using your IDE or command line tools. The application will read the review file, analyze it using the prompts, and generate the results in JSON format.

## Output

The results of the analysis will be stored in the following files:

- `reviewResults.json`: Contains the results of the individual analyses.
- `result.json`: Contains the final review and corrective actions.

## Customization

You can test for your domain and company by modifying the prompt files. Below are some of the prompt files you can customize:

- `base-prompt.txt`: Base prompt used for all analyses.
- `department-prompt.txt`: Prompt for analyzing department ratings.
- `lifecycle-prompt.txt`: Prompt for analyzing lifecycle ratings.

## Additional Information

- The application uses dependency injection to manage the OpenAI client.
- JSON strings are cleaned before being processed and stored to ensure proper formatting.

## Future Enhancements

- Create a plugin to identify the customer using data from customer records, emails, and other sources.
- Create a plugin to log technical recommendations in Jira or other project management tools, using a vector database of existing Jira ticket summaries to identify and vote on existing issues or create new tickets.
