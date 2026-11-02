using System;
using System.Windows.Forms;

class Program
{
  [STAThread]
  static void Main()
  {
    Application.Run(new MainForm());
  }
}

class MainForm : Form
{
  TextBox inputA = new TextBox();
  TextBox inputB = new TextBox();
  TextBox inputC = new TextBox();

  ComboBox method = new ComboBox();
  Button button = new Button();
  Label result = new Label();

  public MainForm()
  {
    Text = "ax² + bx + c = 0 ";
    Width = 500;
    Height = 350; 

    Label labelA = new Label { Text = "a", Left = 20, Top = 15 };
    Label labelB = new Label { Text = "b", Left = 170, Top = 15 };
    Label labelC = new Label { Text = "c", Left = 320, Top = 15 };

    inputA.Left = 20;
    inputA.Top = 40;

    inputB.Left = 170;
    inputB.Top = 40;

    inputC.Left = 320;
    inputC.Top = 40;

    method.Left = 20;
    method.Top = 85;
    method.Width = 250;
    method.DropDownStyle = ComboBoxStyle.DropDownList;
    method.Items.Add("Дискриминант");
    method.Items.Add("Виета");
    method.SelectedIndex = 0;

    button.Text = "Решить";
    button.Left = 20;
    button.Top = 125;
    button.Click += Solve;

    result.Left = 20;
    result.Top = 170;
    result.Width = 440;
    result.Height = 120;

    Controls.AddRange(new Control[]
    {
            labelA, labelB, labelC,
            inputA, inputB, inputC,
            method, button, result
    });
  }

  void Solve(object sender, EventArgs e)
  {
    double a, b, c;

    if (!double.TryParse(inputA.Text, out a) ||
        !double.TryParse(inputB.Text, out b) ||
        !double.TryParse(inputC.Text, out c))
    {
      result.Text = "Введите (Целые) числа в поля a, b и c";
      return;
    }

    if (a == 0)
    {
      if (b != 0)
        result.Text = "x = " + (-c / b);
      else if (c == 0)
        result.Text = "Подходит любое x";
      else
        result.Text = "Решений нет";

      return;
    }

    double x1, x2;

    if (method.SelectedIndex == 0)
    {
      double d = b * b - 4 * a * c;

      if (d < 0)
      {
        result.Text = "Действительных корней нет";
        return;
      }

      x1 = (-b - Math.Sqrt(d)) / (2 * a);
      x2 = (-b + Math.Sqrt(d)) / (2 * a);
    }
    else
    {
      double sum = -b / a;
      double product = c / a;
      double tSquared = sum * sum / 4 - product;

      if (tSquared < 0)
      {
        result.Text = "Действительных корней нет";
        return;
      }

      x1 = sum / 2 - Math.Sqrt(tSquared);
      x2 = sum / 2 + Math.Sqrt(tSquared);
    }

    result.Text = "x1 = " + x1 + "\r\nx2 = " + x2;
  }
}