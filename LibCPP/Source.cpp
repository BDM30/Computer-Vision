#include <iostream>
#include <Eigen/Dense>
#include <vector>
#include <fstream>
#include "Source.h"

using namespace std;
using namespace Eigen;

namespace LibCpp
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
	// out homo.txt and homoi.txt (inverse)
	void calcHomo(double f[], double s[])
	{
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

		ofstream fout("homo.txt");
		fout << homo;
		fout.close();
		fout.open("homoi.txt");
		fout << homo.inverse();
		fout.close();

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
	}

	void get3dCoords(double x, double y, double z3d)
	{
		connPoint newPollPt;
		newPollPt.imgPt.x = x;
		newPollPt.imgPt.y = y;
		newPollPt.imgPt.w = 1;
		newPollPt.scnPt.w = 1;

		// reading Inversed Homo Matrix
		std::fstream myfile("homo.txt", std::ios_base::in);
		float a;
		double refHomoS2Array[9];
		int i = 0;

		while (myfile >> a)
		{
			refHomoS2Array[i++] = a;
		}

		Matrix3d refHomoI2S;
		refHomoI2S(0, 0) = refHomoS2Array[0];
		refHomoI2S(0, 1) = refHomoS2Array[1];
		refHomoI2S(0, 2) = refHomoS2Array[2];
		refHomoI2S(1, 0) = refHomoS2Array[3];
		refHomoI2S(1, 1) = refHomoS2Array[4];
		refHomoI2S(1, 2) = refHomoS2Array[5];
		refHomoI2S(2, 0) = refHomoS2Array[6];
		refHomoI2S(2, 1) = refHomoS2Array[7];
		refHomoI2S(2, 2) = refHomoS2Array[8];

		newPollPt.homoMatrixEig3 = refHomoI2S;
		newPollPt.scnPt.z = z3d;
		
		Vector3d scnCord = newPollPt.homoMatrixEig3 * imgPointToVector3d(newPollPt.imgPt);
		scnCord /= scnCord(2);
		newPollPt.scnPt.x = scnCord(0);
		newPollPt.scnPt.y = scnCord(1);

		cout << "Scene Coordinates: (" << newPollPt.scnPt.x << ", " << newPollPt.scnPt.y << ", " << newPollPt.scnPt.z << ")" << endl;

		ofstream fout("buffer.txt");
		fout << newPollPt.scnPt.x << " " << newPollPt.scnPt.y;
		fout.close();

	}

	// input: zVP, 2 RH points + 2 points RH 3d + 2d
	// out: proj.txt
	// return: gammaZ
	double calcAlphaZ(double f[], double s[])
	{

		// reading Inversed Homo Matrix
		std::fstream myfile("homoi.txt", std::ios_base::in);
		float a;
		double refHomoS2IArray[9];
		int i = 0;

		while (myfile >> a)
		{
			refHomoS2IArray[i++] = a;
		}

		Matrix3d refHomoS2I; // ?? ?­??? ? qt
		refHomoS2I(0, 0) = refHomoS2IArray[0];
		refHomoS2I(0, 1) = refHomoS2IArray[1];
		refHomoS2I(0, 2) = refHomoS2IArray[2];
		refHomoS2I(1, 0) = refHomoS2IArray[3];
		refHomoS2I(1, 1) = refHomoS2IArray[4];
		refHomoS2I(1, 2) = refHomoS2IArray[5];
		refHomoS2I(2, 0) = refHomoS2IArray[6];
		refHomoS2I(2, 1) = refHomoS2IArray[7];
		refHomoS2I(2, 2) = refHomoS2IArray[8];

		imgPoint zVP;
		zVP.x = f[0];
		zVP.y = f[1];
		zVP.w = 1;

		imgPoint RHImage1;
		RHImage1.x = s[0];
		RHImage1.y = s[1];
		RHImage1.w = 1;
		scnPoint RHScene1;
		RHScene1.x = s[2];
		RHScene1.y = s[3];
		RHScene1.z = s[4];
		RHScene1.w = 1;
		connPoint RH1;
		RH1.imgPt = RHImage1;
		RH1.scnPt = RHScene1;

		imgPoint RHImage2;
		RHImage2.x = s[5];
		RHImage2.y = s[6];
		RHImage2.w = 1;
		scnPoint RHScene2;
		RHScene2.x = s[7];
		RHScene2.y = s[8];
		RHScene2.z = s[9];
		RHScene2.w = 1;
		connPoint RH2;
		RH2.imgPt = RHImage2;
		RH2.scnPt = RHScene2;

		vector<connPoint> RHpoints;
		RHpoints.push_back(RH1);
		RHpoints.push_back(RH2);

		// calculation
		Vector3d P1, P2, O, Vz, b, t;
		P1 = refHomoS2I.col(0);
		P2 = refHomoS2I.col(1);
		O = refHomoS2I.col(2);
		Vz = imgPointToVector3d(zVP);

		connPoint bConn = RHpoints[1];
		b = imgPointToVector3d(bConn.imgPt);
		connPoint tConn = RHpoints[0];
		t = imgPointToVector3d(tConn.imgPt);
		double z = bConn.scnPt.z;
		double deltaz = tConn.scnPt.z - z;
		cout << "\n P1: \n" << P1 << "\n\n P2: \n" << P2 << "\n\n O: \n" <<
			O << "\n\n Vz: \n" << Vz << "\n\n b: \n" << b << "\n\n t: \n" << t <<
			"\n\n z: " << z << " deltaz: " << deltaz << endl;

		int sign = 1;
		if ((b.cross(t).dot(Vz.cross(t))) > 0) sign = -1;
		double gammaZ;
		gammaZ = sign * ((P1.cross(P2)).dot(O)) * (b.cross(t)).norm() /
			(deltaz * ((P1.cross(P2)).dot(b)) * (Vz.cross(t)).norm() + z * ((P1.cross(P2)).dot(Vz)) * (b.cross(t)).norm());

		Matrix<double, 3, 4> Proj;
		cout << "\n gammaZ: \n " << gammaZ << endl;
		Proj << P1, P2, gammaZ * Vz, O;
		cout << "\n Projection Matrix from scene to image: \n" << Proj << endl;

		ofstream fout("proj.txt");
		fout << Proj;
		fout.close();

		return gammaZ;
	}

	// input: x,y,z coordinates of 3D point
	// out: buffer.txt
	void get2dCoords(double x, double y, double z)
	{
		Vector4d scnCord;
		scnCord(0) = x;
		scnCord(1) = y;
		scnCord(2) = z;
		scnCord(3) = 1;
		// Todo: ñ÷èòàòü ìàòðèöó P èç ôàéëà

		// reading P
		std::fstream myfile("proj.txt", std::ios_base::in);
		float a;
		double pArray[12];
		int i = 0;

		while (myfile >> a)
		{
			pArray[i++] = a;
		}

		Matrix<double, 3, 4> Proj;
		Proj(0, 0) = pArray[0];
		Proj(0, 1) = pArray[1];
		Proj(0, 2) = pArray[2];
		Proj(0, 3) = pArray[3];
		Proj(1, 0) = pArray[4];
		Proj(1, 1) = pArray[5];
		Proj(1, 2) = pArray[6];
		Proj(1, 3) = pArray[7];
		Proj(2, 0) = pArray[8];
		Proj(2, 1) = pArray[9];
		Proj(2, 2) = pArray[10];
		Proj(2, 3) = pArray[11];

		Vector3d imgCord = Proj * scnCord;
		imgCord /= imgCord(2);
		cout << "\n Scene coordinates: \n" << scnCord << "\n Image coordinates: \n" << imgCord << endl;

		ofstream fout("buffer.txt");
		fout << (int) imgCord(0) << " " << (int) imgCord(1);
		fout.close();
	}

	// input (x,y) 3D coords of previous point; zVP; alphaZ
	// return z coordinate of new point
	// out: buffer.txt for coords and 
	// функция меняет матрицу гомографии.
	double get3dCoordsSwitchPlane(double x, double y, double px, double py, double a, double zx, double zy)
	{
		connPoint newPollPt;
		newPollPt.imgPt.x = x;
		newPollPt.imgPt.y = y;
		newPollPt.imgPt.w = 1;
		newPollPt.scnPt.w = 1;
		newPollPt.scnPt.x = px;
		newPollPt.scnPt.y = py;

		imgPoint zVP;
		zVP.x = zx;
		zVP.y = zy;
		zVP.w = 1;

		double gammaZ = a;

		Vector3d P1, P2, O, Vz, b, bscn, t;

		// reading Inversed Homo Matrix
		std::fstream myfile("homoi.txt", std::ios_base::in);
		float aa;
		double refHomoS2IArray[9];
		int i = 0;

		while (myfile >> aa)
		{
			refHomoS2IArray[i++] = aa;
		}

		Matrix3d refHomoS2I;
		refHomoS2I(0, 0) = refHomoS2IArray[0];
		refHomoS2I(0, 1) = refHomoS2IArray[1];
		refHomoS2I(0, 2) = refHomoS2IArray[2];
		refHomoS2I(1, 0) = refHomoS2IArray[3];
		refHomoS2I(1, 1) = refHomoS2IArray[4];
		refHomoS2I(1, 2) = refHomoS2IArray[5];
		refHomoS2I(2, 0) = refHomoS2IArray[6];
		refHomoS2I(2, 1) = refHomoS2IArray[7];
		refHomoS2I(2, 2) = refHomoS2IArray[8];

		P1 = refHomoS2I.col(0);
		P2 = refHomoS2I.col(1);
		O = refHomoS2I.col(2);
		Vz = imgPointToVector3d(zVP);
		bscn << newPollPt.scnPt.x, newPollPt.scnPt.y, 1;
		b = refHomoS2I * bscn;
		b /= b(2);
		t = imgPointToVector3d(newPollPt.imgPt);
		int sign = 1;
		if ((b.cross(t).dot(Vz.cross(t))) > 0) sign = -1;
		double z;

		z = sign * ((P1.cross(P2)).dot(O)) * (b.cross(t)).norm() / (gammaZ * ((P1.cross(P2)).dot(b)) * (Vz.cross(t)).norm());

		newPollPt.scnPt.z = z;

		Matrix3d P;
		P << P1, P2, O + gammaZ * Vz * z;
		Matrix3d zHomo = P.inverse();
		newPollPt.homoMatrixEig3 = zHomo; // типа новая гомография?

		cout << newPollPt.homoMatrixEig3 << endl;
		cout << newPollPt.scnPt.z << endl;

		ofstream fout("homo.txt");
		fout << newPollPt.homoMatrixEig3;
		fout.close();
		fout.open("homoi.txt");
		fout << newPollPt.homoMatrixEig3.inverse();
		fout.close();
		return z;
	}

}

//int main()
//{
//	LibCpp::get3dCoordsSwitchPlane(395, 367, 1, 1, -0.08894, 533.426, 1246.416);
//	return 0;
//}
