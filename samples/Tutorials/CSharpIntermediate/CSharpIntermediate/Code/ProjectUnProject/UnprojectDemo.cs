using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Graphics;
using Stride.Input;
using Stride.Physics;

namespace CSharpIntermediate.Code
{
    public class UnprojectDemo : SyncScript
    {
        public Vector3 impulse = new(0, 0, 5.0f);
        public CameraComponent camera;
        public Entity ball;
        public Entity ball2;

        private bool applyPulse = false;

        public override void Start()
        {
            //camera = Entity.Get<CameraComponent>();
        }

        public override void Update()
        {
            if (applyPulse)
            {
              
                impulse = Vector3.Transform(impulse, Entity.Transform.Rotation);

                //rigidBody.Activate();
                //rigidBody.ApplyImpulse(impulse);
                applyPulse = false;
            }

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                var backBuffer = GraphicsDevice.Presenter.BackBuffer;
                var mousePosition = Input.AbsoluteMousePosition;
                var viewport = new Viewport(0, 0, backBuffer.Width, backBuffer.Height);

                var positionNearCamera = viewport.Unproject(new Vector3(mousePosition, -4), camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);
                var positionFarFromCamera = viewport.Unproject(new Vector3(mousePosition, 4f), camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);


                var childBall2d = Vector3.Unproject(new Vector3(mousePosition, -4), 0, 0, backBuffer.Width, backBuffer.Height, -4, 4, Entity.Transform.WorldMatrix);
                var globalBall2d = Vector3.Unproject(new Vector3(mousePosition, 4), 0, 0, backBuffer.Width, backBuffer.Height, -4, 4, Entity.Transform.WorldMatrix);

                //var positionNearCamera = viewport.Unproject(new Vector3(mousePosition, 0), ref Entity.Transform.WorldMatrix);
                //var positionFarFromCamera = viewport.Unproject(new Vector3(mousePosition, 2), ref Entity.Transform.WorldMatrix);


                ball.Transform.Position = childBall2d;
                ball2.Transform.Position = globalBall2d;


                var rigidBody = ball.Get<RigidbodyComponent>();
                rigidBody.Mass = 0;
                var rigidBody2 = ball2.Get<RigidbodyComponent>();
                rigidBody2.Mass = 0;

                //applyPulse = true;




                //var positionNearCamera = Vector3.Unproject(new Vector3(mousePosition, 0), 0, 0, backBuffer.Width, backBuffer.Height,0, 2, camera.ProjectionMatrix);
                //var positionFarFromCamera = Vector3.Unproject(new Vector3(mousePosition, 2), 0, 0, backBuffer.Width, backBuffer.Height, 0, 2, camera.ProjectionMatrix);



                //childBall.Transform.Position = positionFarFromCamera;
                //globalBall.Transform.Position = positionFarFromCamera;




                //DebugText.Print($"Camera pos {Entity.Transform.Position.Print()}", new Int2(500, 20));
                //DebugText.Print($"Mousepos {mousePosition.Print()}", new Int2(500, 40));
                ////DebugText.Print($"MousePos abs {mousePositionAbs.Print()}", new Int2(500, 60));
                //DebugText.Print($"positionNearCamera {positionNearCamera.Print()}", new Int2(500, 80));
                //DebugText.Print($"positionFarFromCamera {positionFarFromCamera.Print()}", new Int2(500, 100));


                //DebugText.Print($"childBall.Transform.Position {childBall.Transform.Position.Print()}", new Int2(500, 140));
                //DebugText.Print($"globalBall.Transform.Position {globalBall.Transform.Position.Print()}", new Int2(500, 160));
            }
        }
    }
}
