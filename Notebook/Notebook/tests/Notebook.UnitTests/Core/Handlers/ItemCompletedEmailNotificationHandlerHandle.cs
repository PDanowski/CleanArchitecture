﻿using Moq;
using Notebook.Core.Interfaces;
using Notebook.Core.ProjectAggregate;
using Notebook.Core.ProjectAggregate.Events;
using Notebook.Core.ProjectAggregate.Handlers;
using Xunit;

namespace Notebook.UnitTests.Core.Handlers
{
    public class ItemCompletedEmailNotificationHandlerHandle
    {
        private ToDoItemCompletedEmailNotificationHandler _handler;
        private Mock<IEmailSender> _emailSenderMock;

        public ItemCompletedEmailNotificationHandlerHandle()
        {
            _emailSenderMock = new Mock<IEmailSender>();
            _handler = new ToDoItemCompletedEmailNotificationHandler(_emailSenderMock.Object);
        }

        [Fact]
        public async Task ThrowsExceptionGivenNullEventArgument()
        {
#nullable disable
            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
        }

        [Fact]
        public async Task SendsEmailGivenEventInstance()
        {
            await _handler.Handle(new ToDoItemCompletedEvent(new ToDoItem()), CancellationToken.None);

            _emailSenderMock.Verify(sender => sender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}