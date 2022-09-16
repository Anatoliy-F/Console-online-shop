using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class UserController : IUserController
    {
        /// <summary>
        /// An object that implements possible operations with User objects
        /// </summary>
        private readonly IUserRepo _userRepo;

        /// <summary>
        /// An object that provide session functions
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of UserController
        /// </summary>
        /// <param name="userRepo">An object that implements possible operations with User objects</param>
        /// <param name="session">An object that provide session functions</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public UserController(IUserRepo userRepo, ISession session, IActionResultFactory actionResultFactory)
        {
            _userRepo = userRepo;
            _session = session;
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult LogOut()
        {
            string name = _session.User.Name;
            _session.SetUser(new User());
            return _actionResultFactory.GetResultRender(
                ActionResult.Succes,
                $"Goodbye, dear {name}. Hope to see you soon!");
        }

        /// <inheritdoc/>
        public IActionResult Login(string userName, string password)
        {
            var user = _userRepo.FindByUserName(userName);
            if (user == null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    $"No such user [{userName}] registered for now\n\tPlease register");
            }

            if (VerifyHash(password, user.PasswordHash))
            {   
                _session.SetUser(user);
                return _actionResultFactory.GetResultRender(ActionResult.Succes, $"Hello, dear {userName}!");
            } 
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, "Incorect password");
            }  
        }

        /// <inheritdoc/>
        public IActionResult ChangeName(string userName, string password)
        {
            if (!VerifyHash(password, _session.User.PasswordHash)) {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Incorect password");
            }

            var user = _userRepo.FindByUserName(userName);

            if(user != null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Our shop already has user with name {userName} :( Please choose another name");
            }

            int id = _userRepo.UpdateByName(_session.User.Name, new User { Name = userName });
            _session.SetUser(_userRepo.Find(id));

            return _actionResultFactory.GetResultRender(ActionResult.Succes,
                "Your name is changed!");
        }

        /// <inheritdoc/>
        public IActionResult ChangeUserName(string userName, string newUserName)
        {
            try
            {
                _userRepo.UpdateByName(userName, new User { Name = newUserName });

                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "User name is changed");
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    ex.Message);
            }
        }

        /// <inheritdoc/>
        public IActionResult ChangeUserPassword(string userName, string password, string repeatPassword)
        {
            try
            {
                if (password == repeatPassword)
                {
                    string hash = GetHash(repeatPassword);
                    _userRepo.UpdateByName(userName, new User { PasswordHash = hash});
                    return _actionResultFactory.GetResultRender(ActionResult.Succes,
                        "User password is changed");
                } else
                {
                    return _actionResultFactory.GetResultRender(ActionResult.Warning, "You make a typo when repeat password");
                }
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                   ex.Message);
            }
        }

        /// <inheritdoc/>
        public IActionResult ShowAllUsers()
        {
            var users = _userRepo.GetAll();
            if(users.Any())
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                "All users", users);
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                "No users");
            }
            
        }

        /// <inheritdoc/>
        public IActionResult ShowUserById(int id)
        {
            User user = _userRepo.Find(id);

            if(user != null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "User profile", new[] { user });
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    $"No user with this Id: {id} registered in our shop");
            }
        }

        /// <inheritdoc/>
        public IActionResult ChangePassword(string password, string newPassword, string repeatNewPassword)
        {
            if (!VerifyHash(password, _session.User.PasswordHash))
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Incorect password");
            }

            if(newPassword != repeatNewPassword)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, "You make a typo when repeat your password");
            }

            try
            {
                string hash = GetHash(repeatNewPassword);
                _userRepo.Update(_session.User.Id, new User { PasswordHash = hash });

                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Your password is changed!");
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                   ex.Message);
            }

            
        }

        /// <inheritdoc/>
        public IActionResult RegisterAcount(string userName, string password, string repeatPassword)
        {   
            if(_userRepo.FindByUserName(userName) != null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Our shop already has user with name {userName} :( Please choose another name");
            }
            
            if(password.Length < 5)
            {
                 return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Dear {userName} you password less than 5 symbols:( Please choose stronger password");
            }

            if(password != repeatPassword)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Dear {userName} you make a typo when repeat your password, please try again");
            }

            string hash = GetHash(repeatPassword);

            _userRepo.Add( new User
                    {
                        Name = userName,
                        Role = UserRole.RegisteredUser,
                        PasswordHash = hash,
                    });

            return _actionResultFactory.GetResultRender(ActionResult.Succes, $"Thank you, dear {userName}! Login now, than you can feel all power of this E-Shop :)");
        }

        /// <summary>
        /// Hashes the string
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>Hashed string</returns>
        private string GetHash(string input)
        {
            using SHA256 hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Verify hash
        /// </summary>
        /// <param name="input">String</param>
        /// <param name="hash">Hashed string</param>
        /// <returns>true, if the password matches the hashed one </returns>
        private bool VerifyHash(string input, string hash)
        {
            var hashInput = GetHash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashInput, hash) == 0;
        }
    }
}
