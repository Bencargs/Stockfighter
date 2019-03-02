using Stockfighter.Model;
using Stockfighter.Model.Account.Reply;
using Stockfighter.Model.Account.Request;

namespace Stockfighter.Controller
{
    public class GameManager
    {
        public StartReply StartLevel(string level)
        {
            PostStartRequest request = new PostStartRequest(level);
            request.Execute();
            return (request.Reply as StartReply) ?? new StartReply(false);
        }

        public BaseReply StopLevel(int instance)
        {
            PostStopRequest request = new PostStopRequest(instance);
            request.Execute();
            return request.Reply as BaseReply;
        }

        public GetInstanceReply GetInstance(int instance)
        {
            GetInstanceRequest request = new GetInstanceRequest(instance);
            request.Execute();
            return (request.Reply as GetInstanceReply) ?? new GetInstanceReply(false);
        }

        public BaseReply GetLevels()
        {
            GetLevelsRequest request = new GetLevelsRequest();
            request.Execute();
            return request.Reply as BaseReply;
        }

        public BaseReply RestartLevel(int instance)
        {
            PostPostRestartRequest request = new PostPostRestartRequest(instance);
            request.Execute();
            return request.Reply as BaseReply;
        }

        public BaseReply ResumeLevel(int instance)
        {
            PostResumeRequest request = new PostResumeRequest(instance);
            request.Execute();
            return request.Reply as BaseReply;
        }
    }
}
