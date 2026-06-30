using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Common
{
    public abstract class BaseEntity
    {
        public int Id { get;  protected set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime ?CreatedDate { get; set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
