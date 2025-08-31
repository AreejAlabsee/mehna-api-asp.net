using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MehnahFinalMVC.ViewModels
{
    public class WorksViewModel
    {
            [Key]
            public int Id { get; set; }


            [StringLength(1000)]
            public string Description { get; set; }

            [Column(TypeName = "decimal(10, 2)")]
            public decimal? Price { get; set; }

            [StringLength(50)]
            public string Category { get; set; }

            public string WorkImages { get; set; }
        public string WorkImagesUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedDate { get; set; }

            [Required]
            public int UserId { get; set; }

            public bool IsAvailable { get; set; } = true;

            [StringLength(50)]
            public string ContactMethod { get; set; }




        }
    }


