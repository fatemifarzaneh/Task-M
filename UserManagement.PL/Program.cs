using UserManagement.BLL;
using UserManagement.Entity;

namespace UserManagement.PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            bool isLoggedIn = false;
            string currentUser = "";

            while (true)
            {
                Console.Write("\nEnter command: ");
                string input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No command entered. Please try again.");
                    continue;
                }

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                switch (command)
                {
                    case "register":
                        if (parts.Length >= 5 && parts[1] == "--username" && parts[3] == "--password")
                        {
                            string username = parts[2];
                            string password = parts[4];
                            string result = userService.RegisterUser(username, password);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Invalid register command format. Expected: register --username [username] --password [password]");
                        }
                        break;

                    case "login":
                        if (parts.Length >= 5 && parts[1] == "--username" && parts[3] == "--password")
                        {
                            string username = parts[2];
                            string password = parts[4];
                            string result = userService.LoginUser(username, password);
                            Console.WriteLine(result);
                            if (result.Contains("successful"))
                            {
                                isLoggedIn = true;
                                currentUser = username;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid login command format. Expected: login --username [username] --password [password]");
                        }
                        break;

                    case "change":
                        if (isLoggedIn && parts.Length >= 3 && parts[1] == "--status")
                        {
                            string status = parts[2];
                            string result = userService.ChangeUserStatus(currentUser, status);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Change status failed! You must be logged in and use: change --status [available|not available]");
                        }
                        break;

                    case "search":
                        if (parts.Length >= 3 && parts[1] == "--username")
                        {
                            string partialUsername = parts[2];
                            List<User> users = userService.SearchUsersByUsername(partialUsername);
                            if (users.Count > 0)
                            {
                                int i = 1;
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"{i}- {user.Username} | Status: {user.Status}");
                                    i++;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No users found with the given username prefix.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid search command format. Expected: search --username [partialUsername]");
                        }
                        break;

                    case "changepassword":
                        if (isLoggedIn && parts.Length >= 5 && parts[1] == "--old" && parts[3] == "--new")
                        {
                            string oldPassword = parts[2];
                            string newPassword = parts[4];
                            string result = userService.ChangePassword(currentUser, oldPassword, newPassword);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Change password failed! You must be logged in and use: changepassword --old [myOldPassword] --new [myNewPassword]");
                        }
                        break;

                    case "logout":
                        if (isLoggedIn)
                        {
                            isLoggedIn = false;
                            currentUser = "";
                            Console.WriteLine("Logout successful!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Logout failed! You are not logged in.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command. Please check the command format and try again.");
                        break;
                }
            }
        }
    }
}




