#include <iostream>
#include <Eigen/Dense>
#include <vector>

using namespace std;
using namespace Eigen;

namespace MathFuncs
{

	struct imgPoint {
		double x;
		double y;
		double w;
	};

	struct scnPoint {
		double x;
		double y;
		double z;
		double w;
	};

	struct connPoint {
		imgPoint imgPt;
		scnPoint scnPt;
		Matrix3d homoMatrixEig3;
	};

	Vector3d imgPointToVector3d(imgPoint imgPt)
	{
		Vector3d vecPt;
		vecPt(0) = imgPt.x;
		vecPt(1) = imgPt.y;
		vecPt(2) = imgPt.w;
		return vecPt;
	}

	Vector3d scnPointToVector3d(scnPoint scnPt)
	{
		Vector3d vecPt;
		vecPt(0) = scnPt.x;
		vecPt(1) = scnPt.y;
		vecPt(2) = scnPt.w;
		return vecPt;
	}


	// first array 20 values: 4 points: 3d scene + 2d image
	// second array 4 values: 2 vanishing points

	// out 18 values 2 matrix homo and its inverse

	double* calcHomo(double f[], double s[])
	{
		double* result = new double[18]();

		imgPoint zeroImg;
		zeroImg.x = f[0];
		zeroImg.y = f[1];
		zeroImg.w = 1;

		scnPoint zeroScn;
		zeroScn.x = f[2];
		zeroScn.y = f[3];
		zeroScn.z = f[4];
		zeroScn.w = 1;

		connPoint zero;
		zero.imgPt = zeroImg;
		zero.scnPt = zeroScn;

		imgPoint oneImg;
		oneImg.x = f[5];
		oneImg.y = f[6];
		oneImg.w = 1;
		scnPoint oneScn;
		oneScn.x = f[7];
		oneScn.y = f[8];
		oneScn.z = f[9];
		oneScn.w = 1;
		connPoint one;
		one.imgPt = oneImg;
		one.scnPt = oneScn;

		imgPoint twoImg;
		twoImg.x = f[10];
		twoImg.y = f[11];
		twoImg.w = 1;
		scnPoint twoScn;
		twoScn.x = f[12];
		twoScn.y = f[13];
		twoScn.z = f[14];
		twoScn.w = 1;
		connPoint two;
		two.imgPt = twoImg;
		two.scnPt = twoScn;

		imgPoint threeImg;
		threeImg.x = f[15];
		threeImg.y = f[16];
		threeImg.w = 1;
		scnPoint threeScn;
		threeScn.x = f[17];
		threeScn.y = f[18];
		threeScn.z = f[19];
		threeScn.w = 1;
		connPoint three;
		three.imgPt = threeImg;
		three.scnPt = threeScn;

		vector<connPoint> planePts;
		planePts.push_back(zero);
		planePts.push_back(one);
		planePts.push_back(two);
		planePts.push_back(three);

		imgPoint xVP;
		xVP.x = s[0];
		xVP.y = s[1];

		imgPoint yVP;
		yVP.x = s[2];
		yVP.y = s[3];

		int refPtN = 6;
		MatrixXd A(2 * refPtN, 8);
		VectorXd b(2 * refPtN);
		vector<connPoint>::iterator iterRPt;
		int i = 0;
		for (iterRPt = planePts.begin(); iterRPt != planePts.end(); iterRPt++, i++)
		{
			connPoint refPt = *iterRPt;
			b(2 * i) = refPt.scnPt.x;
			b(2 * i + 1) = refPt.scnPt.y;

			A(2 * i, 0) = refPt.imgPt.x;
			A(2 * i, 1) = refPt.imgPt.y;
			A(2 * i, 2) = 1;
			A(2 * i, 3) = 0;
			A(2 * i, 4) = 0;
			A(2 * i, 5) = 0;
			A(2 * i, 6) = -refPt.imgPt.x * refPt.scnPt.x;
			A(2 * i, 7) = -refPt.imgPt.y * refPt.scnPt.x;

			A(2 * i + 1, 0) = 0;
			A(2 * i + 1, 1) = 0;
			A(2 * i + 1, 2) = 0;
			A(2 * i + 1, 3) = refPt.imgPt.x;
			A(2 * i + 1, 4) = refPt.imgPt.y;
			A(2 * i + 1, 5) = 1;
			A(2 * i + 1, 6) = -refPt.imgPt.x * refPt.scnPt.y;
			A(2 * i + 1, 7) = -refPt.imgPt.y * refPt.scnPt.y;
		}


		// for xVP
		b(2 * i) = 0;
		b(2 * i + 1) = -1;

		A(2 * i, 0) = 0;
		A(2 * i, 1) = 0;
		A(2 * i, 2) = 0;
		A(2 * i, 3) = xVP.x;
		A(2 * i, 4) = xVP.y;
		A(2 * i, 5) = 1;
		A(2 * i, 6) = 0;
		A(2 * i, 7) = 0;

		A(2 * i + 1, 0) = 0;
		A(2 * i + 1, 1) = 0;
		A(2 * i + 1, 2) = 0;
		A(2 * i + 1, 3) = 0;
		A(2 * i + 1, 4) = 0;
		A(2 * i + 1, 5) = 0;
		A(2 * i + 1, 6) = xVP.x;
		A(2 * i + 1, 7) = xVP.y;

		i++;

		b(2 * i) = 0;
		b(2 * i + 1) = -1;

		A(2 * i, 0) = yVP.x;
		A(2 * i, 1) = yVP.y;
		A(2 * i, 2) = 1;
		A(2 * i, 3) = 0;
		A(2 * i, 4) = 0;
		A(2 * i, 5) = 0;
		A(2 * i, 6) = 0;
		A(2 * i, 7) = 0;

		A(2 * i + 1, 0) = 0;
		A(2 * i + 1, 1) = 0;
		A(2 * i + 1, 2) = 0;
		A(2 * i + 1, 3) = 0;
		A(2 * i + 1, 4) = 0;
		A(2 * i + 1, 5) = 0;
		A(2 * i + 1, 6) = yVP.x;
		A(2 * i + 1, 7) = yVP.y;

		VectorXd x = A.fullPivLu().solve(b);
		std::cout << "\n A: \n" << A << endl;
		std::cout << "\n b: \n" << b << endl;
		MatrixXd Ab(2 * refPtN, 9);

		for (int i = 0; i < 2 * refPtN; i++)
		{
			for (int j = 0; j < 8; j++)
				Ab(i, j) = A(i, j);
			Ab(i, 8) = b(i);
		}

		FullPivLU<MatrixXd> lu(Ab);
		lu.setThreshold(1e-5);
		std::cout << "\n Rank of [A b]: \n" << lu.rank() << endl;
		std::cout << "\n x: \n" << x << endl;
		std::cout << "\n A*x: \n" << A*x << endl;

		Matrix3d homo;
		for (int i = 0; i < 8; i++)
		{
			homo(i / 3, i % 3) = x(i);
		}

		homo(2, 2) = 1;

		std::cout << "\n Homography matrix (from image to scene): \n" << homo << endl;
		std::cout << "\n Homography matrix (from scene to image): \n" << homo.inverse() << endl;

		i = 1;
		for (iterRPt = planePts.begin(); iterRPt != planePts.end(); iterRPt++, i++)
		{
			(*iterRPt).homoMatrixEig3 = homo;
			Vector3d imgPtVec = imgPointToVector3d((*iterRPt).imgPt);
			std::cout << "------------Point " << i << " ------------- \n ImageCoord: \n" << imgPtVec << endl;
			Vector3d scnPtFromImg = homo * imgPtVec;
			scnPtFromImg /= scnPtFromImg(2);
			std::cout << "\n HomoMat * ImageCoord: \n" << scnPtFromImg << endl;
			std::cout << "\n SceneCoord: \n" << scnPointToVector3d((*iterRPt).scnPt) << endl;
		}

		Vector3d XimgPtVec = imgPointToVector3d(xVP);
		std::cout << "------------Point " << i << " ------------- \n ImageCoord: \n" << XimgPtVec << endl;
		Vector3d XscnPtFromImg = homo * XimgPtVec;
		std::cout << "\n HomoMat * ImageCoord: \n" << XscnPtFromImg << endl;
		Vector3d xVPScn;
		xVPScn << 1, 0, 0;
		std::cout << "\n SceneCoord: \n" << xVPScn << endl;

		i++;

		Vector3d imgPtVec = imgPointToVector3d(yVP);
		std::cout << "------------Point " << i << " ------------- \n ImageCoord: \n" << imgPtVec << endl;
		Vector3d scnPtFromImg = homo * imgPtVec;
		std::cout << "\n HomoMat * ImageCoord: \n" << scnPtFromImg << endl;
		Vector3d yVPScn;
		yVPScn << 0, 1, 0;
		std::cout << "\n SceneCoord: \n" << yVPScn << endl;

		result[0] = homo.row(0)[0];
		result[1] = homo.row(0)[1];
		result[2] = homo.row(0)[2];
		result[3] = homo.row(1)[0];
		result[4] = homo.row(1)[1];
		result[5] = homo.row(1)[2];
		result[6] = homo.row(2)[0];
		result[7] = homo.row(2)[1];
		result[8] = homo.row(2)[2];

		Matrix3d resi = homo.inverse();

		result[9] = resi.row(0)[0];
		result[10] = resi.row(0)[1];
		result[11] = resi.row(0)[2];
		result[12] = resi.row(1)[0];
		result[13] = resi.row(1)[1];
		result[14] = resi.row(1)[2];
		result[15] = resi.row(2)[0];
		result[16] = resi.row(2)[1];
		result[17] = resi.row(2)[2];

		return result;
	}

	int main()
	{
		// Todo протестировать
		double f[] =
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

		double s[] =
		{
			1337.689,
			-341.289,
			-487.049,
			-324.402
		};

		double *x = calcHomo(f, s);

		std::cout << x[0] << " " << x[1] << " " << x[2] << endl;
		std::cout << x[3] << " " << x[4] << " " << x[5] << endl;
		std::cout << x[6] << " " << x[7] << " " << x[8] << endl << endl;

		std::cout << x[9] << " " << x[10] << " " << x[11] << endl;
		std::cout << x[12] << " " << x[13] << " " << x[14] << endl;
		std::cout << x[15] << " " << x[16] << " " << x[17] << endl << endl;
		return 0;
	}
}