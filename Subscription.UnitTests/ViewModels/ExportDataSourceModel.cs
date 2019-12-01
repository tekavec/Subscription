using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Subscription.Domain;
using Subscription.ViewModels;

namespace Subscription.UnitTests.ViewModels
{
    public class ExportDataSourceModelShould
    {
        [Test]
        public void calculate_its_properties_from_input()
        {
            var year = DateTime.Today.Year;
            var month = new Month(DateTime.Today.Month);
            var model = new ExportDataSourceModel(new YearAndMonth(year, month));

            using (new AssertionScope())
            {
                for (int i = 0; i < 12; i++)
                {
                    model.FromMonths[i].Should().Be(new Month(i + 1));
                }

                model.FromYears[0].Should().Be(year);
                model.FromYears[1].Should().Be(year+1);

                model.SelectedFromMonth.Should().Be(month);
                model.SelectedFromYear.Should().Be(year);

                model.CloneRowsForMultipleCopies.Should().Be(true);
                model.MergeFilePath.Should().BeEmpty();
            }
        }
    }
}