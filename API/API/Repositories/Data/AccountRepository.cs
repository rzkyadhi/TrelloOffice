using API.Context;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public LoginVM Post(string email, string password)
        {
            var data = myContext.TB_M_ROLEUSER.
                Include(x => x.User).
                Include(x => x.User.Employee).
                Include(x => x.Role).
                Where(x => x.User.Email == email).
                Where(x => x.User.Password == password).
                ToList();

            LoginVM login = new LoginVM();
            login.Id = data.FirstOrDefault().User.UserId;
            login.Email = data.FirstOrDefault().User.Email;
            login.Password = data.FirstOrDefault().User.Password;

            foreach (var item in data)
            {
                login.Role.Add(item.Role);
            }

            return login;
        }
    }
}
