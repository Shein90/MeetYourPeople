﻿using Common.DataTransferObjects;
using System.Threading.Tasks;

namespace Common.Services.Abstract;

/// <summary>
/// Provides special functional for consume message from any message broker.
/// </summary>
public interface IEventManager
{
    Task CreateEvent(EventDto eventDto);
}
