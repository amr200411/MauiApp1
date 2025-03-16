using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private string _currentInput = "";
        private List<string> _expression = new();
        private double _memory = 0;

        public MainPage()
        {
            InitializeComponent();
        }

       
        private void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var digit = button.Text;

            if (ResultLabel.Text == "0")
                ResultLabel.Text = digit;
            else
                ResultLabel.Text += digit;

            _currentInput += digit;
        }

       
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var operation = button.Text;

            if (!string.IsNullOrEmpty(_currentInput))
            {
                _expression.Add(_currentInput);
                _expression.Add(operation);
                _currentInput = "";
            }

            ResultLabel.Text += " " + operation + " ";
        }

       
        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _expression.Add(_currentInput);
                _currentInput = "";
            }

            if (_expression.Count > 0)
            {
                double result = EvaluateExpression(_expression);
                ResultLabel.Text = result.ToString();
                _expression.Clear();
            }
        }

       
        private void OnClearClicked(object sender, EventArgs e)
        {
            _currentInput = "";
            _expression.Clear();
            ResultLabel.Text = "0";
        }

       
        private void OnNegateClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double num))
            {
                ResultLabel.Text = (-num).ToString();
                _currentInput = ResultLabel.Text;
            }
        }

       
        private void OnPercentClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double num))
            {
                ResultLabel.Text = (num / 100).ToString();
                _currentInput = ResultLabel.Text;
            }
        }

      
        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!_currentInput.Contains("."))
            {
                _currentInput += ".";
                ResultLabel.Text += ".";
            }
        }

      
        private void OnMemoryClear(object sender, EventArgs e)
        {
            _memory = 0;
        }

        
        private void OnMemoryRecall(object sender, EventArgs e)
        {
            ResultLabel.Text = _memory.ToString();
            _currentInput = _memory.ToString();
        }

       
        private void OnMemoryAdd(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double num))
            {
                _memory += num;
            }
        }

     
        private void OnMemorySubtract(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double num))
            {
                _memory -= num;
            }
        }

      
        private double EvaluateExpression(List<string> tokens)
        {
            List<string> newTokens = new();

            
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "×" || tokens[i] == "÷")
                {
                    double left = Convert.ToDouble(newTokens.Last());
                    double right = Convert.ToDouble(tokens[++i]);
                    newTokens.RemoveAt(newTokens.Count - 1);

                    double intermediateResult = tokens[i - 1] == "×" ? left * right : left / right;
                    newTokens.Add(intermediateResult.ToString());
                }
                else
                {
                    newTokens.Add(tokens[i]);
                }
            }

          
            double result = Convert.ToDouble(newTokens[0]);
            for (int i = 1; i < newTokens.Count; i++)
            {
                if (newTokens[i] == "+")
                {
                    result += Convert.ToDouble(newTokens[++i]);
                }
                else if (newTokens[i] == "-")
                {
                    result -= Convert.ToDouble(newTokens[++i]);
                }
            }

            return result;
        }
    }
}
    