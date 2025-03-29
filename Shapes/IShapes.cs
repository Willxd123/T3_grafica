using OpenTK.Mathematics;

namespace Shapes
{
    public interface IShape
    {
        float[] Vertices { get; }
        uint[] Indices { get; }
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        Vector3 Scale { get; set; }
        int VertexArrayObject { get; set; }
        int VertexBufferObject { get; set; }
        int ElementBufferObject { get; set; }
        void SetupBuffers();
    }
}