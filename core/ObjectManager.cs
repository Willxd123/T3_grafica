using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Shapes;
using System.Collections.Generic;

namespace Core
{
    public class ObjectManager
    {
        private readonly List<BaseShape> _shapes = new();
        private readonly ShaderManager _shaderManager;

        public ObjectManager(ShaderManager shaderManager)
        {
            _shaderManager = shaderManager;
        }

        // Expone la lista de formas como propiedad pública de solo lectura
        public IReadOnlyList<BaseShape> Shapes => _shapes.AsReadOnly();

        public void AddShape(BaseShape shape)
        {
            shape.SetupBuffers();
            _shapes.Add(shape);
        }

        public void RenderAll(Matrix4 view, Matrix4 projection)
        {
            GL.UseProgram(_shaderManager.ShaderProgram);

            foreach (var shape in _shapes)
            {
                Matrix4 model = Matrix4.CreateScale(shape.Scale) *
                              Matrix4.CreateRotationX(shape.Rotation.X) *
                              Matrix4.CreateRotationY(shape.Rotation.Y) *
                              Matrix4.CreateRotationZ(shape.Rotation.Z) *
                              Matrix4.CreateTranslation(shape.Position);

                GL.UniformMatrix4(GL.GetUniformLocation(_shaderManager.ShaderProgram, "model"), false, ref model);
                GL.UniformMatrix4(GL.GetUniformLocation(_shaderManager.ShaderProgram, "view"), false, ref view);
                GL.UniformMatrix4(GL.GetUniformLocation(_shaderManager.ShaderProgram, "projection"), false, ref projection);

                GL.BindVertexArray(shape.VertexArrayObject);
                GL.DrawElements(PrimitiveType.Triangles, shape.Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
        }
    }
}