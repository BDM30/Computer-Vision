using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DotSpatial.Topology;
using System.Runtime.InteropServices;

namespace SingleView.Services
{
  public class Calculator
  {
    private Point CalcVP(Vector a, Vector b, Vector c, Vector d)
    {
      Vector line1Cross = a.Cross(b);
      Vector line2Cross = c.Cross(d);
      Vector res = line1Cross.Cross(line2Cross);
      return new Point(res.X / res.Z, res.Y / res.Z);
    }

    [DllImport("LibCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern void calcHomo(double[] f, double[] s);
    [DllImport("LibCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern double calcAlphaZ(double[] f, double[] s);

    [DllImport("LibCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern void get3dCoords(double x, double y, double z3d);

    [DllImport("LibCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern void get2dCoords(double x, double y, double z);

    [DllImport("LibCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern double get3dCoordsSwitchPlane(double x, double y, double px, double py, double a, double zx, double zy);

    // основано на https://kusemanohar.files.wordpress.com/2014/03/vanishingpt_bobcollins.pdf
    public void CalcVP()
    {

      // moq test
      // for x lines
      DataManager.XVP = CalcVP(
        new Vector(380, 257, 1),
        new Vector(534, 171, 1),
        new Vector(395, 370, 1),
        new Vector(518, 287, 1));

      // for y lines
      DataManager.YVP = CalcVP(
        new Vector(380, 257, 1),
        new Vector(246, 175, 1),
        new Vector(395, 370, 1),
        new Vector(275, 287, 1));

      // for z lines
      DataManager.ZVP = CalcVP(
        new Vector(380, 257, 1),
        new Vector(395, 370, 1),
        new Vector(246, 175, 1),
        new Vector(275, 287, 1));


      //// for x lines
      //DataManager.XVP = CalcVP(
      //  new Vector(DataManager.X11.X, DataManager.X11.Y, 1),
      //  new Vector(DataManager.X12.X, DataManager.X12.Y, 1),
      //  new Vector(DataManager.X21.X, DataManager.X21.Y, 1),
      //  new Vector(DataManager.X22.X, DataManager.X22.Y, 1));

      //// for y lines
      //DataManager.YVP = CalcVP(
      //  new Vector(DataManager.Y11.X, DataManager.Y11.Y, 1),
      //  new Vector(DataManager.Y12.X, DataManager.Y12.Y, 1),
      //  new Vector(DataManager.Y21.X, DataManager.Y21.Y, 1),
      //  new Vector(DataManager.Y22.X, DataManager.Y22.Y, 1));

      //// for z lines
      //DataManager.ZVP = CalcVP(
      //  new Vector(DataManager.Z11.X, DataManager.Z11.Y, 1),
      //  new Vector(DataManager.Z12.X, DataManager.Z12.Y, 1),
      //  new Vector(DataManager.Z21.X, DataManager.Z21.Y, 1),
      //  new Vector(DataManager.Z22.X, DataManager.Z22.Y, 1));
    }

    public List<string> CalcAlphaZ()
    {
      List<string> result = new List<string>();
      double[] f =
      {
        DataManager.ZVP.X,
        DataManager.ZVP.Y
      };

      // moq
      double[] s =
      {
        380,
        257,
        1,
        1,
        0,

        395,
        370,
        1,
        1,
        -1
      };

      // orginal
      //double[] s =
      //{
      //  DataManager.RH1Image.X,
      //  DataManager.RH1Image.Y,
      //  DataManager.RH1Space.X,
      //  DataManager.RH1Space.Y,
      //  DataManager.RH1Space.Z,

      //  DataManager.RH2Image.X,
      //  DataManager.RH2Image.Y,
      //  DataManager.RH2Space.X,
      //  DataManager.RH2Space.Y,
      //  DataManager.RH2Space.Z
      //};


      DataManager.AlphaZ = calcAlphaZ(f, s);
      result.Add("alphaZ = " + DataManager.AlphaZ.ToString());

      result.Add("projection matrix P:");
      string[] linesHomo = System.IO.File.ReadAllLines(@"proj.txt");
      result.AddRange(linesHomo);
      return result;
    } 

    public List<string> CalcHG()
    {
      // what user sees
      List<string> output = new List<string>();

      // at first we need to run c++ code which calculates homorgraphy and its inverse
      // and writes it in homo.txt and homoi.txt respectally.

      // next step is to write the results from the files and to save their values in DataManager.
    
    // moq
      double[] f =
    {
        380,
        257,
        1,
        1,
        0,

        534,
        171,
        0,
        1,
        0,

        392,
        112,
        0,
        0,
        0,

        246,
        175,
        1,
        0,
        0
      };

      double[] s =
      {
        DataManager.XVP.X,
        DataManager.XVP.Y,
        DataManager.YVP.X,
        DataManager.YVP.Y
      };


      // original
      //  double[] f =
      //{
      //  DataManager.RP1Image.X,
      //  DataManager.RP1Image.Y,
      //  DataManager.RP1Space.X,
      //  DataManager.RP1Space.Y,
      //  DataManager.RP1Space.Z,

      //  DataManager.RP2Image.X,
      //  DataManager.RP2Image.Y,
      //  DataManager.RP2Space.X,
      //  DataManager.RP2Space.Y,
      //  DataManager.RP2Space.Z,

      //  DataManager.RP3Image.X,
      //  DataManager.RP3Image.Y,
      //  DataManager.RP3Space.X,
      //  DataManager.RP3Space.Y,
      //  DataManager.RP3Space.Z,

      //  DataManager.RP4Image.X,
      //  DataManager.RP4Image.Y,
      //  DataManager.RP4Space.X,
      //  DataManager.RP4Space.Y,
      //  DataManager.RP4Space.Z,
      //};

      //  double[] s =
      //  {
      //  DataManager.XVP.X,
      //  DataManager.XVP.Y,
      //  DataManager.YVP.X,
      //  DataManager.YVP.Y,
      //};
      calcHomo(f, s);

      output.Add("homography matrix:");
      string[] linesHomo = System.IO.File.ReadAllLines(@"homo.txt");
      output.AddRange(linesHomo);
      string[] linesHomoI = System.IO.File.ReadAllLines(@"homoi.txt");
      output.Add("inversed homography matrix:");
      List<double> homo = new List<double>();
      output.AddRange(linesHomoI);

      foreach (var numbers in linesHomo.Select(line => (IEnumerable<string>) line.Split()))
      {
        homo.AddRange(from number in numbers
          where number.Length > 1 || number.Length == 1 && number.First() != ' '
          select number.Replace(".", ",")
          into pNumber
          select double.Parse(pNumber));
      }
      List<double> homoi = new List<double>();
      foreach (var numbers in linesHomoI.Select(line => (IEnumerable<string>)line.Split()))
      {
        homoi.AddRange(from number in numbers
                      where number.Length > 1
                      select number.Replace(".", ",")
          into pNumber
                      select double.Parse(pNumber));
      }
      // now we have read 2 values of matrixes in an array
      // we need to fill out real matrixes
      var val = new double[3, 3];
      val[0, 0] = homo[0];
      val[0, 1] = homo[1];
      val[0, 2] = homo[2];
      val[1, 0] = homo[3];
      val[1, 1] = homo[4];
      val[1, 2] = homo[5];
      val[2, 0] = homo[6];
      val[2, 1] = homo[7];
      val[2, 2] = homo[8];
      DataManager.Homography.Values = val;

      val = new double[3, 3];
      val[0, 0] = homoi[0];
      val[0, 1] = homoi[1];
      val[0, 2] = homoi[2];
      val[1, 0] = homoi[3];
      val[1, 1] = homoi[4];
      val[1, 2] = homoi[5];
      val[2, 0] = homoi[6];
      val[2, 1] = homoi[7];
      val[2, 2] = homoi[8];
      DataManager.HomographyInversed.Values = val;
      return output;
    }

    public Point get3Dfrom2D(double x, double y)
    {
      get3dCoords(x,y,DataManager.CurrentZ);
      string[] line = System.IO.File.ReadAllLines(@"buffer.txt");
      string[] xy = line.First().Replace(".",",").Split(' ');
      return new Point(double.Parse(xy.First()), double.Parse(xy.Last()), DataManager.CurrentZ);
    }

    public System.Drawing.Point get2Dfrom3D(Point p)
    {
      get2dCoords(p.X, p.Y,p.Z);
      string[] line = System.IO.File.ReadAllLines(@"buffer.txt");
      string[] xy = line.First().Split(' ');
      return new System.Drawing.Point(int.Parse(xy.First()), int.Parse(xy.Last()));
    }

    public Point get3dCoordsSwitchPlane(System.Drawing.Point point)
    {
      double z = get3dCoordsSwitchPlane((double) point.X, (double) point.Y, DataManager.CurrentPlanePoint.X,
        DataManager.CurrentPlanePoint.Y, DataManager.AlphaZ, DataManager.ZVP.X, DataManager.ZVP.Y);
      DataManager.CurrentZ = z;
      return new Point(DataManager.CurrentPlanePoint.X, DataManager.CurrentPlanePoint.Y, z);
    }
  }
}
