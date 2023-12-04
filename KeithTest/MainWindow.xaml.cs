using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeithTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = string.Empty;
        private double memory = 0;
        private List<string> history = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            // Handle number buttons
            if (char.IsDigit(content, 0) || content == ".")
            {
                HandleNumberButton(content);
            }
            else if (CheckIsSymbols(content))
            {
                HandleSymbolButton(content);
            }
            
            else if (content == "History")
            {
                ShowHistory();
            }
   
            else
            {
                switch (content)
                {
                    case "C":
                        ClearAll();
                        break;
                    case "CE":
                        ClearEntry();
                        break;
                    case "=":
                        Evaluate();
                        break;
                    case "M+":
                        AddToMemory();
                        break;
                    case "M-":
                        SubtractFromMemory();
                        break;
                    case "MR":
                        RecallMemory();
                        break;
                        
                }
            }
        }


        private void AddToMemory()
        {
            double result;
            if (double.TryParse(currentInput, out result))
            {
                memory += result;
            }
        }

        private void SubtractFromMemory()
        {
           
            double result;
            if (double.TryParse(currentInput, out result))
            {
                memory -= result;
            }
        }

        private void RecallMemory()
        {
            currentInput = memory.ToString();

            UpdateDisplay();
        }
        private void ShowHistory()
        {
            
            string historyText = string.Join(Environment.NewLine, history);
            MessageBox.Show(historyText, "History", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void HandleSymbolButton(string content)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                history.Add(content);
                
                currentInput += content;
                UpdateDisplay();
            }
        }
        private bool CheckIsSymbols(string content)
        {
            char[] symb = { '+', '-', '*', '/' };
            return Array.IndexOf(symb, content[0]) != -1;
        }
        private void ClearAll()
        {
            currentInput = string.Empty;
            memory = 0;
            UpdateDisplay();
        }

        private void ClearEntry()
        {
            currentInput = string.Empty;
            UpdateDisplay();
        }

        private void Evaluate()
        {
            try
            {
                var result = new DataTable().Compute(currentInput, null);
                currentInput = result.ToString();
            }
            catch (Exception ex)
            {         
                currentInput = "Error";
            }
            UpdateDisplay();
        }

        private void HandleNumberButton(string content)
        {

            history.Add(content);
            currentInput += content;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            Display.Text = currentInput;
        }
    }
}
