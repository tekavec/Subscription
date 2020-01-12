using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Subscription.Domain;
using Subscription.ViewModels;

namespace Subscription.UnitTests.ViewModels
{
    public class CreateDataSourceModelShould
    {
        [Test]
        public void calculate_its_properties_from_input()
        {
            var year = DateTime.Today.Year;
            var month = new Month(DateTime.Today.Month);
            var model = new CreateDataSourceModel(new YearAndMonth(year, month));

            using (new AssertionScope())
            {
                for (int i = 0; i < 12; i++)
                {
                    model.FromMonths[i].Should().Be(new Month(i + 1));
                    model.ToMonths[i].Should().Be(new Month(i + 1));
                }

                model.FromYears[0].Should().Be(year-1);
                model.FromYears[1].Should().Be(year);
                model.FromYears[2].Should().Be(year+1);
                model.ToYears[0].Should().Be(year);
                model.ToYears[1].Should().Be(year+1);

                model.SelectedFromMonth.Should().Be(month);
                model.SelectedFromYear.Should().Be(year);

                var fromDate = new DateTime(model.SelectedFromYear, model.SelectedFromMonth.Number, 1);
                var toDate = new DateTime(model.SelectedToYear, model.SelectedToMonth.Number, 1);
                fromDate.AddMonths(1).Should().Be(toDate);
            }
        }
    }
}