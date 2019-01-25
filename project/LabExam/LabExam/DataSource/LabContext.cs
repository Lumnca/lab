using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.Models;
using Microsoft.EntityFrameworkCore;

namespace LabExam.DataSource
{
    public class LabContext:DbContext
    {
        public LabContext(DbContextOptions<LabContext> options)
            : base(options)
        {
            
        }


        public  DbSet<Module> Modules { get; set; }

    }
}
