using Online_Shopping.Models.AdminData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models.AdminViewModels.AdminPages
{
    public class AdminPagesVm
    {
        public AdminPagesVm()
        {

        }

        public AdminPagesVm(PageDTO row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Body { get; set; }
        public int Sorting { get; set; }
        [DisplayName("Page Side Bar")]
        public bool HasSidebar { get; set; }
    }

}
