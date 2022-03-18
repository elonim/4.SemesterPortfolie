using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platformservice.AsyncDataServices;
using Platformservice.Data;
using Platformservice.Dtos;
using Platformservice.Models;
using Platformservice.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platformservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandDataClient _commandDataClient;
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ImessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepo repo,
            ImessageBusClient messageBusClient,
            ICommandDataClient commandDataClient,
            IMapper mapper)
        {
            _commandDataClient = commandDataClient;
            _repo = repo;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platformItems = _repo.GetallPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repo.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CratePlatform(PlatformCreateDto createDto)
        {
            var platformModel = _mapper.Map<Platform>(createDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            //Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send Synchronously: {ex.Message}");
            }
            //Send Async Message
            try
            {
                var platformpublishedDto = _mapper.Map<PlatformPublishdDto>(platformReadDto);
                platformpublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformpublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send Asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDto }, platformReadDto);
        }
    }
}
