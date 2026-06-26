using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;
using IMDBAPI.Models.DBModels;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IProducerRepository
    {

        int AddProducer(ProducerRequest producer);
       
        void UpdateProducer(ProducerRequest producer, int id);
        void DeleteProducer(int id);
        ProducerDB ProducerById(int id);
        List<ProducerDB> GetAllProducers();
    }
}
