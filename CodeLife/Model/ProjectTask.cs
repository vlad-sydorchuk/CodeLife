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
    }

    public enum ProjectTaskStatus
    {
        Draft,
        Backlog,
        InDevelopment,
        InCodeReview,
        InVerification,
        Done
    }
}
