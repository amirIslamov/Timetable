using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model.Profile.Roles
{
    public class RoleSet
    {
        private int _roleBitSet;

        public IEnumerable<Role> Roles => EnumerateRoles();

        protected IEnumerable<Role> EnumerateRoles()
        {
            return Enum
                .GetValues<Role>()
                .Cast<int>()
                .Where(i => ((_roleBitSet >> i) & 1) == 1)
                .Cast<Role>();
        }

        public void AddRole(Role role)
        {
            var roleToAdd = 1 << (int) role;
            _roleBitSet = _roleBitSet | roleToAdd;
        }

        public void RemoveRole(Role role)
        {
            var notRole = ~0 ^ (1 << (int) role);
            _roleBitSet = _roleBitSet & notRole;
        }

        public bool ContainsRole(Role role)
        {
            return ((_roleBitSet >> (int) role) & 1) == 1;
        }
    }
}