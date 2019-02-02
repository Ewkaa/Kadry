

namespace Kadry.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Base { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Overtime { get; set; }
        public decimal Total { get; set; }
    }
}