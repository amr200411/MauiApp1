using System;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class NewPage1 : ContentPage
    {
        private string currentInput = "";
        private string operation = "";
        private double firstNumber = 0;
        private bool isNewEntry = true;

        public NewPage1()
        {
            InitializeComponent();
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Text;

            if (isNewEntry)
            {
                currentInput = number;
                isNewEntry = false;
            }
            else
            {
                currentInput += number;
            }

            DisplayLabel.Text = currentInput;
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            firstNumber = double.Parse(currentInput);
            isNewEntry = true;
        }

        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(operation))
            {
                double secondNumber = double.Parse(currentInput);
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "×":
                        result = firstNumber * secondNumber;
                        break;
                    case "÷":
                        result = secondNumber != 0 ? firstNumber / secondNumber : 0;
                        break;
                }

                DisplayLabel.Text = result.ToString();
                currentInput = result.ToString();
                isNewEntry = true;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = "0";
            firstNumber = 0;
            operation = "";
            isNewEntry = true;
            DisplayLabel.Text = "0";
        }

        private void OnScientificFunctionClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string function = button.Text;
            double number = double.Parse(currentInput);
            double result = 0;

            switch (function)
            {
                case "π":
                    result = Math.PI;
                    break;
                case "e":
                    result = Math.E;
                    break;
                case "x²":
                    result = Math.Pow(number, 2);
                    break;
                case "√x":
                    result = Math.Sqrt(number);
                    break;
                case "|x|":
                    result = Math.Abs(number);
                    break;
                case "exp":
                    result = Math.Exp(number);
                    break;
                case "mod":
                    result = firstNumber % number;
                    break;
            }

            DisplayLabel.Text = result.ToString();
            currentInput = result.ToString();
            isNewEntry = true;
        }
    }
}
