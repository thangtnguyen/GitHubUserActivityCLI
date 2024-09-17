// See https://aka.ms/new-console-template for more information
using github_activity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Interfaces;
using Services;

Console.WriteLine("Hello, World!");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //services.Configure<FileDataSourceOptions>(context.Configuration.GetSection(nameof(FileDataSourceOptions)));
        services.AddHttpClient<IGithubService, GithubService>();
        services.AddSingleton<IApplication, Application>();
    }).Build();

var app = host.Services.GetService<IApplication>();
app!.HandleBusiness(args);