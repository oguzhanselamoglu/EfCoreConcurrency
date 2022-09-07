using System.ComponentModel.DataAnnotations;

namespace EfCoreConcurrency.Api.DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        

        public byte[] RowVersion { get; set; }
    }
}
