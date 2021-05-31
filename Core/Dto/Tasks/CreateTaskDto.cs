using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Dto.Tasks
{
    public class CreateTaskDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }
    }
}
