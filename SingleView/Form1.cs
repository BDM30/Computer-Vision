﻿using System;
using System.Drawing;
using System.Windows.Forms;
using SingleView.Services;
using Point = System.Drawing.Point;

namespace SingleView
{
  public partial class Form1 : Form
  {
    private Calculator calc = new Calculator();

    public Form1()
    {
      InitializeComponent();
      pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.StartState)
      {
        openFileDialog.InitialDirectory = @"C:\";
        openFileDialog.Multiselect = true;
        openFileDialog.FilterIndex = 1;
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          pictureBox.Image = Image.FromFile(openFileDialog.FileName);
        }
        StateManager.CurrentState = StateManager.ImagesLoaded;
      }
      else
      {
        MessageBox.Show(@"error state please restart program!");
      }
    }

    private void pictureBox_MouseDown(object sender, MouseEventArgs args)
    {
      Graphics g = Graphics.FromHwnd(pictureBox.Handle);
      SolidBrush xPointBrush = new SolidBrush(Color.Aqua);
      SolidBrush yPointBrush = new SolidBrush(Color.Bisque);
      SolidBrush zPointBrush = new SolidBrush(Color.AliceBlue);
      SolidBrush rpPointBrush = new SolidBrush(Color.Gray);
      SolidBrush rhPointBrush = new SolidBrush(Color.Wheat);
      SolidBrush scenePointBrush = new SolidBrush(Color.Black);
      SolidBrush switchPlaneBrush = new SolidBrush(Color.White);

      Pen xPointPen = new Pen(Color.GreenYellow, 5f);
      Pen yPointPen = new Pen(Color.Blue, 5f);
      Pen zPointPen = new Pen(Color.Yellow, 5f);

      /*
      Если уже есть рядом(в радиусе 10 px) годная точка, то мы заменим на нее
      Если нужны мелкие детали, то это лучше убрать.
      Это нужно если вы хотите выбрать ту же самую точку, что выбрали. И без этого шансов нет.
      Из-за ошибок в вычислениях где-то глубоко автокорекция выключена при Edit -> Points (Testing)
      */
      Point e = DataManager.FindNearest(new Point(args.X, args.Y));
      if (e.X == -1 && e.Y == -1)
      {
        e.X = args.X;
        e.Y = args.Y;
      }
      else
      {
        MessageBox.Show("Point Substitued!");
      }


      switch (StateManager.CurrentState)
      {
        // X lines
        case StateManager.PickXLineFirst0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(xPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.X11 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickXLineFirst1PointsPicked;
          break;
        case StateManager.PickXLineFirst1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(xPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.X12 = new Point(e.X, e.Y);
          g.DrawLine(xPointPen, DataManager.X11, DataManager.X12);
          StateManager.CurrentState = StateManager.PickXLineSecond0PointsPicked;
          listBox.Items.Add("first X-line is drawn by GreenYellow color");
          listBox.Items.Add("next step is pick up points of the second XLine");
          break;
        case StateManager.PickXLineSecond0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(xPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.X21 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickXLineSecond1PointsPicked;
          break;
        case StateManager.PickXLineSecond1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(xPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.X22 = new Point(e.X, e.Y);
          g.DrawLine(xPointPen, DataManager.X21, DataManager.X22);
          StateManager.CurrentState = StateManager.PickYLineFirst0PointsPicked;
          listBox.Items.Add("second X-line is drawn by GreenYellow color");
          listBox.Items.Add("next step is pick up points of the first YLine");
          break;
        // Y lines
        case StateManager.PickYLineFirst0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(yPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Y11 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickYLineFirst1PointsPicked;
          break;
        case StateManager.PickYLineFirst1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(yPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Y12 = new Point(e.X, e.Y);
          g.DrawLine(yPointPen, DataManager.Y11, DataManager.Y12);
          StateManager.CurrentState = StateManager.PickYLineSecond0PointsPicked;
          listBox.Items.Add("first Y-line is drawn by Blue color");
          listBox.Items.Add("next step is pick up points of the second YLine");
          break;
        case StateManager.PickYLineSecond0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(yPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Y21 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickYLineSecond1PointsPicked;
          break;
        case StateManager.PickYLineSecond1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(yPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Y22 = new Point(e.X, e.Y);
          g.DrawLine(yPointPen, DataManager.Y21, DataManager.Y22);
          StateManager.CurrentState = StateManager.PickZLineFirst0PointsPicked;
          listBox.Items.Add("second Y-line is drawn by Blue color");
          listBox.Items.Add("next step is pick up points of the first YLine");
          break;
        // Z lines
        case StateManager.PickZLineFirst0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(zPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Z11 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickZLineFirst1PointsPicked;
          break;
        case StateManager.PickZLineFirst1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(zPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Z12 = new Point(e.X, e.Y);
          g.DrawLine(zPointPen, DataManager.Z11, DataManager.Z12);
          StateManager.CurrentState = StateManager.PickZLineSecond0PointsPicked;
          listBox.Items.Add("first Z-line is drawn by Yellow color");
          listBox.Items.Add("next step is pick up points of the second ZLine");
          break;
        case StateManager.PickZLineSecond0PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(zPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Z21 = new Point(e.X, e.Y);
          StateManager.CurrentState = StateManager.PickZLineSecond1PointsPicked;
          break;
        case StateManager.PickZLineSecond1PointsPicked:
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ")");
          g.FillEllipse(zPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.Z22 = new Point(e.X, e.Y);
          g.DrawLine(zPointPen, DataManager.Z21, DataManager.Z22);
          StateManager.CurrentState = StateManager.PickedAllVP;
          listBox.Items.Add("second Z-line is drawn by Yellow color");
          listBox.Items.Add("next step is to calc Vanishing Points");
          break;

        // picking point for Reference Plane
        case StateManager.ReferencePlane0Picked:
          g.FillEllipse(rpPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RP1Image = new Point(e.X, e.Y);
          DataManager.RP1Space = PointGetter3D.ShowDialog(limitedZ:true);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RP1Space.X + ";" +
                            DataManager.RP1Space.Y + ";" + DataManager.RP1Space.Z + ") in space");
          StateManager.CurrentState = StateManager.ReferencePlane1Picked;
          break;
        case StateManager.ReferencePlane1Picked:
          g.FillEllipse(rpPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RP2Image = new Point(e.X, e.Y);
          DataManager.RP2Space = PointGetter3D.ShowDialog(limitedZ: true);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RP2Space.X + ";" +
                            DataManager.RP2Space.Y + ";" + DataManager.RP2Space.Z + ") in space");
          StateManager.CurrentState = StateManager.ReferencePlane2Picked;
          break;
        case StateManager.ReferencePlane2Picked:
          g.FillEllipse(rpPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RP3Image = new Point(e.X, e.Y);
          DataManager.RP3Space = PointGetter3D.ShowDialog(limitedZ: true);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RP3Space.X + ";" +
                            DataManager.RP3Space.Y + ";" + DataManager.RP3Space.Z + ") in space");
          StateManager.CurrentState = StateManager.ReferencePlane3Picked;
          break;
        case StateManager.ReferencePlane3Picked:
          g.FillEllipse(rpPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RP4Image = new Point(e.X, e.Y);
          DataManager.RP4Space = PointGetter3D.ShowDialog(limitedZ: true);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RP4Space.X + ";" +
                            DataManager.RP4Space.Y + ";" + DataManager.RP4Space.Z + ") in space");
          StateManager.CurrentState = StateManager.ReferencePlaneAllPicked;
          break;
        case StateManager.ReferenceHeight0Picked:
          g.FillEllipse(rhPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RH1Image = new Point(e.X, e.Y);
          DataManager.RH1Space = PointGetter3D.ShowDialog(limitedZ: false);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RH1Space.X + ";" +
                            DataManager.RH1Space.Y + ";" + DataManager.RH1Space.Z + ") in space");
          StateManager.CurrentState = StateManager.ReferenceHeight1Picked;
          break;
        case StateManager.ReferenceHeight1Picked:
          g.FillEllipse(rhPointBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.RH2Image = new Point(e.X, e.Y);
          DataManager.RH2Space = PointGetter3D.ShowDialog(limitedZ: false);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + DataManager.RH2Space.X + ";" +
                            DataManager.RH2Space.Y + ";" + DataManager.RH2Space.Z +  ") in space");
          StateManager.CurrentState = StateManager.ReferenceHeightAllPicked;
          break;
        case StateManager.PointsCalc:
          g.FillEllipse(scenePointBrush, e.X - 5, e.Y - 5, 10, 10);
          // проверка не тыкнули ли мы уже в известную точку
          DotSpatial.Topology.Point point = DataManager.IsCalculated(new Point(e.X, e.Y)) ?? calc.get3Dfrom2D(e.X, e.Y);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + point.X + ";" +
            point.Y + ";" + point.Z + ") on the scene");
          break;
        case StateManager.PlaneSwitch:
          DotSpatial.Topology.Point pointps = DataManager.IsCalculated(new Point(e.X, e.Y)) ?? calc.get3Dfrom2D(e.X, e.Y);
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + pointps.X + ";" +
            pointps.Y + ";" + pointps.Z + ") on the scene");
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as point on current plane");
          g.FillEllipse(switchPlaneBrush, e.X - 5, e.Y - 5, 10, 10);
          DataManager.CurrentPlanePoint = pointps;
          StateManager.CurrentState = StateManager.PlaneSwitchPointPicked;
          MessageBox.Show(@"now pick up a point on other plane with same X,Y coordinates!");
          break;
        case StateManager.PlaneSwitchPointPicked:
          g.FillEllipse(switchPlaneBrush, e.X - 5, e.Y - 5, 10, 10);
          DotSpatial.Topology.Point pointPS = calc.get3dCoordsSwitchPlane(new Point(e.X, e.Y));
          // проверка не тыкнули ли мы уже в известную точку
          DotSpatial.Topology.Point pointSuspect = DataManager.IsCalculated(new Point(e.X, e.Y));
          if (pointSuspect != null)
            pointPS = pointSuspect;
          listBox.Items.Add("picked (" + e.X + ";" + e.Y + ") on image as (" + pointPS.X + ";" +
            pointPS.Y + ";" + pointPS.Z + ") on the scene");
          StateManager.CurrentState = StateManager.PointsCalc;
          break;
      }
    }

    private void xLinesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.ImagesLoaded)
      {
        StateManager.CurrentState = StateManager.PickXLineFirst0PointsPicked;
        listBox.Items.Add("next step is pick up points of the first XLine");
      }
      else
      {
        MessageBox.Show(@"error state load picture first!");
      }
    }

    private void xVPointToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.PickedAllVP)
      {
        calc.CalcVP();
        listBox.Items.Add("we found Vanising Points");
        listBox.Items.Add("For X it is (" + DataManager.XVP.X + ";" + DataManager.XVP.Y + ")");
        listBox.Items.Add("For Y it is (" + DataManager.YVP.X + ";" + DataManager.YVP.Y + ")");
        listBox.Items.Add("For Z it is (" + DataManager.ZVP.X + ";" + DataManager.ZVP.Y + ")");
        StateManager.CurrentState = StateManager.CalcedAllVP;
      }
      else
      {
        MessageBox.Show(@"error state pick up lines first!");
      }
    }

    private void referencePlaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.CalcedAllVP)
      {
        listBox.Items.Add("please select 4 points on Reference Plane");
        StateManager.CurrentState = StateManager.ReferencePlane0Picked;
      }
      else
      {
        MessageBox.Show(@"error state calc Vanishing Points first!");
      }
    }

    private void homographyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.ReferencePlaneAllPicked)
      {
        
        var res = calc.CalcHG();
        listBox.Items.Add("we calced homorgaphy and its inverse");
        foreach (var s in res)
        {
          listBox.Items.Add(s);
        }
        StateManager.CurrentState = StateManager.ReferenceHeight0Picked;
      }
      else
      {
        MessageBox.Show(@"error state pick 4 point in reference plane first!");
      }
    }

    private void alphaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.ReferenceHeightAllPicked)
      {
        var res = calc.CalcAlphaZ();
        listBox.Items.Add("we calced alpha Z and Projection Matrix");
        foreach (var s in res)
        {
          listBox.Items.Add(s);
        }
        StateManager.CurrentState = StateManager.AlphaZCalced;
      }
      else
      {
        MessageBox.Show(@"error state pick 2 reference height point first!");
      }
    }

    private void pointsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState == StateManager.AlphaZCalced)
      {
        StateManager.CurrentState = StateManager.PointsCalc;
      }
      else
      {
        MessageBox.Show(@"error state calc projection matrix first!");
      }
    }

    private void sceneToImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState >= StateManager.AlphaZCalced)
      {
        // диалогом взять точку, перевести ее в 2д, отрисовать ее на пикче.
        Graphics g = Graphics.FromHwnd(pictureBox.Handle);
        SolidBrush pointBrush = new SolidBrush(Color.Gold);
        DotSpatial.Topology.Point point3D = PointGetter3D.ShowDialog(limitedZ: false);
        Point point2D = calc.get2Dfrom3D(point3D);
        g.FillEllipse(pointBrush, point2D.X - 5, point2D.Y - 5, 10, 10);
        listBox.Items.Add("picked (" + point3D.X + ";" + point3D.Y + ";" + point3D.Z + ") on scene as (" + point2D.X + ";" +
                            point2D.Y + " ) in picture");
      }
      else
      {
        MessageBox.Show(@"error state calc projection matrix first!");
      }
    }

    private void switchPlaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (StateManager.CurrentState >= StateManager.AlphaZCalced)
      {
        MessageBox.Show(@"select any point in current plane!");
        StateManager.CurrentState = StateManager.PlaneSwitch;
      }
      else
      {
        MessageBox.Show(@"error state calc projection matrix first!");
      }
    }
  }
}
