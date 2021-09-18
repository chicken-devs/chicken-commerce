namespace CKE.Infra.Logging.Application
{
    public interface IStartLogger : Microsoft.Extensions.Logging.ILogger
    {
        void Close();
    }
}
