namespace Musicians.Application.Interfaces.Persistance
{
    public interface ITransactionUnit
    {
        Task RollbackTransactionAsync();

        Task CommitTransactionAsync();

        void FlushTransaction();

        Task StartTransactionAsync();
    }
}
