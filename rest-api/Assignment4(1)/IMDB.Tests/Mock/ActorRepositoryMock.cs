using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using Moq;
using IMDBAPI.Models.DBModels;
using AutoMapper;
using IMDBAPI.Helpers;

namespace IMDB.Tests.Mock
{
    public class ActorRepositoryMock
    {
        public static Mock<IActorRepository> ActorRepositoryMoq;
        private static readonly IEnumerable<ActorDB> ListOfActors = new List<ActorDB>
        {
            new ActorDB
            {
                Id = 1,
                Name = "A1",
                Bio = "--",
                Dob = new DateTime(2002,9,9),
                Sex="Male"
            },
            new ActorDB
            {
                Id = 2,
                Name = "A2",
                Bio = "--",
                Dob = new DateTime(2002,9,10),
                Sex="Male"
            }
        };
        public ActorRepositoryMock()
        {
        }
        public static void MockAll()
        {
            ActorRepositoryMoq = new Mock<IActorRepository>();
            ActorRepositoryMoq.Setup(foo => foo.AddActor(It.IsAny<ActorRequest>()));
            ActorRepositoryMoq.Setup(foo => foo.UpdateActor(It.IsAny<ActorRequest>(), It.IsAny<int>()));
            ActorRepositoryMoq.Setup(foo => foo.DeleteActor(It.IsAny<int>()));
            ActorRepositoryMoq.Setup(foo => foo.ActorById(It.IsAny<int>())).Callback<int>(x=>Console.WriteLine(x)).Returns((int x)=>ListOfActors.FirstOrDefault(z=>z.Id==x));
            ActorRepositoryMoq.Setup(foo => foo.GetAllActors()).Returns(ListOfActors.ToList());
        }
        
    }
}
