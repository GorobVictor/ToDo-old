using Core.Dto.Tasks;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Service
{
    public interface ITaskService
    {
        List<Tasks> GetAllTasks(long userId);

        Task<Tasks> AddTaskAsync(CreateTaskDto task);

        Task UpdateStatus(long taskId, bool status);

        Task UpdateName(long taskId, string name);

        Task<bool> Delete(long taskId);

        Task<bool> Delete(List<long> taskId);
    }
}
