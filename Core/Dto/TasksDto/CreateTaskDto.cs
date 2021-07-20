using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.TasksDto
{
    public class CreateTaskDto
    {
        public CreateTaskDto()
        {
        }

        public CreateTaskDto(string name, string description, long? taskGroupId = null, DateTime? leadTime = null)
        {
            this.Name = name;
            this.Description = description;
            this.TaskGroupId = taskGroupId;
            this.LeadTime = leadTime;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public long UserId { get; set; }

        public long? TaskGroupId { get; set; }

        public DateTime? LeadTime { get; set; }
    }
}
