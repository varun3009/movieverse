using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IMDBAPI.Repository;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Services.Interfaces;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using AutoMapper;
using IMDBAPI.Models.ResponseModels;
using AutoMapper.Configuration.Conventions;


namespace IMDBAPI.Services
{
    public class ActorService:IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        
        public ActorService(IActorRepository actorRepository,IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public List<ActorResponse> GetActors()
        {
            return _mapper.Map<List<ActorResponse>>(_actorRepository.GetAllActors());
        }
        private bool isPureString(String p0)
        {
            var regex = new Regex(@"[A-Za-z.,]+");
            bool res = regex.IsMatch(p0);
            return res;
        }
        public int AddActor(ActorRequest actor)
        {

            if (String.IsNullOrWhiteSpace(actor.Name))
                throw new InvalidInputException("Name is empty");
            if (String.IsNullOrWhiteSpace(actor.Sex))
                throw new InvalidInputException("Sex is empty");
            if (String.IsNullOrWhiteSpace(actor.Bio))
                throw new InvalidInputException("Bio is empty");
            if (!isPureString(actor.Name))            
                throw new InvalidInputException("Name is not valid");
            if (!isPureString(actor.Bio))
                throw new InvalidInputException("Bio is not valid");
            if(actor.Dob>DateTime.Now)
                throw new InvalidInputException("Dob is not valid");
            return _actorRepository.AddActor(actor);
        }
        public void ValidateActorId(int id)
        {
            var actor = _actorRepository.ActorById(id);
            if (actor == null)
                throw new NotFoundException("Actor with given id doesn't exist");
        }
        public ActorResponse GetActorById(int id)
        {
            var actor= _actorRepository.ActorById(id);
            if(actor == null)
                throw new NotFoundException("Actor with given id doesn't exist");
            return _mapper.Map<ActorResponse>(actor);
        }
        public void DeleteActor(int id)
        {
            
            ValidateActorId(id);
            try
            {
                _actorRepository.DeleteActor(id);
            }
            catch(Exception e) {
                throw new InternalServerException("Actor can't be deleted");
            }
        }
        public void UpdateActor(ActorRequest actor,int id)
        {
            ValidateActorId(id);
            if (String.IsNullOrWhiteSpace(actor.Name))
                throw new InvalidInputException("Name is empty");
            if (String.IsNullOrWhiteSpace(actor.Sex))
                throw new InvalidInputException("Sex is empty");
            if (String.IsNullOrWhiteSpace(actor.Bio))
                throw new InvalidInputException("Bio is empty");
            if (!isPureString(actor.Name))
                throw new InvalidInputException("Name is not valid");
            if (!isPureString(actor.Bio))
                throw new InvalidInputException("Bio is not valid");
            if (actor.Dob > DateTime.Now)
                throw new InvalidInputException("Dob is not valid");
            _actorRepository.UpdateActor(actor, id);
        }

    }
}
