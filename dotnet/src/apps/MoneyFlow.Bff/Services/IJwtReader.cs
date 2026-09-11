namespace MoneyFlow.Bff.Services
{
    public interface IJwtReader
    {
        JwtReaderDTO Extract(string token);
    }
}