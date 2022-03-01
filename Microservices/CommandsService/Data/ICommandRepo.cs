using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //Platform Releated
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExists(int platformId);

        //Command Releated
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}