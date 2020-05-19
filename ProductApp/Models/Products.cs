using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class Products
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement, Display(Name = "Product Name"),Required, MaxLength(50), RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Product name should contain only Characters")]
        public string Name { get; set; }

        [BsonElement, Display(Name = "Price"), Required]
        public decimal Price { get; set; }

        [BsonElement, Display(Name = "Quantity"), Required]
        public int Quantity { get; set; }

        [BsonElement, Display(Name = "Is GST Applicable ?"),Required]
        public string IsGSTApplicable { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false, HtmlEncode = true)]
        [BsonElement, Display(Name = "Purchase Date"), Required, BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Purchase_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false, HtmlEncode = true)]
        [BsonElement, Display(Name = "Expiry Date"), Required, BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Expiry_Date { get; set; }

        [BsonElement, Display(Name = "Color"), Required]
        public string Color { get; set; }

        [BsonElement, Display(Name = "Status")]
        public string Status { get; set; }



    }
}
