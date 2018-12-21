using HelloWorld.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UnitTests
{
    public class UnitTests
    {
        private MessageContext GetContextWithData()
        {
            //Use In Memory Database for unit tests
            var options = new DbContextOptionsBuilder<MessageContext>()
                .UseInMemoryDatabase("Test_GetMessage")
                .Options;

            //Test Context
            var context = new MessageContext(options);
            context.Database.EnsureDeleted();   //delete in memory db in order to start fresh each test
            context.Message.Add(new Message { MessageID = 1, Text = "Hello World" });
            context.Message.Add(new Message { MessageID = 2, Text = "Goodbye World" });
            //etc
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void GetMessage_ShouldReturnHelloWorldFirst()
        {
            using (var context = GetContextWithData())
            {
                MessagesController controller = new MessagesController(context);
                IEnumerable<Message> results = controller.GetMessage();
                Assert.NotNull(results);
                Assert.IsAssignableFrom<IEnumerable<Message>>(results);
                Assert.Equal("Hello World", results.Select(x => x.Text).FirstOrDefault());
            }
        }

        [Fact]
        public async void GetMessageInt_ShouldReturnGoodbyeWorldSecond()
        {
            int id = 2;
            using (var context = GetContextWithData())
            {
                MessagesController controller = new MessagesController(context);
                IActionResult response = await controller.GetMessage(id);
                Assert.NotNull(response);
                OkObjectResult result = Assert.IsType<OkObjectResult>(response);
                Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
                Message msg = Assert.IsType<Message>(result.Value);
                Assert.Equal(id, msg.MessageID);
                Assert.Equal("Goodbye World", msg.Text);
            }
        }

        //etc...
    }
}
