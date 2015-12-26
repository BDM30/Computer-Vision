using System;
using System.Runtime.Remoting.Messaging;
using DotSpatial.Topology;
using Point = System.Drawing.Point;

namespace SingleView.Services
{
  public static class DataManager
  {
    // Need to store points for future computing their VP

    // first line X
    public static Point X11 = new Point(-1,-1);
    public static Point X12 = new Point(-1, -1);
    // second line X
    public static Point X21 = new Point(-1, -1);
    public static Point X22 = new Point(-1,-1);

    public static Point Y11 = new Point(-1,-1);
    public static Point Y12 = new Point(-1,-1);
    public static Point Y21 = new Point(-1,-1);
    public static Point Y22 = new Point(-1,-1);

    public static Point Z11 = new Point(-1,-1);
    public static Point Z12 = new Point(-1,-1);
    public static Point Z21 = new Point(-1,-1);
    public static Point Z22 = new Point(-1,-1);

    // Vanishing Points
    public static DotSpatial.Topology.Point XVP;
    public static DotSpatial.Topology.Point YVP;
    public static DotSpatial.Topology.Point ZVP;

    // Reference Plane Points
    public static Point RP1Image = new Point (-1,-1);
    public static Point RP2Image = new Point (-1, -1);
    public static Point RP3Image = new Point (-1, -1);
    public static Point RP4Image = new Point (-1, -1);
    public static DotSpatial.Topology.Point RP1Space;
    public static DotSpatial.Topology.Point RP2Space;
    public static DotSpatial.Topology.Point RP3Space;
    public static DotSpatial.Topology.Point RP4Space;

    //Current Homography and its Inverse
    public static DotSpatial.Topology.MatrixD Homography = new MatrixD(3,3);
    public static DotSpatial.Topology.MatrixD HomographyInversed = new MatrixD(3, 3);

    // Reference Height Points
    public static Point RH1Image = new Point(-1, -1);
    public static Point RH2Image = new Point(-1, -1);
    public static DotSpatial.Topology.Point RH1Space;
    public static DotSpatial.Topology.Point RH2Space;

    // alpha Z
    public static double AlphaZ;

    // если не пуста и лежит в радиусе 10px
    private static bool IsGoodPoint(Point i, Point lineEnd)
    {
      return !(lineEnd.X == -1 && lineEnd.Y == -1) &&
             Math.Sqrt((i.X - lineEnd.X)*(i.X - lineEnd.X) + (i.Y - lineEnd.Y)*(i.Y - lineEnd.Y)) < 10;
    }

    // если мы задали эту точку, то как минимум нет смысла ее находить
    public static DotSpatial.Topology.Point IsCalculated(Point p)
    {
      if (RP1Image.X == p.X && RP1Image.Y == p.Y)
        return RP1Space;
      if (RP2Image.X == p.X && RP2Image.Y == p.Y)
        return RP2Space;
      if (RP3Image.X == p.X && RP3Image.Y == p.Y)
        return RP3Space;
      if (RP4Image.X == p.X && RP4Image.Y == p.Y)
        return RP4Space;
      if (RH1Image.X == p.X && RH1Image.Y == p.Y)
        return RH1Space;
      if (RH2Image.X == p.X && RH2Image.Y == p.Y)
        return RH2Space;
      return null;
    }

    public static Point FindNearest(Point i)
    {
      // x points
      if (IsGoodPoint(i, X11))
        return X11;
      if (IsGoodPoint(i, X12))
        return X12;
      if (IsGoodPoint(i, X21))
        return X21;
      if (IsGoodPoint(i, X22))
        return X22;

      // y points
      if (IsGoodPoint(i, Y11))
        return Y11;
      if (IsGoodPoint(i, Y12))
        return Y12;
      if (IsGoodPoint(i, Y21))
        return Y21;
      if (IsGoodPoint(i, Y22))
        return Y22;

      // z points
      if (IsGoodPoint(i, Z11))
        return Z11;
      if (IsGoodPoint(i, Z12))
        return Z12;
      if (IsGoodPoint(i, Z21))
        return Z21;
      if (IsGoodPoint(i, Z22))
        return Z22;

      return new Point(-1,-1);
    }

  }
}
