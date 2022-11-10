using System.CodeDom;
using System.Collections.Immutable;
using System.Windows.Forms;
using Calc.Interfaces;

namespace Calc.WinFormsInput;

public class DirtyHack
{
  public static void Main() {}
}

public partial class InputForm : Form
{
  private readonly ImmutableArray<OperandInfo> _actions;
  private readonly TextBox[] _textBoxes;
  
  public InputForm(ImmutableArray<OperandInfo> actions)
  {
    _actions = actions;
    InitializeComponent();

    _textBoxes = new TextBox[_actions.Length];
    Height = _actions.Length * 64 + 16;
    for (int i = 0; i < _actions.Length; i++)
    {
      _textBoxes[i] = new TextBox() {
        Width = this.Width - 16,
        Margin = new Padding(8),
        Top = i * 64
      };
    }

    this.Controls.AddRange(_textBoxes);
  }


  public ImmutableArray<OperandValue> GetResult()
  {
    return _textBoxes.Select((tb, i) =>
    {
      if (_actions[i].Type == typeof(float))
        return (object)float.Parse(tb.Text);
      if (_actions[i].Type == typeof(double))
        return double.Parse(tb.Text);
      if (_actions[i].Type == typeof(int))
        return int.Parse(tb.Text);
      if (_actions[i].Type == typeof(long))
        return long.Parse(tb.Text);
      return null;
    }).Select((val, i) => new OperandValue(val, _actions[i]))
      .ToImmutableArray();
  }
}