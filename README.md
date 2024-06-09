# Cube
- 解題除了SQL 題目外，皆以 UnitTest 型態作呈現
- 程式題 Source code 皆可在 UnitTest 專案中尋獲
- SQL題 可在 `sql-schemes` 資料夾中尋獲
## Questions1
- API 回傳以下金額資訊，請您將金額 * 利率0.33後，由大到小進行排序。 ( - 視為 0 )
   - string[] amountList = { "1.2", "1.4", "0.2", "-", "-0.005" };
```csharp
   var amountList = new List<string>()
   {
      "1.2",
      "1.4",
      "0.2",
      "-",
      "-0.005"
   };
   
   var rate = 0.33;
   
   var orderedAmountList = amountList.Select(x =>
   {
      if (x == "-")
      {
          return 0;
      }
   
      return double.Parse(x, CultureInfo.InvariantCulture) * rate;
   }).OrderByDescending(x => x).ToList();
   
   orderedAmountList.Should().BeEquivalentTo(new List<double>()
   {
      1.4 * rate,
      1.2 * rate,
      0.2 * rate,
      0 * rate,
      -0.005 * rate
   });
```

## Question2
- 客戶傳入信用卡號，請進行信用卡卡號隱碼，信用卡號分為16碼與12碼隱碼規則 : 僅顯示末4碼，其餘以*隱碼，並每四位以 - 分隔開
- Example :
  - 0123456789012345 -> `****-****-****-2345`
  - 012345678901 -> `****-****-8901`
```csharp
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
```
## Question 3
- 請用泛型實作一個function滿足以下需求:
  - 輸入參數型別可為任何型別 
  - 輸出結果為字串型別 
  - 當輸入型別為DateTime時，輸出結果須轉為”xxxx/xx/xx”格式之年月日字串 
  - 例如 
    - 輸入數字888輸出字串型別 "888", 
    - 輸入DateTime 2022/6/2 12:05:33輸出字串型別 ”2022/06/2”
```csharp
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
```

## Question 4 (SQL)
- 請使用SQL回答問題，DB中有以下三個Table
  - Students [ ClassID , StdID , StdName ]
  - Examiner [ ClassID , ExerID , ExerName ]
  - Exam [ ExamType , ExamSubject , StdID , Score ]
### 列出每個班級的主考官姓名(ExaminerName)與學生人數
```sql
select ex.[ExerName], std.[ClassId], count(1) [studnet-count]
from Students std
         join Examiner ex on ex.[ClassId] = std.[ClassId]
group by ex.[ExerName], std.[ClassId]
```
### 列出ExamType為「FinalExam」的考試，每班所有科目分數相加後，總分最高的學生姓名
```sql
select top 1  s.[StdName]
from Exam e
         join dbo.Students S on e.StdID = S.StdID
where e.[ExamType] = 'Final'
group by e.[StdID], s.[StdName]
order by sum(e.[Score]) desc
```
