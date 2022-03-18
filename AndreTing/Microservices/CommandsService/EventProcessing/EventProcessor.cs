using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using CommandsService.Dtos;
using CommandsService.Data;
using CommandsService.Models;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper _mapper;
        //ScopedFactory kan bruges i en klasse som er dependency injected som singleton 
        //hvor man har behov for eoget der er scoped i det her tilfælde repository
        //er pt ikke helt sikker på om det er en god ide
        //men det er nødvendigt i det her tilfælde fordi denne klasse skal injectes i messagebussen som er en singleton
        //og man kan ikke injecte noget som har en kortere levetid i en singleton
        private readonly IServiceScopeFactory _scopeFactory;
        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            if (eventType == EventType.PlatformPublished)
            {
                AddPlatform(message);
                return;
            }

        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishdDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExist(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Platform added...");
                    }
                    else
                    {
                        Console.WriteLine("--> Platform Alreaty exists...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            if (eventType.Event == "Platform_Published")
            {
                Console.WriteLine("--> Platform Published Event Detected");
                return EventType.PlatformPublished;
            }
            Console.WriteLine("--> Could not determine the event type");
            return EventType.Undetermined;
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}