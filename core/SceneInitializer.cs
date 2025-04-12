using Core;
using OpenTK.Mathematics;
using Shapes;

namespace Proyecto3D
{
    public class SceneInitializer
    {
        private readonly ObjectManager _objectManager;

        public SceneInitializer(ObjectManager objectManager)
        {
            _objectManager = objectManager;
        }

        public void InitializeScene()
        {
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
                Scale = new Vector3(0.5f),
                Rotation = new Vector3(0, 0, 0)
            });
        }
    }
}