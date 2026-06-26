using System.Threading.Tasks;

namespace IMDBAPI.Services.Interfaces
{
    public interface IMessageService<T> where T : class
    {
        public Task PostMessageAsync(T message, string queueName);
    }
}
