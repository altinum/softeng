
| Data type            | **Value set**                                                                             | **C# type** |
| -------------------- | ----------------------------------------------------------------------------------------- | ----------- |
| `bigint`             | Ranges from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.                      | `long`      |
| `int`                | Ranges from -2,147,483,648 to 2,147,483,647.                                              | `int`       |
| `smallint`           | Ranges from -32,768 to 32,767.                                                            | `short`     |
| `tinyint`            | Ranges from 0 to 255.                                                                     | `byte`      |
| `decimal/numeric`    | Represents decimal fractions, for example, 123.45 or -987.654.                            | `decimal`   |
| `float`              | Represents floating-point numbers, such as 3.14 or -0.001.                                | `double`    |
| `real`               | Represents single-precision floating-point numbers.                                       | `float`     |
| `money/smallmoney`   | Represents monetary values, like 123.45 or -987.65.                                       | `decimal`   |
| `char/varchar`       | Represents non-Unicode character strings, e.g., `'Hello'` or `'World'`.                   | `string`    |
| `nchar/nvarchar`     | Represents Unicode character strings, e.g., `'Árvíztűrő tükörfúrógép'` or `'ОТП Лизинг'`. | `string`    |
| `datetime/datetime2` | Represents a combination of date and time, e.g., `'2024-05-22 14:30:00'`.                 | `DateTime`  |
| `date`               | Represents only the date, e.g., `'2024-05-22'`.                                           | `DateTime`  |
| `time`               | Represents only the time, e.g., `'14:30:00'`.                                             | `TimeSpan`  |
| `bit`                | Represents logical values (`true` or `false`).                                            | `bool`      |
| `binary/varbinary`   | Represents binary data, such as images or files.                                          | `byte[]`    |
| `uniqueidentifier`   | Represents unique identifiers, e.g., `'6F9619FF-8B86-D011-B42D-00C04FC964FF'`.            | `Guid`      |

 > [!NOTE]
 > a `float` megfeleltetés könnyen félrevezető, mert a két nyelv ugyanazokat a szavakat használja eltérő jelentéssel:
 > - SQL Server `real` → C# `float` (32 bit)
> - SQL Server `float` → C# `double` (alapesetben 53 bites mantissza)
> - SQL Server `decimal` → C# `decimal` (128 bit, decimális számábrázolás)


> [!NOTE]
> A `money` és `smallmoney` esetén C# oldalon általában **`decimal` a megfelelő választás**, nem `float`/`double`.

## Fun Fact

The phrase *"The quick brown fox jumps over the lazy dog"*—a classic pangram containing every letter of the English alphabet—has long been used for typing practice, font displays, and testing devices like typewriters and keyboards. It was designed to allow people to see every letter in context quickly.

The *"The quick brown fox jumps over the lazy dog 1234567890”* was the first official message sent over the Washington-Moscow Hotline, connecting the U.S. and the Soviet Union. This historic test message was transmitted on August 30, 1963, following the Cuban Missile Crisis, which underscored the urgent need for quick, direct communication between the two superpowers to reduce nuclear tensions. Designed to ensure that the teletype system could correctly transmit all English letters and numbers, this message marked a breakthrough in Cold War diplomacy, creating a secure, reliable link between the two countries’ leadership

The hotline was a teleprinter link, not a phone as often depicted in movies, and was used for carefully selected messages that were translated and encrypted before sending. Initially, each side performed regular tests, often using innocuous literary excerpts or news. It became a tool for crisis communication, providing a direct line to prevent misunderstandings during several tense moments over the years, including the Six-Day War and the Yom Kippur War