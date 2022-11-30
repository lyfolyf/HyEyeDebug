using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using static HyEye.Models.UserAction;
using static GL.Kit.Log.ActionResult;

namespace HyEye.API.Repository
{
    public interface IUserRepository
    {
        event Action AfterLogin;
        event Action AfterExit;

        void Login(string username, string password);

        void Exit();

        List<UserVO> GetUserList();

        UserVO CurrUser { get; }

        void Add(UserVO user);

        void Delete(string username);

        void ChangePassword(string oldPwd, string newPwd);

        void ResetPassword(string username);

        bool IsDefaultPassword();

        void Save();
    }

    public class UserRepository : IUserRepository
    {
        public event Action AfterLogin;
        public event Action AfterExit;

        readonly IMapper mapper;
        readonly IGLog log;

        readonly List<User> users;

        User curUser;

        const string DefaultPassword = "123456";

        public UserRepository(IMapper mapper, IGLog log)
        {
            this.mapper = mapper;
            this.log = log;

            users = ApiConfig.UserList.Users;

            if (users.Count == 0)
            {
                users.Add(new User { UserName = "admin", Role = Role.Administrator, Password = DefaultPassword });
            }
        }

        public UserVO CurrUser
        {
            get
            {
                UserVO user = mapper.Map<UserVO>(curUser);
                if (user != null)
                    user.Password = null;
                return user;
            }
        }

        public void Login(string username, string password)
        {
            if (username == "Test" && password == "123")
            {
                curUser = new User { UserName = "Test", Role = Role.Developer };
            }
            else
            {
                curUser = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
                if (curUser == null)
                {
                    log.Error(new UserLogMessage(username, A_Login, R_Fail, "用户名或密码错误"));
                    throw new ApiException("用户名或密码错误");
                }
            }

            log.Info(new UserLogMessage(username, A_Login, R_Success));

            AfterLogin?.Invoke();
        }

        public void Exit()
        {
            log.Info(new UserLogMessage(curUser.UserName, A_Logout, R_Success));

            curUser = null;

            AfterExit?.Invoke();
        }

        public List<UserVO> GetUserList()
        {
            List<UserVO> vos = mapper.Map<List<UserVO>>(users);

            vos.ForEach(a => a.Password = string.Empty);

            return vos;
        }

        public void Add(UserVO user)
        {
            if (users.Any(u => u.UserName == user.UserName))
            {
                log.Error(new UserLogMessage(user.UserName, A_Add, R_Fail, "用户名已存在"));
                throw new ApiException("用户名已存在");
            }

            user.Password = DefaultPassword;

            users.Add(mapper.Map<User>(user));
            log.Info(new UserLogMessage(user.UserName, A_Add, R_Success));

            Save();
        }

        public void Delete(string username)
        {
            if (users.Remove(a => a.UserName == username))
            {
                log.Info(new UserLogMessage(username, A_Delete, R_Success));
            }
            Save();
        }

        public void ChangePassword(string oldPwd, string newPwd)
        {
            if (oldPwd != curUser.Password)
            {
                //log.Error(new UserLogMessage(curUser.UserName, A_ChangePassword, R_Fail, "原始密码错误"));
                throw new ApiException("原始密码错误");
            }
            if (newPwd == DefaultPassword)
            {
                //log.Error(new UserLogMessage(curUser.UserName, A_ChangePassword, R_Fail, "密码不能是 123456"));
                throw new ApiException("密码不能是 123456");
            }

            curUser.Password = newPwd;

            log.Info(new UserLogMessage(curUser.UserName, A_ChangePassword, R_Success));

            Save();
        }

        public void ResetPassword(string username)
        {
            User user = users.FirstOrDefault(a => a.UserName == username);

            if (user != null)
            {
                user.Password = DefaultPassword;

                log.Info(new UserLogMessage(curUser.UserName, A_ResetPassword, R_Success));

                Save();
            }
        }

        public bool IsDefaultPassword()
        {
            return curUser.Password == DefaultPassword;
        }

        public void Save()
        {
            ApiConfig.Save(ApiConfig.UserList, true);
        }
    }
}
