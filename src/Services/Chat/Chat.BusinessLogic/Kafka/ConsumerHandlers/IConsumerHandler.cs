﻿namespace Chat.BusinessLogic.Kafka.ConsumerHandlers
{
    public interface IConsumerHandler<Tk,Tv>
        where Tv : class
    {
        Task HandleAsync(Tk tk, Tv v);
    }
}
