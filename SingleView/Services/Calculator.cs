using System;
using DotSpatial.Topology;
using StarMathLib;

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

    public void CalcHG()
    {
      
    }
  }
}
