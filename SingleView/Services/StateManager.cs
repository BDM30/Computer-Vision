using System.CodeDom;

namespace SingleView.Services
{
  public static class StateManager
  {
    public const int StartState = 0;

    public const int ImagesLoaded = 1;

    public const int PickXLineFirst0PointsPicked = 2;
    public const int PickXLineFirst1PointsPicked = 3;
    public const int PickXLineSecond0PointsPicked = 4;
    public const int PickXLineSecond1PointsPicked = 5;

    public const int PickYLineFirst0PointsPicked = 6;
    public const int PickYLineFirst1PointsPicked = 7;
    public const int PickYLineSecond0PointsPicked = 8;
    public const int PickYLineSecond1PointsPicked = 9;

    public const int PickZLineFirst0PointsPicked = 10;
    public const int PickZLineFirst1PointsPicked = 11;
    public const int PickZLineSecond0PointsPicked = 12;
    public const int PickZLineSecond1PointsPicked = 13;

    public const int PickedAllVP = 14;
    public const int CalcedAllVP = 15;

    public const int ReferencePlane0Picked = 16;
    public const int ReferencePlane1Picked = 17;
    public const int ReferencePlane2Picked = 18;
    public const int ReferencePlane3Picked = 19;

    public const int ReferencePlaneAllPicked = 20;

    public const int ReferenceHeight0Picked = 21;
    public const int ReferenceHeight1Picked = 22;
    public const int ReferenceHeightAllPicked = 23;

    public const int AlphaZCalced = 24;

    public static int CurrentState = 0;

  }
}
