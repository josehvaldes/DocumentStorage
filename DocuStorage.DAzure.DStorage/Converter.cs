namespace DocuStorage.DAzure.DStorage;

using DocuStorage.Common.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Converter
{
    public static DocumentEntity Convert(Document document ) 
    {
        return new DocumentEntity() {
            Id = document.Id.ToString(),
            Name = document.Name,
            Description = document.Description,
            Category = document.Category,
            Created_On = document.Created_On.ToString(),
            PartitionKey = document.Category,
            RowKey = document.Id.ToString()
        };
    
    }

    public static Document Convert(DocumentEntity entity) 
    {
        DateTime date = DateTime.Now;
        return new Document() {
            Id = Int32.Parse(entity.Id.ToString()) ,
            Name = entity.Name ,
            Description = entity.Description ,
            Category = entity.Category,
            Created_On = DateTime.TryParse(entity.Created_On, out date)? date: date,
        };
    }

}
