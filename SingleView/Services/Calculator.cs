using System.Collections.Generic;
using System.Linq;
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

    // основано на https://kusemanohar.files.wordpress.com/2014/03/vanishingpt_bobcollins.pdf
    public void CalcVP()
    {
      // for x lines
      DataManager.XVP = CalcVP(
        new Vector(DataManager.X11.X, DataManager.X11.Y, 1),
        new Vector(DataManager.X12.X, DataManager.X12.Y, 1),
        new Vector(DataManager.X21.X, DataManager.X21.Y, 1),
        new Vector(DataManager.X22.X, DataManager.X22.Y, 1));

      // for y lines
      DataManager.YVP = CalcVP(
        new Vector(DataManager.Y11.X, DataManager.Y11.Y, 1),
        new Vector(DataManager.Y12.X, DataManager.Y12.Y, 1),
        new Vector(DataManager.Y21.X, DataManager.Y21.Y, 1),
        new Vector(DataManager.Y22.X, DataManager.Y22.Y, 1));

      // for z lines
      DataManager.ZVP = CalcVP(
        new Vector(DataManager.Z11.X, DataManager.Z11.Y, 1),
        new Vector(DataManager.Z12.X, DataManager.Z12.Y, 1),
        new Vector(DataManager.Z21.X, DataManager.Z21.Y, 1),
        new Vector(DataManager.Z22.X, DataManager.Z22.Y, 1));
    }

    public List<string> CalcHG()
    {
      // what user sees
      List<string> output = new List<string>();

      // at first we need to run c++ code which calculates homorgraphy and its inverse
      // and writes it in homo.txt and homoi.txt respectally.

      // next step is to write the results from the files and to save their values in DataManager. 

      double[] f =
    {
      388.220,
      324.755,
      1,
      1,
      0,
      541.450,
      217.266,
      0,
      1,
      0,
      250.999,
      222.983,
      1,
      0,
      0,
      399.083,
      140.079,
      0,
      0,
      0
    };

      double[] s =
      {
      1337.689,
      -341.289,
      -487.049,
      -324.402
    };
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
  }
}
