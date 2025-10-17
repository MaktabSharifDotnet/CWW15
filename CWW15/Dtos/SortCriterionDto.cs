
using CWW15.Enums;

namespace CWW15.Dtos
{
    public class SortCriterionDto
    {
        public SortByOptionEnum SortBy { get; set; }
        public SortDirectionOptionEnum SortDirection { get; set; } = SortDirectionOptionEnum.Ascending;
    }
}