using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class UserList
    {
        public List<User> Users { get; set; } = new List<User>();
    }
}
