#include <stdexcept>

using namespace std;

namespace LibCpp
{
	extern "C" { __declspec(dllexport) void calcHomo(double f[], double s[]); }
	extern "C" { __declspec(dllexport) double calcAlphaZ(double f[], double s[]); }
	extern "C" { __declspec(dllexport) void get3dCoords(double x, double y, double z3d); }
	extern "C" { __declspec(dllexport) void get2dCoords(double x, double y, double z); }
	extern "C" { __declspec(dllexport) double get3dCoordsSwitchPlane(double x, double y, double px, double py, double a, double zx, double zy); }
	
	
}