using Confluent.Kafka;

namespace KafkaProducerApi
{
    public class Startup
    {
        public IConfiguration configRoot {get;}
        private IProducer<string, string> _producer;
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRazorPages();
            //dependecy Injection service for producer
            services.AddSingleton<IProducer<string, string>>(provider => _producer);
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Initialize the Kafka producer configuration
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092", // Update this with your Kafka broker address
                ClientId = "KafkaProducerApi"
            };
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }

    }
}
