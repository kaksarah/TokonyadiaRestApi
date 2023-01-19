using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TokonyadiaRestApi.Entities
{
    [Table(name: "m_store")]
    public class Store
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }

        [Column(name: "store_name", TypeName = "NVarchar(48)")]
        public string StoreName { get; set; }

        [Column(name: "phone_number", TypeName = "NVarchar(14)")]
        public string PhoneNumber { get; set; }

        [Column(name: "address", TypeName = "NVarchar(100)")]
        public string Address { get; set; }

    }

}
