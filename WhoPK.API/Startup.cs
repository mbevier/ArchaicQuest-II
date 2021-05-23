using WhoPK.API.Helpers;
using WhoPK.DataAccess;
using WhoPK.GameLogic.Character.Alignment;
using WhoPK.GameLogic.Character.AttackTypes;
using WhoPK.GameLogic.Character.Class;
using WhoPK.GameLogic.Character.Race;
using WhoPK.GameLogic.Character.Status;
using LiteDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using System.Threading;
using WhoPK.GameLogic.Commands;
using WhoPK.GameLogic.Commands.Movement;
using WhoPK.GameLogic.Core;
using WhoPK.GameLogic.Hubs;
using WhoPK.GameLogic.World.Room;
using Microsoft.AspNetCore.SignalR;
using static WhoPK.API.Services.services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using WhoPK.GameLogic.World.Area;
using System.Runtime.InteropServices.ComTypes;
using WhoPK.GameLogic.Core;
using Artemis;
using WhoPK.GameLogic.Core.System;

namespace WhoPK.API
{
    public class Startup
    {
        private IDataBase _db;
        private ICache _cache;
      
        private IClientMessenger _writeToClient;
        private IHubContext<GameHub> _hubContext;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("client",
                    builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:1337", "http://52.141.211.127:4200", "http://52.141.211.127", "52.141.211.127/:1")
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                options.AddPolicy("admin",
                    builder => builder.WithOrigins("http://52.141.211.177:4300")
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                options.AddPolicy("CorsPolicy",
                builder => builder
                //.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true) //for signalr cors                
        );
            });




            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // configure DI for application services
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddSingleton<LiteDatabase>(
                new LiteDatabase(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AQ.db")));
            services.AddScoped<IDataBase, DataBase>();
            services.AddSingleton<ICache>(new Cache());
            services.AddTransient<IVisual, Visual>();
            services.AddTransient<IInterpreter, Interpreter>();
            services.AddSingleton<ICommandManager, CommandManager>();
            services.AddSingleton<IGameLoop, GameLoop>();
            services.AddTransient<IRoomActions, RoomActions>();
            services.AddTransient<IAddRoom, AddRoom>();
            services.AddSingleton<IClientMessenger, WriteToClient>((factory) => new WriteToClient(_hubContext));
            services.AddSingleton<EntityWorld>();
            // Register the Swagger services
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataBase db, ICache cache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Play/Error");
            }
            _db = db;
            _cache = cache;
          
         
            app.UseStaticFiles();
            



            //    app.UseCors(
            //        options => options.WithOrigins("http://52.141.211.127:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            //    );

            //    app.UseCors(
            //    options => options.WithOrigins("52.141.211.127/:1").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            //);

            //app.UseCors(options => options.AllowAnyOrigin());
            app.UseCors("CorsPolicy");
            //app.UseCors("AllowOrigin");
            app.UseAuthentication();
            
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseSignalR(routes =>
            {
                routes.MapHub<GameHub>("/Hubs/game");

            });
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Play}/{action=Index}/{id?}");

            });

            _hubContext = app.ApplicationServices.GetService<IHubContext<GameHub>>();
            app.StartLoops();


            var rooms = _db.GetList<Room>(DataBase.Collections.Room);

            foreach (var room in rooms)
            {
                _cache.AddRoom(room.Id, room);
            }



            if (!_db.DoesCollectionExist(DataBase.Collections.Alignment))
            {
                foreach (var data in new Alignment().SeedData())
                {
                    _db.Save(data, DataBase.Collections.Alignment);
                }
            }

            if (!_db.DoesCollectionExist(DataBase.Collections.AttackType))
            {
                foreach (var data in new AttackTypes().SeedData())
                {
                    _db.Save(data, DataBase.Collections.AttackType);
                }
            }

            if (!_db.DoesCollectionExist(DataBase.Collections.Race))
            {
                foreach (var data in new Race().SeedData())
                {
                    _db.Save(data, DataBase.Collections.Race);
                }
            }

            if (!_db.DoesCollectionExist(DataBase.Collections.Status))
            {
                foreach (var data in new CharacterStatus().SeedData())
                {
                    _db.Save(data, DataBase.Collections.Status);
                }
            }

            if (!_db.DoesCollectionExist(DataBase.Collections.Class))
            {
                foreach (var data in new Class().SeedData())
                {
                    _db.Save(data, DataBase.Collections.Class);
                }
            }
          
        }
    }

    public static class Loops
    {
        public static void StartLoops(this IApplicationBuilder app)
        {
            var loop = app.ApplicationServices.GetRequiredService<IGameLoop>();
            
            Task.Run(loop.Start).ContinueWith((t) =>
            {
                if (t.IsFaulted) throw t.Exception;
            });

            //Task.Run(loop.UpdatePlayers);
        }
    }

}
