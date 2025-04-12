using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Core;

namespace Proyecto3D
{
    public class ObjectController
    {
        private readonly ObjectManager _objectManager;
        private int _selectedObjectIndex = 0;
        private float _moveSpeed = 2.5f;
        private float _rotation = 0.0f;
        private float _zoom = 45.0f;

        public float Zoom => _zoom; // Getter para que MainWindow use la proyección
        public float Rotation => _rotation;

        public ObjectController(ObjectManager objectManager)
        {
            _objectManager = objectManager;
        }

        public void Update(KeyboardState keyboardState, float deltaTime)
        {
            // Rotación continua
            _rotation += deltaTime;

            // Cambiar objeto seleccionado con teclas numéricas
            if (keyboardState.IsKeyDown(Keys.D1)) _selectedObjectIndex = 0;
            if (keyboardState.IsKeyDown(Keys.D2)) _selectedObjectIndex = 1;
            if (keyboardState.IsKeyDown(Keys.D3)) _selectedObjectIndex = 2;

            // Mover objeto seleccionado
            if (_objectManager.Shapes.Count > _selectedObjectIndex)
            {
                var selectedObject = _objectManager.Shapes[_selectedObjectIndex];
                var newPosition = selectedObject.Position;

                if (keyboardState.IsKeyDown(Keys.W)) newPosition.Z -= _moveSpeed * deltaTime;
                if (keyboardState.IsKeyDown(Keys.S)) newPosition.Z += _moveSpeed * deltaTime;
                if (keyboardState.IsKeyDown(Keys.A)) newPosition.X -= _moveSpeed * deltaTime;
                if (keyboardState.IsKeyDown(Keys.D)) newPosition.X += _moveSpeed * deltaTime;
                if (keyboardState.IsKeyDown(Keys.Q)) newPosition.Y -= _moveSpeed * deltaTime;
                if (keyboardState.IsKeyDown(Keys.E)) newPosition.Y += _moveSpeed * deltaTime;

                selectedObject.Position = newPosition;
            }

            // Control de zoom
            if (keyboardState.IsKeyDown(Keys.Up)) _zoom -= 1f;
            if (keyboardState.IsKeyDown(Keys.Down)) _zoom += 1f;

            _zoom = MathHelper.Clamp(_zoom, 5.0f, 100.0f);
        }

        public void OnMouseWheel(float offsetY)
        {
            _zoom -= offsetY;
            _zoom = MathHelper.Clamp(_zoom, 1.0f, 90.0f);
        }
        
    }
}
