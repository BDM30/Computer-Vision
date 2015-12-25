#include <stdexcept>

using namespace std;

namespace MathFuncs
{
	extern "C" { __declspec(dllexport) double* calcHomo(double f[], double s[]); }
}