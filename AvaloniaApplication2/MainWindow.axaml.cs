using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication2;

public partial class MainWindow : Window
{
    private float firstNumber;
    private bool firstNumberSaved;
    private string currentNumber;
    private string operation;
    private float result;

    public MainWindow()
    {
        InitializeComponent();
        ResetExpression();
    }

    public void RedrawExpression()
    {
        expressionTextBlock.Text = "";
        if (firstNumberSaved)
        {
            expressionTextBlock.Text += firstNumber;
            if (operation != "")
            {
                expressionTextBlock.Text += operation;

                if (firstNumberSaved)
                {
                    expressionTextBlock.Text += currentNumber;
                }
            }
        }
        else
        {
            expressionTextBlock.Text += currentNumber;
        }
    }

    public void Operator(object sender, RoutedEventArgs args)
    {
        operation = (string)((Avalonia.Controls.Button)sender).Content;
        if (!firstNumberSaved)
        {
            firstNumber = (float)Double.Parse(currentNumber);
            currentNumber = "";
            firstNumberSaved = true;
        }

        RedrawExpression();
    }

    public void WriteNumber(object sender, RoutedEventArgs args)
    {
        currentNumber += ((Avalonia.Controls.Button)sender).Content;
        RedrawExpression();
    }

    public void ResetExpression()
    {
        firstNumber = 0;
        firstNumberSaved = false;
        currentNumber = "";
        operation = "";
        RedrawExpression();
    }

    public void ResetExpression(object sender, RoutedEventArgs args)
    {
        firstNumber = 0;
        firstNumberSaved = false;
        currentNumber = "";
        operation = "";
        RedrawExpression();
    }

    public void ResetAll(object sender, RoutedEventArgs args)
    {
        ResetExpression();
        resultTextBlock.Text = "";
    }

    public void Backspace(object sender, RoutedEventArgs args)
    {
        if (currentNumber.Length > 0)
        {
            currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
            RedrawExpression();
        }
    }

    public void EvaluateExpression(object sender, RoutedEventArgs args)
    {
        switch (operation)
        {
            case "+":
            {
                result = (float)(firstNumber + Double.Parse(currentNumber));
                break;
            }
            case "-":
            {
                result = (float)(firstNumber - Double.Parse(currentNumber));
                break;
            }
            case "*":
            {
                result = (float)(firstNumber * Double.Parse(currentNumber));
                break;
            }
            case "/":
            {
                result = (float)(firstNumber / Double.Parse(currentNumber));
                break;
            }
        }

        resultTextBlock.Text = expressionTextBlock.Text + " = " + result;
        if ((bool)IsSaveHistoryCheckbox.IsChecked)
        {
            Results.Items.Insert(0, resultTextBlock.Text);
        }
        ResetExpression();
    }
    
    public void ClearHistory(object sender, RoutedEventArgs args)
    {
        Results.Items.Clear();
    }
    public void UndoHistory(object sender, RoutedEventArgs args)
    {
        if (Results.Items.Count > 0)
        {
            Results.Items.RemoveAt(0);
        }
    }

    private void ToggleButton_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (RightSide == null) return;
        if ((string)((Avalonia.Controls.RadioButton) sender).Content != "Y")
        {
            RightSide.IsVisible = true;
            this.Width = 540;
        }
        else
        {
            RightSide.IsVisible = false;
            this.Width = 268;
        }
    }
}