#include <stdexcept>

using namespace std;

namespace LibCpp
{
	extern "C" { __declspec(dllexport) void calcHomo(double f[], double s[]); }
	extern "C" { __declspec(dllexport) double calcAlphaZ(double f[], double s[]); }
}