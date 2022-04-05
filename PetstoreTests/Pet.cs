using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Petstore_Swagger_Testing
{
    public class Pet
    {
        public static int petId = (new Random()).Next(1, 1000);
        public int id { get; set; }
        public Categories category { get; set; }
        public string name { get; set; }
        public string[]? photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }

        public class Categories
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Tag
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    }

    public class PetTests {
        [SetUp]
        public void SetUp()
        {   }

        [Test]
        [TestCase(TestName = "Adding a new pet", Description = "Adds a new pet with new id list")]
        public void AddPet()
        {
            Pet pet = new Pet() {
                id = Pet.petId,
                category = new Pet.Categories() { id = 0, name = "string" },          
                name = "doggie",                                                                        
                photoUrls = new[] { "string" },
                tags = new List<Pet.Tag>() { new Pet.Tag() { id = 0, name = "string" } },
                status = "available"
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string body = JsonSerializer.Serialize(pet, options);

            Console.WriteLine(Pet.petId);
            string response = Petstore.SendRequest("/pet", "POST", body).ToString();
            Console.WriteLine(response);

            Assert.That(response.Contains(body));
        }

        [Test]
        [TestCase(TestName = "Finding a pet by ID", Description = "Searches a pet by ID")]
        public void FindPetById()
        {
            var response = Petstore.SendRequest("/pet/" + Pet.petId, "GET", "").ToString();
            Assert.That(response.Contains("\"id\":" + Pet.petId + ","));
        }

        [Test]
        [TestCase(TestName = "Deleteing a pet", Description = "Deletes a pet by ID")]
        public void DeletePetById()
        {
            AddPet();
            var response = Petstore.SendRequest("/pet/" + Pet.petId, "DELETE", "").ToString();
            Assert.That(response.Contains("\"message\":\"" + Pet.petId + "\""));
        }
    }
}
