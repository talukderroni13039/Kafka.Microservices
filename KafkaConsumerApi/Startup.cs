using Confluent.Kafka;

namespace KafkaConsumerApi
{
    public class Startup
    {
        public IConfiguration configRoot {get;}
        private  IConsumer<string, string> _consumer;
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
            //dependecy Injection service for Consumer
            services.AddSingleton<IConsumer<string, string>>(provider => _consumer);
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Initialize the Kafka producer configuration
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", // Update this with your Kafka broker address
                GroupId = "KafkaConsumerApi",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _consumer.Subscribe("order-topic"); // Subscribe to the topic

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
