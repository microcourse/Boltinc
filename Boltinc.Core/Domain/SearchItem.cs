using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace Boltinc.Core.Domain
{
    public class SearchItem
    {

        public int Id { get; set; }

        [Required]
        public SearchEngineType SearchEngine { get; set; } = SearchEngineType.Google; //	SearchEngine  - Bing/Google

        [Required] public string Title { get; set; } // result title

        [Required] public DateTime EnteredDate { get; set; } = DateTime.UtcNow; //EnteredDate – date the value entered
    }
}
