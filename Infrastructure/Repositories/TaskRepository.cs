using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class TaskRepository : BaseRepository<Tasks>, ITaskRepository
    {
        public TaskRepository(ToDoContext context)
               : base(context)
        {

        }
    }
}
