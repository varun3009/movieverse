using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;

using System.Linq;
using System.Collections.Generic;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IActorRepository
    {
        int AddActor(ActorRequest actor);
        
        void UpdateActor(ActorRequest actor, int id);
        void DeleteActor(int id);
        ActorDB ActorById(int id);
        List<ActorDB> GetAllActors();
    }
}
