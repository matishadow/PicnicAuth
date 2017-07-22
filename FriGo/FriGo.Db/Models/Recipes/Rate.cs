using FriGo.Db.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriGo.Db.Models.Recipes
{
    public class Rate : OwnedEntity, IComparable<Rate>
    {
        public int CompareTo(Rate other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Rating.CompareTo(other.Rating);
        }

        public int Rating { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
