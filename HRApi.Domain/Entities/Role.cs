using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }        

        public List<Worker> Workers { get; set; } = new();

        public Role(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}
