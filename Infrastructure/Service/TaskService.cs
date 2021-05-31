using AutoMapper;
using Core.Dto.Tasks;
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
    }
}
