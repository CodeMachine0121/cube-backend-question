using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace cube_unit_test_practice;

[TestFixture]
public class CodingQuestions
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

    [Test]
    public void should_hide_credit_card_number()
    {
        var creditCardNumber = "0123456789012345";
        var hiddenCreditCardNumberCharArray = creditCardNumber.ToCharArray()
            .Select((x, i) => i < creditCardNumber.Length - 4
                ? '*'
                : x).ToArray();
        
        var hiddenCreditCardNumberString = new string(hiddenCreditCardNumberCharArray);
        var length = hiddenCreditCardNumberString.Length;

        var result = new string("");
        for (var i = 0; i < length; i++)
        {
            result += i % 4 == 0 && i != 0
                ? "-" + hiddenCreditCardNumberString[i]
                : hiddenCreditCardNumberString[i];
        }

        result.Should().Be("****-****-****-2345");
    }

    [Test]
    public void should_print_data_with_general_type()
    {
        // only print in console so doesnt need to assert
        PrinterClass<int>.PrintData(1);
        PrinterClass<string>.PrintData("Hello"); 
        PrinterClass<DateTime>.PrintData(DateTime.Now);
    }
}

public static class PrinterClass<T>
{
    public static void PrintData(T data)
    {
        Console.Write("Data: ");
        if (data is DateTime dateTime)
        {
           Console.WriteLine(dateTime.ToString("yyyy/MM/dd").Replace(".", "/"));
        }

        Console.WriteLine(data);
    }
}