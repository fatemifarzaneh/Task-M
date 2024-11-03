using UserManagement.DAL;
using UserManagement.Entity;

namespace UserManagement.BLL
{
    public class UserService
    {
        private readonly DataAccessLayer dataAccessLayer;

        public UserService()
        {
            dataAccessLayer = new DataAccessLayer();
        }

        public string RegisterUser(string username, string password)
        {
            User existingUser = dataAccessLayer.GetUserByUsername(username);
            if (existingUser != null)
            {
                return "Register failed! Username already exists.";
            }

            User newUser = new User
            {
                Username = username,
                Password = password,
                Status = "available" // وضعیت پیش‌فرض 
            };

            bool isRegistered = dataAccessLayer.AddUser(newUser);
            return isRegistered ? "Register successful!" : "Register failed due to an internal error.";
        }
        public string LoginUser(string username, string password)
        {
            User user = dataAccessLayer.GetUserByUsername(username);
            if (user == null)
            {
                return "Login failed! Username does not exist.";
            }

            if (user.Password != password)
            {
                return "Login failed! Incorrect password.";
            }
            return "Login successful!";
        }
        public string ChangeUserStatus(string username, string newStatus)
        {
            User user = dataAccessLayer.GetUserByUsername(username);
            if (user == null)
            {
                return "Change status failed! User not found.";
            }

            user.Status = newStatus;
            bool isUpdated = dataAccessLayer.UpdateUserStatus(user);
            return isUpdated ? "Status updated successfully!" : "Failed to update status.";
        }
        public List<User> SearchUsersByUsername(string partialUsername)
        {
            return dataAccessLayer.SearchUsersByUsername(partialUsername);
        }
        public string ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = dataAccessLayer.GetUserByUsername(username);
            if (user == null)
            {
                return "Change password failed! User not found.";
            }

            if (user.Password != oldPassword)
            {
                return "Change password failed! Incorrect old password.";
            }

            user.Password = newPassword;
            bool isUpdated = dataAccessLayer.ChangeUserPassword(user.Username, oldPassword, newPassword);
            return isUpdated ? "Password changed successfully!" : "Failed to change password.";
        }
        public string LogoutUser(string username)
        {
            return "Logout successful!";
        }
    }

}
