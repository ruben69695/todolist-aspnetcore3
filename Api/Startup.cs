using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using Data.Repositories.Interfaces;
using Data.Repositories.Mongo;
using Data;
using Services.Interfaces;
using Services;
using Data.Helpers.Mongo;
using Microsoft.Extensions.Options;

namespace todoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection("TodoDatabaseSettings"));

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../App/dist";
            });
            services.AddServicesForInjection();
            services
                .AddAuthentication(options => {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(options => {
                    options.ClientId = Configuration["Google:ClientId"];
                    options.ClientSecret = Configuration["Google:ClientSecret"];
                    options.CallbackPath = new PathString("/signin-google");
                })
                .AddMicrosoftAccount(options => {
                    options.ClientId = Configuration["Microsoft:ClientId"];
                    options.ClientSecret = Configuration["Microsoft:ClientSecret"];
                    options.CallbackPath = new PathString("/signin-microsoft");
                })
                .AddOAuth("GitHub", options => {
                    options.ClientId = Configuration["GitHub:ClientId"];
                    options.ClientSecret = Configuration["GitHub:ClientSecret"];

                    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    options.UserInformationEndpoint = "https://api.github.com/user";
                    options.CallbackPath = new PathString("/signin-github");

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey("urn.github.login", "login");
                    options.ClaimActions.MapJsonKey("urn.github.url", "html_url");
                    options.ClaimActions.MapJsonKey("urn.github.avatar", "avatar_url");

                    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
                    {
                        OnCreatingTicket = async context => {
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            context.RunClaimActions(user.RootElement);
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "../App";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddServicesForInjection(this IServiceCollection services) {
            return services
                .AddTransient<MongoContext>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ITodoRepository, TodoRepository>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ITodoService, TodoService>()
                .AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }
    }
}
