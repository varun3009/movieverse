using AutoMapper;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
namespace IMDBAPI.Repository
{
    public class ProducerRepository:BaseRepository<ProducerDB>,IProducerRepository
    {
        private readonly IMapper _mapper;
        public ProducerRepository(IMapper mapper, IOptions<ConnectionString> options) : base(options.Value.IMDBDB)
        {
            _mapper = mapper;
           
        }
        public int AddProducer(ProducerRequest producer)
        {
            var query = @"INSERT INTO Foundation.Persons
            (NAME,
             bio,
             sex,
             dob,
             roleid)
            OUTPUT INSERTED.Id
            VALUES     (@Name,
            @Bio,
            @Sex,
            @Dob,
            2) ";
            return QueryPerson(query, producer);
            
        }
        
        public void UpdateProducer(ProducerRequest producer, int id)
        {
            var producerdb = _mapper.Map<ProducerDB>(producer);
            producerdb.Id = id;
            var query = @"UPDATE Foundation.Persons
            SET    NAME = @Name,
                   bio = @Bio,
                   sex = @Sex,
                   dob = @Dob
            WHERE  id = @Id ";
            ExecuteQuery(query, producerdb);
            
        }
        public void DeleteProducer(int id)
        {
            var query = @"DELETE FROM Foundation.Persons
            WHERE  id = @id ";
            ExecuteQuery(query, new { id = id });
            
        }
        public ProducerDB ProducerById(int id)
        {
            var query = @"SELECT p.id Id,
                   [sex],
                   [bio],
                   [dob],
                   [name]
            FROM   Foundation.Persons p(nolock)
                   JOIN Foundation.Roles r(nolock)
                     ON r.id = p.roleid
            WHERE  roleId=2
                   AND p.id = @Id ";
            var item= Get(query, new {Id=id});
            return item;
        }
        public List<ProducerDB> GetAllProducers()
        {
            var query = @"SELECT p.id Id,
                   [sex],
                   [bio],
                   [dob],
                   [name]
            FROM   Foundation.Persons p(nolock)
                   JOIN Foundation.Roles r(nolock)
                     ON r.id = p.roleid
            WHERE  roleId=2 ";
            var items = GetAll(query);
            return items.ToList();
        }
    }
}
