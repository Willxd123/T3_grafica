using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Shapes
{
    public class U2 : BaseShape
    {
        public override  float[] Vertices { get; } = {
            // Cara frontal izquierda
            -1.0f, -1.0f, -1.0f,  1.0f, 0.0f, 0.0f, // 0 
            -0.5f, -1.0f, -1.0f,  1.0f, 0.0f, 0.0f, // 1 
            -0.5f,  1.0f, -1.0f,  1.0f, 0.0f, 0.0f, // 2 
            -1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 0.0f, // 3 

            // Cara trasera izquierda
            -1.0f, -1.0f,  0.5f,  1.0f, 0.5f, 0.0f, // 4 
            -0.5f, -1.0f,  0.5f,  1.0f, 0.5f, 0.0f, // 5 
            -0.5f,  1.0f,  0.5f,  1.0f, 0.5f, 0.0f, // 6 
            -1.0f,  1.0f,  0.5f,  1.0f, 0.5f, 0.0f, // 7 

            // Cara frontal derecha
             0.5f, -1.0f, -1.0f,  0.0f, 0.0f, 1.0f, // 8 
             1.0f, -1.0f, -1.0f,  0.0f, 0.0f, 1.0f, // 9 
             1.0f,  1.0f, -1.0f,  0.0f, 0.0f, 1.0f, //10 
             0.5f,  1.0f, -1.0f,  0.0f, 0.0f, 1.0f, //11 

            // Cara trasera derecha
             0.5f, -1.0f,  0.5f,  0.0f, 1.0f, 1.0f, //12 
             1.0f, -1.0f,  0.5f,  0.0f, 1.0f, 1.0f, //13 
             1.0f,  1.0f,  0.5f,  0.0f, 1.0f, 1.0f, //14 
             0.5f,  1.0f,  0.5f,  0.0f, 1.0f, 1.0f, //15 

            // Base frontal
            -1.0f, -1.0f, -1.0f,  1.0f, 0.0f, 1.0f, //16 
             1.0f, -1.0f, -1.0f,  1.0f, 0.0f, 1.0f, //17 
             1.0f, -0.5f, -1.0f,  1.0f, 0.0f, 1.0f, //18 
            -1.0f, -0.5f, -1.0f,  1.0f, 0.0f, 1.0f, //19 

            // Base trasera
            -1.0f, -1.0f,  0.5f,  0.5f, 0.5f, 0.5f, //20 
             1.0f, -1.0f,  0.5f,  0.5f, 0.5f, 0.5f, //21 
             1.0f, -0.5f,  0.5f,  0.5f, 0.5f, 0.5f, //22 
            -1.0f, -0.5f,  0.5f,  0.5f, 0.5f, 0.5f  //23 
        };

        public override  uint[] Indices { get; } = {
            // Caras columna izquierda
            0, 1, 2, 2, 3, 0,   // Cara frontal 
            4, 5, 6, 6, 7, 4,   // Cara trasera 
            0, 1, 5, 5, 4, 0,   // Cara inferior 
            2, 3, 7, 7, 6, 2,   // Cara superior 
            0, 3, 7, 7, 4, 0,   // Cara izquierda 
            1, 2, 6, 6, 5, 1,   // Cara derecha 

            // Caras columna derecha
            8, 9, 10, 10, 11, 8,   // Cara frontal 
            12, 13, 14, 14, 15, 12, // Cara trasera 
            8, 9, 13, 13, 12, 8,   // Cara inferior 
            10, 11, 15, 15, 14, 10, // Cara superior 
            8, 11, 15, 15, 12, 8,   // Cara izquierda 
            9, 10, 14, 14, 13, 9,   // Cara derecha 

            // Caras base
            16, 17, 18, 18, 19, 16, // Cara frontal 
            20, 21, 22, 22, 23, 20, // Cara trasera 
            16, 17, 21, 21, 20, 16, // Cara inferior 
            18, 19, 23, 23, 22, 18, // Cara superior 
            16, 19, 23, 23, 20, 16, // Cara izquierda 
            17, 18, 22, 22, 21, 17  // Cara derecha 
        };
    }
}