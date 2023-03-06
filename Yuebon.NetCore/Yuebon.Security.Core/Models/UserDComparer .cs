using System;
using System.Collections.Generic;

namespace Yuebon.Security.Models
{
    class UserDComparer : IEqualityComparer<UserD>
    {
        public bool Equals(UserD x, UserD y) {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(UserD obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            return obj.Id == null ? 0 :obj.Id.GetHashCode();
        }
    }
}
