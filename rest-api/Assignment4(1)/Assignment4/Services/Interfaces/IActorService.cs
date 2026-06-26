using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;

namespace IMDBAPI.Services.Interfaces
{
    public interface IActorService
    {
        List<ActorResponse> GetActors();
        public int AddActor(ActorRequest actor);
        public ActorResponse GetActorById(int id);
        public void UpdateActor(ActorRequest actor, int id);
        public void DeleteActor(int id);
    }
}
