using System;
using System.Collections.Generic;
using System.Linq;
using buybaseapi.Data;
using buybaseapi.Controllers;
using buybaseapi.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace buybaseapi.test
{
    public class ContactsControllerTest
    {
        private readonly ContactsController _controller;
        private readonly IContactData _contactData;
        public ContactsControllerTest()
        {
            _contactData = new ContactsFake();
            _controller = new ContactsController(_contactData);
        }

        [Fact]
        public void GetContacts_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetContacts();
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetContacts_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetContacts() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Contact>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }


        [Fact]
        public void GetContact_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetContact(Guid.NewGuid());
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetContact_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = _controller.GetContact(testGuid);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetContact_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = _controller.GetContact(testGuid) as OkObjectResult;
            // Assert
            Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as Contact).Id);
        }

        [Fact]
        public void AddContacts_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var firstNameMissingItem = new Contact()
            {
                Firm = "test firm3",
                LastName = "atac"
            };
            _controller.ModelState.AddModelError("FirstName", "Required");
            // Act
            var badResponse = _controller.AddContacts(firstNameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddContacts_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Contact testItem = new Contact()
            {
                FirstName = "Ahmet",
                LastName = "Guri",
                Firm = "avax"
            };
            // Act
            var createdResponse = _controller.AddContacts(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void AddContacts_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Contact()
            {
                FirstName = "Ahmet",
                LastName = "Guri",
                Firm = "avax"
            };
            // Act
            var createdResponse = _controller.AddContacts(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Contact;
            // Assert
            Assert.IsType<Contact>(item);
            Assert.Equal("Ahmet", item.FirstName);
        }

        [Fact]
        public void DeleteContact_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
            // Act
            var badResponse = _controller.DeleteContact(notExistingGuid);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse as NotFoundResult);
        }

        [Fact]
        public void DeleteContact_ExistingGuidPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var noContentResponse = _controller.DeleteContact(existingGuid);
            // Assert
            Assert.IsType<OkResult>(noContentResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResponse = _controller.DeleteContact(existingGuid);
            // Assert
            Assert.Equal(2, _contactData.GetContacts().Count());
        }


    }
}
