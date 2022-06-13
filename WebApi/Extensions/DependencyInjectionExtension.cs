using System;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<UserManager<UserCredentials>>();
            services.AddScoped<SignInManager<UserCredentials>>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<AbstractValidator<TopicModel>, TopicValidator>();
            services.AddScoped<AbstractValidator<ResponseModel>, ResponseValidator>();
            services.AddScoped<AbstractValidator<CommentModel>, CommentValidator>();
            services.AddScoped<AbstractValidator<UserModel>, UserValidator>();
        }
    }
}
