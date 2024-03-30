using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUserMovieRepository userMovieRepository;
        private readonly IUserUserRepository userUserRepository;
        private readonly IWatchListRepository watchListRepository;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserService(IUserMovieRepository userMovieRepository, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IWatchListRepository watchListRepository, IUserUserRepository userUserRepository)
        {
            this.userManager = userManager;
            this.userMovieRepository = userMovieRepository;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.watchListRepository = watchListRepository;
            this.userUserRepository = userUserRepository;
        }

        public async Task DeleteUserAsync(string id)
        {
            var userToDelete = await this.userManager.FindByIdAsync(id);  

            if (userToDelete is null) throw new NullReferenceException("There is no user with this Id");

            await this.userMovieRepository.DeleteAllForUserAsync(id);
            await this.watchListRepository.DeleteAllForUserAsync(id);
            await this.userUserRepository.DeleteUserInFollowersAndFollowed(id);
            await this.userManager.DeleteAsync(userToDelete);
        }

        public async Task<User> GetUserByIdAsync(string id) {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            var user = await this.userManager.FindByIdAsync(id);
            if (user is null) throw new NullReferenceException("There is no user with this Id");
            return user;
        }

        public async Task LoginAsync(string userName, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new NullReferenceException("Incorrect Login");
            }

            var result = await this.signInManager.PasswordSignInAsync(user, password, true, true);

            if (!result.Succeeded) 
            {
                throw new ArgumentException("Incorrect Password");
            }
        }

        public async Task SignOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task SignupAsync(User user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);

            if(!result.Succeeded) 
            {
                var exceptions = new List<Exception>();

                foreach (var error in result.Errors)
                {
                    exceptions.Add(new ArgumentException(error.Description, error.Code));
                }

                throw new AggregateException(exceptions);
            }

            // var role = new IdentityRole {Name = "Admin"};
            // await roleManager.CreateAsync(role);
            // await userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task UpdateUserAsync(User user)
        {
            var result = await this.userManager.UpdateAsync(user);;

            if (!result.Succeeded)
            {
                var exceptions = new List<Exception>();

                foreach (var error in result.Errors)
                {
                    exceptions.Add(new ArgumentException(error.Description, error.Code));
                }

                throw new AggregateException(exceptions);
            }

            await this.signInManager.RefreshSignInAsync(user);
        }

        public async Task<FileStream> UploadProfilePicture(string fileName, User user)
        {
            var userId = user?.Id;
            var fileExtension = new FileInfo(fileName).Extension;
            var fileFullName = $"{userId}{fileExtension}";

            var files = Directory.GetFiles("wwwroot/ProfilePictures/", userId + ".*");

            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }
            
            user!.ProfilePicture = $"/ProfilePictures/{fileFullName}";
            await this.userManager.UpdateAsync(user);

            return System.IO.File.Create($"wwwroot/ProfilePictures/{fileFullName}");
        }
    }
}