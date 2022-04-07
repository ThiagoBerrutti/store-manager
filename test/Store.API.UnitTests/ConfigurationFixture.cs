using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAPI.Mapper;
using System.IO;

namespace Store.API.UnitTests
{
    public class ConfigurationFixture
    {
        public IConfigurationRoot Configuration { get; private set; }
        public ServiceProvider ServiceProvider { get; private set; }
        public IMapper Mapper { get; private set; }

        public ConfigurationFixture()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Testing.json")
                .Build();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            var mapper = new Mapper(config);

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IMapper>(mapper);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            Configuration = configuration;
            Mapper = mapper;

        }
    }
}