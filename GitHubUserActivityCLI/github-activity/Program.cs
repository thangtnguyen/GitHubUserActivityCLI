// See https://aka.ms/new-console-template for more information
using github_activity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Models.Interfaces;
using Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<GitHubInfoOption>(context.Configuration.GetSection(nameof(GitHubInfoOption)));
        services.AddHttpClient<IGithubService, GithubService>();
        services.AddSingleton<IApplication, Application>();
    }).Build();

var app = host.Services.GetService<IApplication>();
app!.HandleBusiness(args);