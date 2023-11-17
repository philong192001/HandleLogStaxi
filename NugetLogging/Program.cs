IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices((context, services) =>
{
   var appSetting = AppSetting.MapValue(context.Configuration);
   services.AddSingleton(appSetting);
   services.AddKafkaLogger(appSetting);
   services.AddRetryPolicy();
   services.AddDisruptor();
   services.AddSecuritiesStorageContext(appSetting.ConnectionStrings.Securities, appSetting.SecuritiesDBDefaultTableSchema);
   services.AddRedis();
   services.AddKafkaConsumers(builder =>
   {
       builder.AddConsumer<SecuritiesConsumingTask>(Constants.SecuritiesSettingId);
   });
})
.Build();

await host.RunAsync();