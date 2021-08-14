using System;
using System.IO;
using DevQuiz.Libraries.Core.Configurations;
using DevQuiz.Libraries.Core.Mappers;
using DevQuiz.TelegramBot.Configurations;
using DevQuiz.TelegramBot.Interfaces;
using DevQuiz.TelegramBot.Mappers;
using DevQuiz.TelegramBot.MediatR.Commands;
using DevQuiz.TelegramBot.MediatR.Handlers;
using DevQuiz.TelegramBot.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserDto = DevQuiz.Libraries.Core.Models.Dto.UserDto;

namespace DevQuiz.TelegramBot.Extensions
{
    /// <summary>
    /// Extensions for IServiceCollection instance
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddCustomOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DbConfiguration>(configuration.GetSection(nameof(DbConfiguration)));
            services.Configure<BotConfiguration>(configuration.GetSection(nameof(BotConfiguration)));

            return services;
        }

        internal static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(_ => { }).ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "DevQuiz.TelegramBot.xml"));
            });
            services.AddSwaggerGenNewtonsoftSupport();
            
            return services;
        }

        internal static IServiceCollection AddTelegramBotServices(this IServiceCollection services)
        {
            services.AddSingleton<IBotService, BotService>()
                .AddScoped<IBotMessageService, BotMessageService>()
                .AddScoped<IRequestHandler<StartCommand, Unit>, StartCommandHandler>();
            
            return services;
        }

        internal static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserMapperProfile>();
                config.AddProfile<QuestionMapperProfile>();
                config.AddProfile<UserBotMapperProfile>();
                config.AddProfile<QuestionsAdminApiMapperProfile>();
            });
            
            services.AddDevQuizMapperServices();

            return services;
        }
    }
}