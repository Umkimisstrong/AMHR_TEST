using Entity;
using System.Collections.Generic;

namespace Contract
{
    public class UserContract : GlobalContract
    {


        public string USER_ID { get; set; }

        public string USER_TYPE { get; set; }

        public UserEntity UserEntity { get; set; }

        public List<UserEntity> UserList { get; set; }


    }
}
