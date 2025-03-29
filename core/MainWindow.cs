using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Core;
using Shapes;

namespace Proyecto3D
{
    public class MainWindow : GameWindow
    {
        private readonly ObjectManager _objectManager;
        private readonly ShaderManager _shaderManager = new();
        private float _rotation = 0.0f;
        private Vector3 _cameraPosition = new Vector3(0, 0, 3);
        private float _zoom = 45.0f;

        public MainWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Size = new Vector2i(800, 600);
            Title = "3D Shapes with OpenTK";
            _objectManager = new ObjectManager(_shaderManager);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _shaderManager.LoadShaders();

            // Agregar figuras
            _objectManager.AddShape(new UShape()
            {
                Position = new Vector3(0, 0, 0),
                Scale = new Vector3(0.5f)
            });

            // Puedes agregar más figuras aquí
            // Nuevo cubo
            _objectManager.AddShape(new CubeShape()
            {
                Position = new Vector3(1, 0, 0),
                Scale = new Vector3(0.5f),
                Rotation = new Vector3(0, 45, 0) // Rotación opcional
            });
            
            _objectManager.AddShape(new U2()
            {
                Position = new Vector3(-1, 0.5f, 0),
                Scale = new Vector3(0.9f),
                Rotation = new Vector3(0, 0, 0) // Rotación opcional
            });

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = Matrix4.LookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(_zoom), Size.X / (float)Size.Y, 0.1f, 100.0f);

            _objectManager.RenderAll(view, projection);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            _rotation += (float)args.Time;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            base.OnUnload();
        }
    }
}