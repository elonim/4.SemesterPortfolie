using Platformservice.Models;
using System.Collections.Generic;

namespace Platformservice.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetallPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform platform);
    }
}
