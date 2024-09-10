using System;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        string currentEntry = "";
        string currentOperator;
        double firstNumber = 0, secondNumber = 0, baseNumber = 0;
        bool isOperationPerformed = false;
        bool isBaseSet = false;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (DisplayLabel.Text == "0" || isOperationPerformed)
            {
                DisplayLabel.Text = "";
                isOperationPerformed = false;
            }

            currentEntry += button.Text;
            DisplayLabel.Text += button.Text;
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out firstNumber))
                {
                    currentOperator = button.Text;
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
            else if (isOperationPerformed)
            {
                currentOperator = button.Text;
            }
        }

        void OnEqualsClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out secondNumber))
                {
                    double result = CalculateResult(firstNumber, secondNumber, currentOperator);
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    firstNumber = result;
                    currentEntry = "";
                    currentOperator = null;
                    isOperationPerformed = true;
                }
            }
        }

        void OnClearClicked(object sender, EventArgs e)
        {
            currentEntry = "";
            firstNumber = 0;
            secondNumber = 0;
            baseNumber = 0;
            currentOperator = null;
            isOperationPerformed = false;
            isBaseSet = false;
            DisplayLabel.Text = "0";
        }

        void OnSqrtClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DisplayLabel.Text))
            {
                DisplayLabel.Text = DisplayLabel.Text.Replace(',', '.');
                if (double.TryParse(DisplayLabel.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double currentNumber))
                {
                    double result = Math.Sqrt(currentNumber);
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }
        }
        void OnExponentClicked(object sender, EventArgs e)
        {
            string text = DisplayLabel.Text;
            if (text.Contains('^'))
            {
                var parts = text.Split('^');
                if (parts.Length == 2)
                {
                    if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double baseNumber) &&
                        double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double exponent))
                    {
                        double result = Math.Pow(baseNumber, exponent);
                        DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        DisplayLabel.Text = "Invalid input";
                    }
                }
                else
                {
                    DisplayLabel.Text = "Invalid format";
                }
            }
            else
            {
                DisplayLabel.Text = "No '^' found";
            }
        }
        void OnCaretClicked(object sender, EventArgs e)
        {
            DisplayLabel.Text += "^";
        }

        void OnPercentageClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out firstNumber))
                {
                    double result = firstNumber / 100;
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    firstNumber = result;
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }
        void OnPowerClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DisplayLabel.Text))
            {
                DisplayLabel.Text = DisplayLabel.Text.Replace(',', '.');
                if (double.TryParse(DisplayLabel.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double currentNumber))
                {
                    double result = Math.Pow(currentNumber, 2); 
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                }
            }
        }
        void OnLog10Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out firstNumber))
                {
                    double result = Math.Log10(firstNumber);
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    firstNumber = result; 
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }

        void OnLogWithBaseClicked(object sender, EventArgs e)
        {
            if (isBaseSet)
            {
                if (!string.IsNullOrEmpty(currentEntry))
                {
                    currentEntry = currentEntry.Replace(',', '.');
                    if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out firstNumber))
                    {

                        double result = Math.Log(firstNumber, baseNumber);
                        DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                        currentEntry = "";
                        isOperationPerformed = true;
                        isBaseSet = false;
                    }
                }
                else
                {
                    DisplayLabel.Text = "Set base first";
                }
            }
        }

        void OnSinClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out double angleInDegrees))
                {
                    double angleInRadians = angleInDegrees * (Math.PI / 180);

                    double result = Math.Sin(angleInRadians);

                    result = Math.Round(result, 4);

                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    firstNumber = result;
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }

        void OnCosClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');

                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out double angleInDegrees))
                {
                    double angleInRadians = angleInDegrees * (Math.PI / 180);

                    double result = Math.Cos(angleInRadians);

                    result = Math.Round(result, 4);

                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);


                    firstNumber = result;
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }


        void OnTanClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out double angleInDegrees))
                {
                    double angleInRadians = angleInDegrees * (Math.PI / 180);

                    double result = Math.Tan(angleInRadians);

                    result = Math.Round(result, 4);
 
                    DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                    firstNumber = result;
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }

        void OnBaseClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out baseNumber))
                {
                    DisplayLabel.Text = $"Base {baseNumber} set";
                    currentEntry = "";
                    isBaseSet = true;
                }
            }
        }

        void OnInverseClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentEntry))
            {
                currentEntry = currentEntry.Replace(',', '.');
                if (double.TryParse(currentEntry, NumberStyles.Any, CultureInfo.InvariantCulture, out firstNumber))
                {
                    if (firstNumber != 0)
                    {
                        double result = 1 / firstNumber;
                        DisplayLabel.Text = result.ToString(CultureInfo.InvariantCulture);
                        firstNumber = result;
                    }
                    else
                    {
                        DisplayLabel.Text = "Nie dziel przez zero!";
                    }
                    currentEntry = "";
                    isOperationPerformed = true;
                }
            }
        }

        double CalculateResult(double firstNumber, double secondNumber, string operatorType)
        {
            switch (operatorType)
            {
                case "+":
                    return firstNumber + secondNumber;
                case "-":
                    return firstNumber - secondNumber;
                case "*":
                    return firstNumber * secondNumber;
                case "/":
          
                        return secondNumber != 0 ? firstNumber / secondNumber : 0;
             
                
                    
                default:
                    return 0;
            }
        }
    }
}
