using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buybaseapi.Models;
using buybaseapi.Data;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;

namespace buybaseapi.Controllers
{
    
    [ApiController]
    public class ContactInformationsController : ControllerBase
    {
        private IContactInformationData _contactInformationData;
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;

        public ContactInformationsController(IContactInformationData contactInformationData)
        {
            _contactInformationData = contactInformationData;
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "buybase", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var jsonString = Encoding.UTF8.GetString(body);
                Console.WriteLine($"message received {jsonString}");
            };
        }

       

       
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddContacts(ContactInformation contactInformation)
        {
            _contactInformationData.AddContactInformation(contactInformation);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + contactInformation.Id, contactInformation);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = _contactInformationData.GetContactInformation(id);

            if (contact != null)
            {
                using (var channel = this.connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "buybase", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var payload = JsonSerializer.Serialize(contact);
                    var body = Encoding.UTF8.GetBytes(payload);
                    channel.BasicPublish(exchange: "", routingKey: "buybase", basicProperties: null, body: body);
                }
                //_contactInformationData.DeleteContactInformation(contact);
                return Ok();
            }
            return NotFound("Record Not Found");
        }

        [HttpGet]
        [Route("api/[controller]/contactsOfLocation/{location}")]
        public IActionResult GetCountOfContactsOfLocation(string location)
        {
            var count = _contactInformationData.GetCountOfContactsOfLocation(location);
            return Ok(count);
        }


    }
}
