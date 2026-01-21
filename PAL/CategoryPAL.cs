using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class CategoryPAL
    {
        #region properties

        // Sql Type: int
        public int ID { get; set; }

        // Sql Type: varchar
        public string Name { get; set; }

        // Sql Type: varchar
        public string Description { get; set; }

        // Sql Type: bit
        public bool Status { get; set; }

        // Sql Type: datetime
        public DateTime CreatedOn { get; set; }

        public string ImageName { get; set; }

        public string CategoryMetaTitle { get; set; }

        public string CategoryMetaKeyword { get; set; }
        
        public string CategoryMetaDescription { get; set; }
        public string ParentID { get; set; }
        public int CLT_Version { get; set; }
        

        #endregion properties

    }
}
