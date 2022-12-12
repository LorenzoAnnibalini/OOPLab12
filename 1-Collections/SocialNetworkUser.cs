using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private IDictionary<string,ISet<TUser>> _utentiSeguiti = new Dictionary<string,ISet<TUser>>();
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
        }

        public bool AddFollowedUser(string group, TUser user)
        {   
            if(_utentiSeguiti.ContainsKey(group)){
                var tmplist = _utentiSeguiti[group];
                return tmplist.Add(user);
            }else{
                var tmplist = new HashSet<TUser>();
                tmplist.Add(user);
                _utentiSeguiti[group] = tmplist;
                return true;
            }
            
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var listaFollower = new List<TUser>();
                foreach(var group in _utentiSeguiti.Values){
                    listaFollower.AddRange(group);
                }
                return listaFollower;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {   
            if(_utentiSeguiti.ContainsKey(group)) return new HashSet<TUser>(_utentiSeguiti[group]);
            return new HashSet<TUser>();
        }
    }
}
