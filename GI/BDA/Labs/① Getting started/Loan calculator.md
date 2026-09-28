
## Objective

- Get to know the Visual Studio 2026 IDE
- Create a "Windows Forms Application" project
- Create simple UI
- C# language basics

## Calculating the Cost of a Loan

> [!note]
> This excersise is intended to demonstrate language elements; it does not approach the problem from a financial perspective.

```csharp
private void btnOk_Click(object sender, EventArgs e)
{
    decimal loanAmount = decimal.Parse(textBoxLoanAmount.Text);
    decimal monthlyInterest = decimal.Parse(textBoxMonthlyInterest.Text);
    decimal monthlyPaymant = decimal.Parse(textBoxMonthlyPayment.Text);

    decimal reamining = loanAmount;
    decimal paid = 0;

    int month = 0;

    //Loop until the loan is paid off
    while (reamining > 0)
    {
        decimal interest = reamining * (monthlyInterest / 100m);

        if (interest >= monthlyPaymant)
        {
            //The loan will never be paid off because the interest is greater than or equal to the monthly payment
            textBoxMonths.Text = "Never";

            return;
        }

        reamining += interest;

        if (reamining < monthlyPaymant)
        {
            //The last payment can be less than the monthly payment, so we pay off the remaining balance
            paid += reamining;
            reamining = 0;
        }
        else
        {
            paid += monthlyPaymant;
            reamining -= monthlyPaymant;
        }

        month++;
    }

    //Display results
    textBoxTotalPayment.Text = paid.ToString();
    textBoxMonths.Text = $"{month}";
}

```

