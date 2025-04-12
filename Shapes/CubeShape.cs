using OpenTK.Mathematics;

namespace Shapes
{
    public class CubeShape : BaseShape
    {
        public override float[] Vertices { get; } = {
            // Cara frontal (Z = -1)
            -0.5f, -0.5f, -0.5f,  1.0f, 0.0f, 0.0f, // 0
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f, 0.0f, // 1
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 0.0f, // 2
            -0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 0.0f, // 3
            
            // Cara trasera (Z = 1)
            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 4
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 5
             0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 6
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 7

            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 4
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 5
             0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 6
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // 7
        };

        public override uint[] Indices { get; } = {
            // Cara frontal
            0, 1, 2, 2, 3, 0,
            // Cara trasera
            4, 5, 6, 6, 7, 4,
            // Caras laterales
            0, 3, 7, 7, 4, 0,
            1, 2, 6, 6, 5, 1,
            // Caras superior/inferior
            0, 1, 5, 5, 4, 0,
            2, 3, 7, 7, 6, 2
        };
    }
}