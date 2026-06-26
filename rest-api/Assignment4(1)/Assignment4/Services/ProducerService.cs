using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AutoMapper;
using IMDBAPI.Services.Interfaces;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;


namespace IMDBAPI.Services
{
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }
        public List<ProducerResponse> GetProducers()
        {
            return _mapper.Map<List<ProducerResponse>>(_producerRepository.GetAllProducers());
        }
        private bool isPureString(String p0)
        {
            var regex = new Regex(@"[A-Za-z.,]+");
            bool res = regex.IsMatch(p0);
            return res;
        }
        public int AddProducer(ProducerRequest producer)
        {

            if (String.IsNullOrWhiteSpace(producer.Name))
                throw new InvalidInputException("Name is empty");
            if (String.IsNullOrWhiteSpace(producer.Sex))
                throw new InvalidInputException("Sex is empty");
            if (String.IsNullOrWhiteSpace(producer.Bio))
                throw new InvalidInputException("Bio is empty");
            if (!isPureString(producer.Name))
                throw new InvalidInputException("Name is not valid");
            if (!isPureString(producer.Bio))
                throw new InvalidInputException("Bio is not valid");
            if (producer.Dob > DateTime.Now)
                throw new InvalidInputException("Dob is not valid");
            return _producerRepository.AddProducer(producer);
        }
        public void ValidateProducerId(int id)
        {
            var Producer = _producerRepository.ProducerById(id);
            if (Producer == null)
                throw new NotFoundException("Producer with given id doesn't exist");
        }
        public ProducerResponse GetProducerById(int id)
        {
            var Producer = _producerRepository.ProducerById(id);
            if (Producer == null)
                throw new NotFoundException("Producer with given id doesn't exist"+id+" "+Producer);
            return _mapper.Map<ProducerResponse>(Producer);
        }
        public void DeleteProducer(int id)
        {
            ValidateProducerId(id);
            try
            {
                _producerRepository.DeleteProducer(id);
            }
            catch (Exception e)
            {
                throw new InternalServerException("Producer can't be deleted");
            }
        }
        public void UpdateProducer(ProducerRequest producer, int id)
        {
            ValidateProducerId(id);
            if (String.IsNullOrWhiteSpace(producer.Name))
                throw new InvalidInputException("Name is empty");
            if (String.IsNullOrWhiteSpace(producer.Sex))
                throw new InvalidInputException("Sex is empty");
            if (String.IsNullOrWhiteSpace(producer.Bio))
                throw new InvalidInputException("Bio is empty");
            if (!isPureString(producer.Name))
                throw new InvalidInputException("Name is not valid");
            if (!isPureString(producer.Bio))
                throw new InvalidInputException("Bio is not valid");
            if (producer.Dob > DateTime.Now)
                throw new InvalidInputException("Dob is not valid");
            _producerRepository.UpdateProducer(producer, id);
        }
    }
}
