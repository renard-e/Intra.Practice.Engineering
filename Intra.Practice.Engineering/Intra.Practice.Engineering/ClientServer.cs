using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intra.Practice.Engineering
{
    public enum Group : int
    {
        ADMIN = 0,
        BASICUSER,
        UNDEFINED
    };

    public class ClientServer
    {
        private String      _email;
        private Group       _group;

        public ClientServer()
        {
            this._email = "";
            this._group = Group.UNDEFINED;
        }

        public void setEmail(String newEmail)
        {
            this._email = newEmail;
        }

        public void setGroup(int newGroup)
        {
            this._group = (Group)newGroup;
        }

        public String getEmail()
        {
            return (this._email);
        }


    }
}
