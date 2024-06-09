namespace cube_unit_test_practice;

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