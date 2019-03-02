namespace Stockfighter.Model
{
    public interface IRequest
    {
        string Url { get; set; }

        IReply Reply { get; set; }
    }
}
