using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Entities
{
    public interface IBaseEntity
    {
        public DateTime CreateDate { get; set; } 
    }
}
