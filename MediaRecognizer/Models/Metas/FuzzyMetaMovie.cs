using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cyberinternauts.all.MediaRecognizer.Models.Metas
{
    [Keyless]
    public class FuzzyMetaMovie : MetaMovie
    {
        [Column("Distance")]
        public int LevenshteinDistance { get; set; }
    }
}
