namespace BussinessObject.AI
{
    public class ChatModel
    {
        public string Model { get; set; } = "tinyllama:latest";
        public string Request { get; set; }
        public List<string> Context { get; set; }
        public List<Message> Messages { get; set; } = new();
    }

}
