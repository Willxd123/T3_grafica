using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Core;
using Shapes;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Proyecto3D
{
    public class MainWindow : GameWindow
    {
        private readonly ObjectManager _objectManager;
        private readonly ShaderManager _shaderManager = new();
        private float _rotation = 0.0f;
        private Vector3 _cameraPosition = new Vector3(0, 0, 3);
        private float _zoom = 45.0f;
        private float _moveSpeed = 2.5f; // Añadido
        private int _selectedObjectIndex = 0; // Añadido

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

            _objectManager.AddShape(new UShape()
            {
                Position = new Vector3(0, 0, 0),
                Scale = new Vector3(0.5f)
            });

            _objectManager.AddShape(new CubeShape()
            {
                Position = new Vector3(1, 0, 0),
                Scale = new Vector3(0.5f),
                Rotation = new Vector3(0, 45, 0)
            });

            _objectManager.AddShape(new U2()
            {
                Position = new Vector3(-1, 0.5f, 0),
                Scale = new Vector3(0.9f),
                Rotation = new Vector3(0, 0, 0)
            });
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
{
    base.OnUpdateFrame(args);
    
    var keyboardState = KeyboardState;
    var time = (float)args.Time;
    
    // Rotación continua (opcional)
    _rotation += (float)args.Time;
    
    // Cambiar objeto seleccionado con teclas numéricas
    if (keyboardState.IsKeyDown(Keys.D1)) _selectedObjectIndex = 0;
    if (keyboardState.IsKeyDown(Keys.D2)) _selectedObjectIndex = 1;
    if (keyboardState.IsKeyDown(Keys.D3)) _selectedObjectIndex = 2;
    
    // Mover el objeto seleccionado
    if (_objectManager.Shapes.Count > _selectedObjectIndex)
    {
        var selectedObject = _objectManager.Shapes[_selectedObjectIndex];
        var newPosition = selectedObject.Position;
        
        if (keyboardState.IsKeyDown(Keys.W)) newPosition.Z -= _moveSpeed * time;
        if (keyboardState.IsKeyDown(Keys.S)) newPosition.Z += _moveSpeed * time;
        if (keyboardState.IsKeyDown(Keys.A)) newPosition.X -= _moveSpeed * time;
        if (keyboardState.IsKeyDown(Keys.D)) newPosition.X += _moveSpeed * time;
        if (keyboardState.IsKeyDown(Keys.Q)) newPosition.Y -= _moveSpeed * time;
        if (keyboardState.IsKeyDown(Keys.E)) newPosition.Y += _moveSpeed * time;
        
        selectedObject.Position = newPosition;
    }
    
    // Control de cámara/zoom
    if (keyboardState.IsKeyDown(Keys.Up)) _zoom -= 1f;
    if (keyboardState.IsKeyDown(Keys.Down)) _zoom += 1f;
    
    _zoom = MathHelper.Clamp(_zoom, 1.0f, 90.0f);
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

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _zoom -= e.OffsetY;
            _zoom = MathHelper.Clamp(_zoom, 1.0f, 90.0f);
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