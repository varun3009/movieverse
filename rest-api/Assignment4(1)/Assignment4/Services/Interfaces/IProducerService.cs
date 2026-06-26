using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;

namespace IMDBAPI.Services.Interfaces
{
    public interface IProducerService
    {

        List<ProducerResponse> GetProducers();
        public int AddProducer(ProducerRequest Producer);
        public ProducerResponse GetProducerById(int id);
        public void UpdateProducer(ProducerRequest Producer, int id);
        public void DeleteProducer(int id);
    }
}
