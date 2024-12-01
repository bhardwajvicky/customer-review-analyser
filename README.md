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

## Sample Review Used
- I picked an actual review from Google Reviews for a Fleet Management company
- ![image](https://github.com/user-attachments/assets/063d7f00-9554-4d86-8dbd-25d1409c552e)

### Report generated by Review Analyzer application
```bash
[
  {
    "Sentiment": "Negative"
  },
  {
    "Stages": [
      {
        "Stage": "Pre-Order",
        "Rating": 1,
        "RatingReason": "The feedback highlights issues with incompetence and a lack of personal service, indicating problems in the initial stages before the order was confirmed."
      },
      {
        "Stage": "Order to Delivery",
        "Rating": 1,
        "RatingReason": "Multiple delivery postponements and administrative errors with finance agreements contributed to a poor experience during the order to delivery stage."
      },
      {
        "Stage": "Post-Delivery",
        "Rating": 1,
        "RatingReason": "Receiving a request to sign a lease agreement after car delivery suggests ongoing disorganization even after delivery was completed."
      }
    ]
  },
  {
    "Departments": [
      {
        "Department": "Sales",
        "Rating": 2,
        "RatingReason": "Numerous proposals and agreements were required before finalizing the lease."
      },
      {
        "Department": "Customer Service",
        "Rating": 1,
        "RatingReason": "Incompetent service with ineffective communication and unwillingness to assist."
      },
      {
        "Department": "Delivery",
        "Rating": 2,
        "RatingReason": "Delivery was postponed twice due to internal administrative issues."
      },
      {
        "Department": "Finance",
        "Rating": 1,
        "RatingReason": "Slow process and inclusion of non-financeable items in the lease."
      },
      {
        "Department": "Insurance",
        "Rating": "",
        "RatingReason": ""
      },
      {
        "Department": "Software",
        "Rating": 2,
        "RatingReason": "Over-reliance on automated emails and processes."
      }
    ]
  },
  {
    "Summary": [
      "Customer dealt with incompetent staff at the organization.",
      "Company heavily relies on automated processes and emails, leading to ineffective customer support.",
      "Customer experienced poor communication, with misaligned assurances in emails.",
      "Car delivery was postponed twice due to slow administrative processes and issues with lease details.",
      "Customer had to sign numerous proposals and make about 30 unproductive phone calls.",
      "Customer received a request to sign a new lease agreement after already having completed the process.",        
      "Customer expressed surprise at the company's ability to remain operational despite disorganization and poor service."
    ]
  }
]
```

```bash
Corrective Actions:
{
  "technical_corrective_actions": [
    {
      "action": "Implement AI-driven chatbots and virtual assistants",
      "benefit": "Enhance preliminary customer interactions, reducing wait times and improving competence during the pre-order stage."
    },
    {
      "action": "Develop a centralized order tracking system",
      "benefit": "Provide real-time updates to customers regarding order status and ensure delivery timelines are accurate and adhered to."
    },
    {
      "action": "Integrate an automated document processing system",
      "benefit": "Streamline finance and lease agreement processes to reduce errors and facilitate faster processing."    },
    {
      "action": "Upgrade CRM software",
      "benefit": "Ensure seamless communication across departments, enabling synchronized customer interactions and reducing miscommunication."
    }
  ],
  "non_technical_corrective_actions": [
    {
      "action": "Conduct staff training sessions on customer service excellence",
      "benefit": "Improve staff competence and communication skills, ensuring a more personalized and effective service experience."
    },
    {
      "action": "Establish a dedicated customer service escalation team",
      "benefit": "Address and resolve customer issues promptly, minimizing the number of unproductive calls and enhanc      "benefit": "Address and resolve customer issues promptly, minimizing the number of unproductive calls and enhancing overall satisfaction."
    },
    {
      "action": "Revise internal processes",
      "benefit": "Ensure alignment between automated emails and actual customer service capabilities to avoid contradictory assurances."
    }
  ]
}
```

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
