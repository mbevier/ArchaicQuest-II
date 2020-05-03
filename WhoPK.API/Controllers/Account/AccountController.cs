using WhoPK.DataAccess;
using WhoPK.GameLogic.Account;
using WhoPK.GameLogic.Character;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WhoPK.API.Entities;
using WhoPK.API.Services;
using WhoPK.DataAccess.DataModels;
using Newtonsoft.Json;
using Account = WhoPK.GameLogic.Account.Account;
using AccountStats = WhoPK.GameLogic.Account.AccountStats;
using static WhoPK.API.Services.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WhoPK.API.Controllers
{
    public class AccountController : Controller
    {
        private IDataBase _db { get; }
        private IAdminUserService _adminUserService;
        public AccountController(IDataBase db, IAdminUserService adminUser)
        {
            _db = db;
            _adminUserService = adminUser;
        }

        [HttpPost]
        [Route("api/Account")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Account account)
        {

            if (!ModelState.IsValid)
            {
                var exception = new Exception("Invalid Account details");
                throw exception;
            }

            var hasEmail = _db.GetCollection<Account>(DataBase.Collections.Account).FindOne(x => x.Email.Equals(account.Email));

            if (hasEmail != null)
            {
                return BadRequest("An account with that email address already exists.");
            }

            var data = new Account()
            {
                UserName = account.UserName,
                Id = Guid.NewGuid(),
                Characters = new List<Guid>(),
                Credits = 0,
                Email = account.Email,
                EmailVerified = false,
                Password = BCrypt.Net.BCrypt.HashPassword(account.Password), //BCrypt.Verify("my password", passwordHash);
                Stats = new AccountStats(),
            };

            var saved = _db.Save(data, DataBase.Collections.Account);

            return saved ? (IActionResult)Ok(data.Id) : BadRequest("Error saving account");
        }

        [HttpPost]
        [Route("api/Account/Login")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] Login login)
        {

            if (!ModelState.IsValid)
            {
                var exception = new Exception("Invalid login details");
                throw exception;
            }

            var user = _db.GetCollection<Account>(DataBase.Collections.Account).FindOne(x => x.Email.Equals(login.Username));

            if (user == null)
            {
                return BadRequest("Sorry that account does not exist.");
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return BadRequest("Password is not correct.");
            }      

            return Ok(user.Id);
            
        }

        [HttpGet]
        [Route("api/Account/Profile/{id}")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        public IActionResult GetProfile(string id)
        {

            if (!ModelState.IsValid)
            {
                var exception = new Exception("Invalid request");
                throw exception;
            }
            var accounts = _db.GetCollection<Account>(DataBase.Collections.Account).FindAll();
            var user = _db.GetCollection<Account>(DataBase.Collections.Account).FindOne(x => x.Id.ToString().Equals(id));

            if (user == null)
            {
                return BadRequest("Sorry that account does not exist.");
            }

            var characters = _db.GetCollection<Player>(DataBase.Collections.Players)
                .Find(x => x.AccountId.Equals(user.Id));

            var profile = new AccountViewModel()
            {
                Characters = characters.ToList(),
                Credits = 0,
                DateJoined = user.DateJoined,
                Stats = new AccountStats()
                {
                    
                }
            };

           
            return Ok(profile);

        }


        //https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
        [HttpPost("api/Account/authenticate")]
        [ProducesResponseType(typeof(AdminUser), StatusCodes.Status200OK)]
        public IActionResult Authenticate([FromBody]AdminUser userParam)
        {
            var user = _adminUserService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize]
        [HttpGet("api/Account/getusers")]
        public IActionResult GetAll()
        {
            var users = _adminUserService.GetAll();
            return Ok(users);
        }
    }



}
