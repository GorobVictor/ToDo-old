using AutoMapper;
using Core.Dto.TasksDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class TaskService : ITaskService
    {
        ITaskRepository taskRepo { get; set; }
        IMapper mapper { get; set; }

        public TaskService(
            ITaskRepository taskRepo,
            IMapper mapper
            )
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
        }

        public List<Tasks> GetAllTasks(long userId)
        {
            return this.taskRepo.Get(x => x.UserId == userId).ToList();
        }

        public async Task<Tasks> AddTaskAsync(CreateTaskDto task)
        {
            return await this.taskRepo.AddAsync(this.mapper.Map<Tasks>(task));
        }

        public async Task UpdateStatus(long taskId, bool status)
        {
            var task = await this.taskRepo.GetFirstAsync(x => x.Id == taskId);

            if (status)
            {
                task.Status = status;
                task.ClosingTime = DateTime.Now;
            }
            else
            {
                task.Status = status;
                task.ClosingTime = null;
            }

            await this.taskRepo.UpdateAsync(task);
        }

        public async Task UpdateFavorite(long taskId, bool favorite)
        {
            var task = await this.taskRepo.GetFirstAsync(x => x.Id == taskId);

            task.Favorite = favorite;

            await this.taskRepo.UpdateAsync(task);
        }

        public async Task UpdateName(long taskId, string name)
        {
            var task = await this.taskRepo.GetFirstAsync(x => x.Id == taskId);

            task.Name = name;

            await this.taskRepo.UpdateAsync(task);
        }

        public async Task<bool> Delete(long taskId)
        {
            return await this.taskRepo.DeleteAsync(taskId);
        }

        public async Task<bool> Delete(List<long> taskId)
        {
            return await this.taskRepo.DeleteAsync(taskId);
        }
    }
}
