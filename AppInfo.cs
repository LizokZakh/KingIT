using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingIT
{
    static class AppInfo
    {
        public static int CurrentUserId { get; set; }
        public static int GetCurrentUser() => CurrentUserId;
        public static void SetEmployee(int employee) => CurrentUserId = employee;
    }
}
