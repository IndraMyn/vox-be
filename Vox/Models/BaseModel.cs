using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vox.Models
{
    public class BaseModel
    {
        /// <summary>
        ///     Entity Framework Identification
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        /// <summary>
        ///     Created data entry Date
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Last Updated data entry Date
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("update_date")]
        public DateTime UpdateDate { get; set; }
    }
}
