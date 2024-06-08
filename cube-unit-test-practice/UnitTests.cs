using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace cube_unit_test_practice;

[TestFixture]
public class UnitTests
{
    [Test]
    public void should_be_ordered()
    {
        var amountList = new List<string>()
        {
            "1.2",
            "1.4",
            "0.2",
            "-",
            "-0.005"
        };

        var orderedAmountList = amountList.Select(x =>
        {
            if (x == "-")
            {
                return 0;
            }

            return double.Parse(x, CultureInfo.InvariantCulture) * 0.33;
        }).OrderBy(x => x).ToList();
        
        orderedAmountList[0].Should().Be(-0.005 * 0.33);

    }

}