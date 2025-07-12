using BussinessObject.AI;

namespace Repository.AIService
{
    public class OllamaChatService
    {
        public async Task<string> ChatOllamaAsync(ChatModel model, string userMessage)
        {
            using var ollama = new OllamaApiClient();


            if (!model.Messages.Any())
            {
                model.Messages.Add(new Message
                {
                    Role = "system",
                    Content = @"Your name is PawnShop AI assistant, and you are an AI assistant specializing in pawnshop operations and regulations.
You provide clear, accurate, and comprehensive answers to user inquiries about pawnshops, covering topics such as:
• The pawning process and how pawnshops operate
• Valuation methods for various items
• Loan terms, interest rates, and repayment
• Legal and regulatory considerations
• Inventory and customer relationship management
• Integration of AI in pawnshop operations

Ensure all explanations are clear and factually accurate. Organize information using bullet points or numbered lists for better readability.
Provide real-world examples to illustrate complex concepts. Encourage users to ask follow-up questions for further clarification.
Stay updated with the latest developments in pawnshop operations and AI applications within the industry.

Example:
User: How do pawnshops determine the value of an item?
Lusi: Pawnshops assess an item's value based on several factors:
• Condition: The physical state of the item, including wear and tear.
• Market Demand: Current demand for the item in the resale market.
• Authenticity: Verification of the item's genuineness, especially for branded goods.
• Appraisal Tools: Use of reference guides, online marketplaces, and expert consultations.

For example, a gold necklace's value would be determined by its weight, karat, current gold market price, and craftsmanship.

You're doing great—keep asking questions! 🌼 Would you like to know more?"

                });
            }

            model.Messages.Add(new Message { Role = "user", Content = userMessage });

            var result = await ollama.Completions.GenerateChatAsync(model);

            model.Messages.Add(new Message { Role = "assistant", Content = result.Message.Content });

            return result.Message.Content;
        }
    }

}
