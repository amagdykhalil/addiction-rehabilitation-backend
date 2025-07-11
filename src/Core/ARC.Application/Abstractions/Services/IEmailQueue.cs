﻿namespace ARC.Application.Abstractions.Services
{
    public interface IEmailQueue
    {
        Task EnqueueEmailAsync(CompiledEmailMessage message);
        Task<CompiledEmailMessage> DequeueEmailAsync();
    }
}
