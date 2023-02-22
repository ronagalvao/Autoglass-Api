namespace Autoglass.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? CompanyDocument { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
