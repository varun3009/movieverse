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
    public class ProducerRepositoryMock
    {
        public static Mock<IProducerRepository> ProducerRepositoryMoq;
        private static readonly IEnumerable<ProducerDB> ListOfProducers = new List<ProducerDB>
        {
            new ProducerDB
            {
                Id = 1,
                Name = "A1",
                Bio = "--",
                Dob = new DateTime(2002,9,9),
                Sex="Male"
            },
            new ProducerDB
            {
                Id = 2,
                Name = "A2",
                Bio = "--",
                Dob = new DateTime(2002,9,10),
                Sex="Male"
            }
        };
        public ProducerRepositoryMock()
        {
        }
        public static void MockAll()
        {
            ProducerRepositoryMoq = new Mock<IProducerRepository>();
            
            ProducerRepositoryMoq.Setup(foo => foo.AddProducer(It.IsAny<ProducerRequest>()));
            ProducerRepositoryMoq.Setup(foo => foo.UpdateProducer(It.IsAny<ProducerRequest>(), It.IsAny<int>()));
            ProducerRepositoryMoq.Setup(foo => foo.DeleteProducer(It.IsAny<int>()));
            ProducerRepositoryMoq.Setup(foo => foo.ProducerById(It.IsAny<int>())).Callback<int>(x => Console.WriteLine(x)).Returns((int x) => ListOfProducers.FirstOrDefault(z => z.Id == x));
            ProducerRepositoryMoq.Setup(foo => foo.GetAllProducers()).Returns(ListOfProducers.ToList());
        }

    }
}
