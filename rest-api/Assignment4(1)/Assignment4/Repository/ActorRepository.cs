using AutoMapper;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
namespace IMDBAPI.Repository
{
    public class ActorRepository:BaseRepository<ActorDB>,IActorRepository
    {
        private readonly ConnectionString _connectionString;
        private readonly IMapper _mapper;
        public ActorRepository(IMapper mapper,IOptions<ConnectionString> options):base(options.Value.IMDBDB)
        {
            _mapper = mapper;
            _connectionString = options.Value;
           
        }
        public int AddActor(ActorRequest actor)
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
            1) ";
            return QueryPerson(query, actor);
            
        }
        public void UpdateActor(ActorRequest actor,int id)
        {
            var actordb = _mapper.Map<ActorDB>(actor);
            actordb.Id = id;
            var query= @"UPDATE Foundation.Persons
            SET    NAME = @Name,
                   bio = @Bio,
                   sex = @Sex,
                   dob = @Dob
            WHERE  id = @Id ";
            ExecuteQuery(query,actordb);
        }
        public void DeleteActor(int id)
        {
            var query = @"DELETE FROM Foundation.Persons
            WHERE  id = @id ";
            ExecuteQuery(query, new { id = id });
            
        }
        public ActorDB ActorById(int id)
        {
            var query = @"SELECT p.id Id,
           [sex],
           [bio],
           [dob],
           [name]   
            FROM   Foundation.Persons p(nolock)
                JOIN Foundation.Roles r(nolock)
                ON r.id = p.roleid
            WHERE  roleId=1
                AND p.id = @Id ";
            var item = Get(query,new { Id=id});
            return item;
        }
        public List<ActorDB> GetAllActors()
        {
            var query = @"SELECT p.id Id,
           [sex],
           [bio],
           [dob],
           [name]
            FROM   Foundation.Persons p(nolock)
                JOIN Foundation.Roles r(nolock)
                ON r.id = p.roleid
            WHERE  roleid = 1 ";
            var items=GetAll(query);
            return items.ToList();
        }
    }
}
