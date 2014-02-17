using Hazelcast.Serialization.Hook;

namespace Hazelcast.Client.Request.Concurrent.Semaphore
{
    internal class InitRequest : SemaphoreRequest
    {
        public InitRequest()
        {
        }

        public InitRequest(string name, int permitCount) : base(name, permitCount)
        {
        }

        public override int GetClassId()
        {
            return SemaphorePortableHook.Init;
        }
    }
}