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
        private readonly ObjectManager _objectManager; // Administra y renderiza los objetos de la escena
        private readonly ShaderManager _shaderManager = new(); // Administra los shaders utilizados en la escena
        private ObjectController _objectController; // Controla el comportamiento de los objetos (movimiento, rotación, zoom)
        private SceneInitializer _sceneInitializer; // Inicializa la escena con los objetos necesarios

        private Vector3 _cameraPosition = new Vector3(0, 0, 3); // Posición inicial de la cámara

        public MainWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            // Configuración inicial de la ventana
            Size = new Vector2i(800, 600); // Tamaño de la ventana
            Title = "3D Shapes with OpenTK"; // Título de la ventana
            _objectManager = new ObjectManager(_shaderManager); // Inicializa el administrador de objetos
            _sceneInitializer = new SceneInitializer(_objectManager); // Inicializa la escena
            _objectController = new ObjectController(_objectManager); // Inicializa el controlador de objetos
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Configuración inicial de OpenGL
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // Color de fondo
            GL.Enable(EnableCap.DepthTest); // Habilita el test de profundidad
            _shaderManager.LoadShaders(); // Carga los shaders necesarios

            // Inicializa la escena con los objetos definidos
            _sceneInitializer.InitializeScene();

            // Inicializa el controlador de objetos
            _objectController = new ObjectController(_objectManager);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _objectController.OnMouseWheel(e.OffsetY);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Actualiza la lógica de los objetos (movimiento, rotación, zoom) en cada frame
            _objectController.Update(KeyboardState, (float)args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpia el buffer de color y profundidad para preparar el frame actual
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Calcula la matriz de vista (posición y orientación de la cámara)
            Matrix4 view = Matrix4.LookAt(_cameraPosition, Vector3.Zero, Vector3.UnitY);

            // Calcula la matriz de proyección (perspectiva de la cámara)
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(_objectController.Zoom),
                Size.X / (float)Size.Y,
                0.1f, 100.0f);

            // Renderiza todos los objetos de la escena usando las matrices de vista y proyección
            _objectManager.RenderAll(view, projection);

            // Intercambia los buffers para mostrar el frame renderizado en la ventana
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Ajusta el área de renderizado al nuevo tamaño de la ventana
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            // Libera los recursos de OpenGL al cerrar la aplicación
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            base.OnUnload();
        }
    }
}
