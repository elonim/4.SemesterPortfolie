
using Platformservice.Dtos;

namespace Platformservice.AsyncDataServices
{
    public interface ImessageBusClient
    {
        void PublishNewPlatform(PlatformPublishdDto PlatformPublishdDto);
    }
}