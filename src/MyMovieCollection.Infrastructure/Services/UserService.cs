using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUserMovieRepository userMovieRepository;

        public UserService(UserManager<User> userManager, IUserMovieRepository userMovieRepository)
        {
            this.userManager = userManager;
            this.userMovieRepository = userMovieRepository;
        }

        public async Task DeleteUserAsync(string id)
        {
            var userToDelete = await this.userManager.FindByIdAsync(id);  

            if (userToDelete is null) throw new NullReferenceException();

            await this.userMovieRepository.DeleteAllForUserAsync(id);
            await this.userManager.DeleteAsync(userToDelete);
        }

        public async Task<User> GetUserByIdAsync(string id) {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            var user = await this.userManager.FindByIdAsync(id);
            if (user is null) throw new NullReferenceException();
            return user;
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