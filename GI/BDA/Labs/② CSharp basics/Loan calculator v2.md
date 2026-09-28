## 1. Objective

Create a simple C# desktop application that calculates the monthly payment of a loan.
The user enters parameters in **TextBox controls**, clicks a button, and the application displays the result.

This exercise practices:

- UI input handling
- Numeric parsing and validation
- Mathematical formulas
- Conditional error handling
- Formatting output

------

## 2. Problem Description

Build a Windows Forms application that computes the monthly payment for a fixed-rate loan using the following formula:

$$[
M = P \cdot \frac{r(1+r)^n}{(1+r)^n - 1}
]$$

Where:

- **M** — Monthly payment
- **P** — Loan principal (amount borrowed)
- **r** — Monthly interest rate
- **n** — Number of months

The monthly rate is derived from the annual interest rate:

$$[
r = \frac{\text{annualRate}}{100 \times 12}
]$$

------

## 3. User Interface Requirements

Your form must contain:

| Control | Name            | Purpose                  |
| ------- | --------------- | ------------------------ |
| TextBox | `txtPrincipal`  | Loan amount              |
| TextBox | `txtAnnualRate` | Annual interest rate (%) |
| TextBox | `txtMonths`     | Number of months         |
| Button  | `btnCalculate`  | Trigger calculation      |
| Label   | `lblResult`     | Display output           |

Suggested layout:

```
Loan Amount:        [________]
Annual Rate (%):    [________]
Months:             [________]

[ Calculate ]

Monthly Payment:  ________
```

------

## 4. Functional Requirements

When the user presses **Calculate**:

1. Read values from TextBoxes
2. Validate input:
   - Must be numeric
   - Must be positive
3. Convert interest rate to monthly rate
4. Compute monthly payment
5. Display result formatted to two decimals
6. Show error message if input is invalid

------

## 5. Implementation Example

## 5.1 User input error handling

This version of the code does not check for user input errors. If the user enters invalid data in the text boxes in such a way that conversion to a decimal is not possible—for example, by entering a letter—the execution fails with a runtime error.

To avoid this, we use a safe way to convert text to a decimal, as shown below.

```csharp
private void button1_Click(object sender, EventArgs e)
{
    decimal loanAmount = 0;

    if (!decimal.TryParse(textBoxLoanAmount.Text, out loanAmount))
    {
        textBoxLoanAmount.BackColor = Color.Salmon;
        return;
    }
    
    ...
```

> [!IMPORTANT]
>
> Please learn and remember this code pattern, as it is an important one!

Once we change the color of the text box to `Color.Salmon`, the new background color remains forever — or at least until the application terminates. When the user corrects the mistake by modifying the text in the TextBox, we should change the color back to white.

```csharp
private void textBoxLoanAmount_TextChanged(object sender, EventArgs e)
{
    textBoxLoanAmount.BackColor = Color.White;
}
```

## 5.2 Tasks 

Please make the conversion safe for the remaining two TextBoxes as well!

## 5.3 Button Click Handler

```csharp
private void btnCalculate_Click(object sender, EventArgs e)
{
    if (!double.TryParse(txtPrincipal.Text, out double principal) ||
        !double.TryParse(txtAnnualRate.Text, out double annualRate) ||
        !int.TryParse(txtMonths.Text, out int months) ||
        principal <= 0 || annualRate < 0 || months <= 0)
    {
        MessageBox.Show("Please enter valid positive numbers.");
        return;
    }

    double r = annualRate / 100.0 / 12.0;

    double payment;

    if (r == 0)
    {
        // Interest-free loan
        payment = principal / months;
    }
    else
    {
        payment = principal *
                  (r * Math.Pow(1 + r, months)) /
                  (Math.Pow(1 + r, months) - 1);
    }

    lblResult.Text = payment.ToString("F2");
}
```

------

## 6. Extension Tasks (Optional)

Try enhancing the application:

1. Display total payment and total interest
2. Add input range limits
3. Add a Reset button

------

## 7. Learning Outcomes

After completing this exercise you should understand:

- Handling UI input in C#
- Safe numeric parsing (`TryParse`)
- Implementing real-world formulas
- Basic validation patterns
- Formatting numeric output

