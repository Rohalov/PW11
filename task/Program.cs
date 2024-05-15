using System.Xml;
using System.Text.Json;
using System.Xml.Serialization;
using task;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        var product = new Product
        {
            Name = "Phone",
            Price = 1000,
            IsInStock = true,
            Categories = new List<string>
            {
                "phones"
            }
        };

        //JSON
        var json = JsonSerializer.Serialize(product);
        Console.WriteLine(json);

        var restoredProduct = JsonSerializer.Deserialize<Product>(json);
        Console.WriteLine(restoredProduct.ToString());


        //XML
        var xmlSerializer = new XmlSerializer(typeof(Product));

        using (FileStream fs = new FileStream("product.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, product);
            Console.WriteLine("Object has been serialized");

            fs.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(fs);
            string xmlContent = reader.ReadToEnd();
            Console.WriteLine(xmlContent);

            fs.Seek(0, SeekOrigin.Begin);

            var restopedProd = xmlSerializer.Deserialize(fs) as Product;
            Console.WriteLine(restopedProd.ToString());
        }
    }
}