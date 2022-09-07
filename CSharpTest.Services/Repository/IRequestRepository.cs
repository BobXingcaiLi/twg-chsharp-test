namespace CSharpTest.Services.Repository
{
    public interface IRequestRepository
    {
        Task<long> InsertAsync(char kind);
    }
}
