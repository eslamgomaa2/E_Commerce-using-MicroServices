using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specs
{
    public class CatalogSpecParams
    {
        private const int MaxPageSize = 50; // الحد الأقصى المسموح به لأي صفحة
        public int PageIndex { get; set; } = 1; // الصفحة الافتراضية

        private int _pageSize = 10; // الحجم الافتراضي
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // حماية ضد الأعداد الضخمة
        }

        
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
    }
}
