using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Subscription.Domain;
using Subscription.ViewModels;

namespace Subscription.UnitTests.ViewModels
{
    public class MainWindowModelShould
    {
        private readonly Mock<IClock> clock = new Mock<IClock>();

        [Test]
        public void have_selected_mont_as_current_month()
        {
            clock.Setup(a => a.UtcNow).Returns(new DateTime(2020, 10, 10));
            var model = new MainWindowModel(clock.Object);

            model.SelectedMonth.Number.Should().Be(10);
        }

        [Test]
        public void have_selected_year_as_current_year()
        {
            clock.Setup(a => a.UtcNow).Returns(new DateTime(2020, 10, 10));
            var model = new MainWindowModel(clock.Object);

            model.SelectedYear.Should().Be(2020);
        }
    }
}