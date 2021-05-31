using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUpdatedEntity
    {
        DateTime? UpdatedAt { get; set; }

        long? UpdatedBy { get; set; }
    }
}
