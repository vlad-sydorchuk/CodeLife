using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLife.Model
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descritption { get; set; }
        public DateTime? Deadline { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Developer")]
        public string DeveloperId { get; set; }
        public ApplicationUser Developer { get; set; }

        [ForeignKey("Tester")]
        public string TesterId { get; set; }
        public ApplicationUser Tester { get; set; }

        [ForeignKey("Reviwer")]
        public string ReviwerId { get; set; }
        public ApplicationUser Reviwer { get; set; }

        public ProjectTaskStatus Status { get; set; }

        //public SelectList ProjectTaskStatuses { get; set; }

        public ProjectTask()
        {

            //ProjectTaskStatuses = (SelectList)(Enum.GetValues(typeof(ProjectTaskStatus)).Cast<ProjectTaskStatus>().Select(e => new { Value = e.ToString(), Text = e.ToString() }).ToList()

            //return new SelectList(enumValues, "Value", "Text", "");
        }
    }

    public enum ProjectTaskStatus
    {
        Draft = 1,
        Backlog = 2,
        InDevelopment = 3,
        InCodeReview = 4,
        InVerification = 5,
        Done = 6
    }

    public class ProjectTaskStatusAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
