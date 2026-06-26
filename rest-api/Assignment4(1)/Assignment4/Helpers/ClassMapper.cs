using AutoMapper;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;

namespace IMDBAPI.Helpers
{
    public class ClassMapper:Profile
    {
        public ClassMapper()
        {
            CreateMap<ActorRequest, ActorDB>(MemberList.Source);
            CreateMap<ActorResponse,ActorDB>().ReverseMap();
            CreateMap<MovieRequest, MovieDB>(MemberList.Source);
            CreateMap<MovieDB, MovieResponse>(MemberList.Source);
            CreateMap<GenreRequest, GenreDB>(MemberList.Source);
            CreateMap<GenreResponse, GenreDB>().ReverseMap();
            CreateMap<ProducerRequest, ProducerDB>(MemberList.Source);
            CreateMap<ProducerResponse, ProducerDB>().ReverseMap();
            CreateMap<ReviewRequest, ReviewDB>(MemberList.Source);
            CreateMap<ReviewDB, ReviewResponse>();
            CreateMap<UserRequest, UserDB>();
        }
    }
}
