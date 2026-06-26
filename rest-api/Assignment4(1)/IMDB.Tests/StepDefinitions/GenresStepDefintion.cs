using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using IMDBAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using IMDBAPI.Services;
using IMDBAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using IMDBAPI.Repository.Interfaces;
using IMDB.Tests.Mock;
using IMDBAPI.Controllers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using IMDBAPI.Models.ResponseModels;
using Moq;
using IMDBAPI.Models.RequestModels;
using IMDB.Tests.StepDefinitions;
using IMDBAPI.Helpers;

namespace IMDB.Tests
{
    [Scope(Feature = "Genres")]
    [Binding]
    public class GenreStepDefinitions : BaseSteps
    {
        public GenreStepDefinitions(CustomWebApplicationFactory factory) : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IGenreService, GenreService>();
                services.AddScoped(x => GenreRepositoryMock.GenreRepositoryMoq.Object);
            });
        }))
        { }

        [BeforeScenario]
        public void MockAll()
        {
            GenreRepositoryMock.MockAll();
        }
    }
}

