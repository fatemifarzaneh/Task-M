namespace UserManagement.Entity
{
    public class User
    {
        public int Id { get; set; } // شناسه منحصر به فرد برای کاربر
        public string Username { get; set; } // نام کاربری
        public string Password { get; set; } // رمز عبور
        public string Status { get; set; } // وضعیت (مثل "available" یا "not available")

        // متدهای اختیاری برای عملیات خاص
        public void ChangeStatus(string newStatus)
        {
            Status = newStatus;
        }
    }

}
