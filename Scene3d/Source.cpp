#include <GL/glew.h>
#include <GL/glut.h>  // GLUT, include glu.h and gl.h

#include <stdio.h>
#include <math.h>

/* Global variables */
char title[] = "3D Shapes with animation";
GLfloat angleCube = 0.0f;     // Rotational angle for cube [NEW]
int refreshMills = 15;        // refresh interval in milliseconds [NEW]
double dist = 0;

GLuint rbtop, rbleft, rbright;
GLuint b1top, b1left, b1right;
GLuint b2top, b2left, b2right;


GLuint loadBMP_custom(const char * imagepath) {

	printf("Reading image %s\n", imagepath);

	// Data read from the header of the BMP file
	unsigned char header[54];
	unsigned int dataPos;
	unsigned int imageSize;
	unsigned int width, height;
	// Actual RGB data
	unsigned char * data;

	// Open the file
	FILE * file = fopen(imagepath, "rb");
	if (!file) { printf("%s could not be opened. Are you in the right directory ? Don't forget to read the FAQ !\n", imagepath); getchar(); return 0; }

	// Read the header, i.e. the 54 first bytes

	// If less than 54 bytes are read, problem
	if (fread(header, 1, 54, file) != 54) {
		printf("Not a correct BMP file\n");
		return 0;
	}
	// A BMP files always begins with "BM"
	if (header[0] != 'B' || header[1] != 'M') {
		printf("Not a correct BMP file\n");
		return 0;
	}
	// Make sure this is a 24bpp file
	if (*(int*)&(header[0x1E]) != 0) { printf("Not a correct BMP file\n");    return 0; }
	if (*(int*)&(header[0x1C]) != 24) { printf("Not a correct BMP file\n");    return 0; }

	// Read the information about the image
	dataPos = *(int*)&(header[0x0A]);
	imageSize = *(int*)&(header[0x22]);
	width = *(int*)&(header[0x12]);
	height = *(int*)&(header[0x16]);

	// Some BMP files are misformatted, guess missing information
	if (imageSize == 0)    imageSize = width*height * 3; // 3 : one byte for each Red, Green and Blue component
	if (dataPos == 0)      dataPos = 54; // The BMP header is done that way

										 // Create a buffer
	data = new unsigned char[imageSize];

	// Read the actual data from the file into the buffer
	fread(data, 1, imageSize, file);

	// Everything is in memory now, the file wan be closed
	fclose(file);

	// Create one OpenGL texture
	GLuint textureID;
	glGenTextures(1, &textureID);

	// "Bind" the newly created texture : all future texture functions will modify this texture
	glBindTexture(GL_TEXTURE_2D, textureID);

	// Give the image to OpenGL
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_BGR, GL_UNSIGNED_BYTE, data);

	// OpenGL has now copied the data. Free our own version
	delete[] data;

	// Poor filtering, or ...
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST); 

	// ... nice trilinear filtering.
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
	glGenerateMipmap(GL_TEXTURE_2D);

	// Return the ID of the texture we just created
	return textureID;
}

/* Initialize OpenGL Graphics */
void initGL() {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f); // Set background color to black and opaque
	glClearDepth(1.0f);                   // Set background depth to farthest
	glEnable(GL_DEPTH_TEST);   // Enable depth testing for z-culling
	glDepthFunc(GL_LEQUAL);    // Set the type of depth-test
	glShadeModel(GL_SMOOTH);   // Enable smooth shading
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);  // Nice perspective corrections

	rbtop = loadBMP_custom("textures/rbtop.bmp");
	rbleft = loadBMP_custom("textures/rbleft.bmp");
	rbright = loadBMP_custom("textures/rbright.bmp");

	b1top = loadBMP_custom("textures/b1top.bmp");
	b1left = loadBMP_custom("textures/b1left.bmp");
	b1right = loadBMP_custom("textures/b1right.bmp");

	b2top = loadBMP_custom("textures/b2top.bmp");
	b2left = loadBMP_custom("textures/b2left.bmp");
	b2right = loadBMP_custom("textures/b2right.bmp");
}


void showBox(GLuint top, GLuint left, GLuint right)
{
	glColor3f(1.0f, 1.0f, 1.0f);
	glBindTexture(GL_TEXTURE_2D, top);
	glBegin(GL_QUADS);
	glTexCoord2f(1, 1); glVertex3f(0.0f, 0.0f, 1.0f);
	glTexCoord2f(1, 0); glVertex3f(0.0f, 1.0f, 1.0f);
	glTexCoord2f(0, 0); glVertex3f(1.0f, 1.0f, 1.0f);
	glTexCoord2f(0, 1); glVertex3f(1.0f, 0.0f, 1.0f);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, right);
	glBegin(GL_QUADS);
	glTexCoord2f(1, 0); glVertex3f(0.0f, 1.0f, 0.0f);
	glTexCoord2f(0, 0); glVertex3f(1.0f, 1.0f, 0.0f);
	glTexCoord2f(0, 1); glVertex3f(1.0f, 1.0f, 1.0f);
	glTexCoord2f(1, 1); glVertex3f(0.0f, 1.0f, 1.0f);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, left);
	glBegin(GL_QUADS);
	glTexCoord2f(0, 0); glVertex3f(1.0f, 0.0f, 0.0f);
	glTexCoord2f(1, 0); glVertex3f(1.0f, 1.0f, 0.0f);
	glTexCoord2f(1, 1); glVertex3f(1.0f, 1.0f, 1.0f);
	glTexCoord2f(0, 1); glVertex3f(1.0f, 0.0f, 1.0f);
	glEnd();
}

void showTable()
{
	glBegin(GL_QUADS);
	glColor3f(1.0f, 1.0f, 1.0f);
	glVertex3f(-4.0f, -2.0f, 0.1f);
	glVertex3f(+4.0f, -2.0f, 0.1f);
	glVertex3f(+4.0f, +2.0f, 0.1f);
	glVertex3f(-4.0f, +2.0f, 0.1f);
	glEnd();
}

void showGrass()
{
	glBegin(GL_QUADS);
	glColor3f(0.0f, 1.0f, 0.0f);
	glVertex3f(-1000.0f, -500.0f, 0.1f);
	glVertex3f(+1000.0f, -500.0f, 0.1f);
	glVertex3f(+1000.0f, +500.0f, 0.1f);
	glVertex3f(-1000.0f, +500.0f, 0.1f);
	glEnd();
}

/* Handler for window-repaint event. Called back when the window first appears and
whenever the window needs to be re-painted. */

void display() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); // Clear color and depth buffers	
	glMatrixMode(GL_MODELVIEW);     // To operate on model-view matrix

									// Render a color-cube consisting of 6 quads with different colors
	glLoadIdentity();
	// Reset the model-view matrix

	double ang = 35;/// угол камеры к плоскости стола
	double camdist = 6; /// расстояние от камеры до начала коорд
	gluLookAt(0, camdist*cos(ang * 3.141592 / 180.0), camdist*sin(ang * 3.141592 / 180.0), /* look from camera XYZ */
		0, 0, 0, /* look at the origin */
		0, 0, 1); /* positive Y up vector */

				  ///glTranslatef(0.0f, 0.0f, -10.0f);
				  ///glRotatef(angleCube, 1.0f, 1.0f, 1.0f);  // Rotate about (1,1,1)-axis [NEW]
				  ///glRotatef(angleCube, 1.0f, 1.0f, 1.0f);  // Rotate about (1,1,1)-axis [NEW]

	glEnable(GL_TEXTURE_2D);

	showTable();
	showGrass();

	glTranslatef(0, dist, 0); /// перемещение кубов
							  /// rose
	glPushMatrix();
	glRotatef(40, 0.0f, 0.0f, 1.0f);
	showBox(rbtop, rbleft, rbright);
	glPopMatrix();

	/// left
	glPushMatrix();
	glTranslatef(+1.43, -0.77, 0);
	glRotatef(60, 0.0f, 0.0f, 1.0f);
	glScalef(0.5, 0.5, 0.5);/// размер(масштаб)
	showBox(b1top, b1left, b1right);
	glPopMatrix();

	/// right
	glPushMatrix();
	glTranslatef(-1.4, -0.9, 0);
	glRotatef(45, 0.0f, 0.0f, 1.0f);
	glScalef(0.5, 0.5, 0.5);/// размер(масштаб)
	showBox(b2top, b2left, b2right);
	glPopMatrix();



	glDisable(GL_TEXTURE_2D);

	glutSwapBuffers();  // Swap the front and back frame buffers (double buffering)


						// Update the rotational angle after each refresh [NEW]
						///angleCube -= 0.5f;

	dist -= 0.01; /// изменение удаления кубов
}

/* Called back when timer expired */
void timer(int value) {
	glutPostRedisplay();      // Post re-paint request to activate display()
	glutTimerFunc(refreshMills, timer, 0); // next timer call milliseconds later
}

/* Handler for window re-size event. Called back when the window first appears and
whenever the window is re-sized with its new width and height */
void reshape(GLsizei width, GLsizei height) {  // GLsizei for non-negative integer
											   // Compute aspect ratio of the new window
	if (height == 0)
		height = 1;                // To prevent divide by 0

	GLfloat aspect = (GLfloat)width / (GLfloat)height;

	// Set the viewport to cover the new window
	glViewport(0, 0, width, height);

	// Set the aspect ratio of the clipping volume to match the viewport
	glMatrixMode(GL_PROJECTION);  // To operate on the Projection matrix
	glLoadIdentity();             // Reset
								  // Enable perspective projection with fovy, aspect, zNear and zFar
	gluPerspective(40.0f, aspect, 0.1f, 10000.0f);
}

/* Main function: GLUT runs as a console application starting at main() */
int main(int argc, char** argv) {
	glutInit(&argc, argv);            // Initialize GLUT
	glutInitDisplayMode(GLUT_DOUBLE); // Enable double buffered mode
	glutInitWindowSize(800, 600);   // Set the window's initial width & height
	glutInitWindowPosition(50, 50); // Position the window's initial top-left corner
	glutCreateWindow(title);          // Create window with the given title
	glewInit();
	glutDisplayFunc(display);       // Register callback handler for window re-paint event
	glutReshapeFunc(reshape);       // Register callback handler for window re-size event
	initGL();                       // Our own OpenGL initialization
	glutTimerFunc(2000, timer, 0);     // First timer call immediately
	glutMainLoop();                 // Enter the infinite event-processing loop
	return 0;
}