using System.Drawing;
using System.Windows.Forms;
using Point = DotSpatial.Topology.Point;

public static class PointGetter3D
{
  private static bool _warningShown = false;

  public static Point ShowDialog(bool limitedZ)
  {
    if (!_warningShown)
    {
      MessageBox.Show(@"for double use 1,04 format!");
      _warningShown = true;
    }
    

    Form prompt = new Form()
    {
      Width = 280,
      Height = 220,
      FormBorderStyle = FormBorderStyle.FixedDialog,
      Text = @"Enter 3D coordinates",
      StartPosition = FormStartPosition.CenterScreen
    };

    Label XtextLabel = new Label() {Left = 80, Top = 20, Text = "X"};
    TextBox XtextBox = new TextBox() {Left = 80, Top = 35, Width = 60};

    Label YtextLabel = new Label() {Left = 80, Top = 55, Text = "Y"};
    TextBox YtextBox = new TextBox() {Left = 80, Top = 70, Width = 60};

    Label ZtextLabel = new Label() {Left = 80, Top = 95, Text = "Z"};
    TextBox ZtextBox = new TextBox() {Left = 80, Top = 110, Width = 60};
    if (limitedZ)
    {
      ZtextBox.Text = "0";
      ZtextBox.BackColor = Color.Gray;
    }
    Button confirmation = new Button() {Text = "Ok", Left = 80, Width = 80, Top = 140, DialogResult = DialogResult.OK};
    confirmation.Click += (sender, e) => { prompt.Close(); };

    prompt.Controls.Add(XtextBox);
    prompt.Controls.Add(XtextLabel);

    prompt.Controls.Add(YtextBox);
    prompt.Controls.Add(YtextLabel);

    prompt.Controls.Add(ZtextBox);
    prompt.Controls.Add(ZtextLabel);

    prompt.Controls.Add(confirmation);
    prompt.AcceptButton = confirmation;

    if (prompt.ShowDialog() == DialogResult.OK)
    {
      
      double x = double.Parse(XtextBox.Text);
      double y = double.Parse(YtextBox.Text);
      double z = double.Parse(ZtextBox.Text);
      return new Point(x, y, z);
    }
    // недостижимый код
    return new Point();
  }
}