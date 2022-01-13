using FoodAPI.Assets.Contain;
using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodAPI.Models.DAO
{
    public class UserDAO
    {
        private static UserDAO instance;

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        FoodAppDbEntities db = new FoodAppDbEntities();

        public async Task<List<UserDTO>> GetAllUser()
        {
            var UserList = (await db.Users
                        .ToListAsync())
                        .Select(user => new UserDTO(user))
                        .ToList();
            return UserList;
        }

        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            var userName = userDTO.Email;
            var passWord = userDTO.Password;

            passWord = Const.CreateMD5(passWord);

            try
            {
                var myUser = await db.Users.SingleOrDefaultAsync(user => user.Email == userName && user.Password == passWord);
                if (myUser != null)
                {
                    return new UserDTO(myUser);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<int> Register(UserDTO userDTO)
        {
            try
            {
                var userWithSameEmail = await db.Users.SingleOrDefaultAsync(u => u.Email == userDTO.Email);

                if (userWithSameEmail != null) return -1;
                string passWord = Const.CreateMD5(userDTO.Password);

                User user = new User()
                {
                    Name = userDTO.Name,
                    Password = passWord,
                    Email = userDTO.Email,
                    Role = "User"

                };

                user.Avatar = "default.png";

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return user.Id;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var result = db.Users.SingleOrDefault(c => c.Id == userDTO.Id);
            try
            {
                if (!string.IsNullOrWhiteSpace(userDTO.Name))
                    result.Name = userDTO.Name;
                if (!string.IsNullOrWhiteSpace(userDTO.Password))
                    result.Password = Const.CreateMD5(userDTO.Password);
                if (!string.IsNullOrWhiteSpace(userDTO.Email))
                    result.Email = userDTO.Email;
                if (!string.IsNullOrWhiteSpace(userDTO.Avatar))
                    result.Avatar = userDTO.Avatar;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<UserDTO> GetUserByID(int ID)
        {
            try
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Id == ID);
                if (user != null)
                {
                    return new UserDTO(user);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
    }
}