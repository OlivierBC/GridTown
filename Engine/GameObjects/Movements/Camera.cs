using GridTown.Engine.Observers.Observables;
using Raylib_cs;
using System.Numerics;

namespace GridTown.Engine.GameObjects.Movements
{
    internal class Camera : Entity, IUpdatable
    {
        float moveSpeed;
        float rotationSpeed;

        float yaw;
        float pitch;

        Vector2 mousePosBeforeDisable = new();

        public static Camera3D instance = new()
        {
            Position = new Vector3(0, 6, 0),
            Target = new Vector3(0, 6, 1),
            Up = Vector3.UnitY,
            FovY = 45,
            Projection = CameraProjection.Perspective
        };

        public Camera(float moveSpeed = 10f, float rotationSpeed = 0.003f)
        {
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;

            position = instance.Position;
        }

        public void Update(float dt)
        {
            CameraRotation();

            Vector3 forward = GetForward();
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));

            Vector3 flatForward = new Vector3(forward.X, 0, forward.Z);

            if (flatForward.LengthSquared() > 0)
                flatForward = Vector3.Normalize(flatForward);

            Vector2 m = Movement();

            if (m.LengthSquared() > 0)
                m = Vector2.Normalize(m);

            position += moveSpeed * dt * ((flatForward * m.Y) - (right * m.X));

            if (Raylib.IsKeyDown(KeyboardKey.Space))
                position += Vector3.UnitY * moveSpeed * dt;

            if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
                position -= Vector3.UnitY * moveSpeed * dt;

            instance.Position = position;
            instance.Target = position + forward;
            instance.Up = Vector3.UnitY;
        }

        Vector2 Movement()
        {
            Vector2 m = new();
            m.X += Raylib.IsKeyDown(KeyboardKey.A);
            m.X -= Raylib.IsKeyDown(KeyboardKey.D);
            m.Y += Raylib.IsKeyDown(KeyboardKey.W);
            m.Y -= Raylib.IsKeyDown(KeyboardKey.S);
            return m;
        }

        void CameraRotation()
        {
            SetCursorState(out bool canRotate);

            if (!canRotate)
                return;

            Vector2 mouseDelta = Raylib.GetMouseDelta();

            yaw -= mouseDelta.X * rotationSpeed;
            pitch -= mouseDelta.Y * rotationSpeed;

            float limit = MathF.PI / 2f - 0.01f;

            pitch = Math.Clamp(pitch, -limit, limit);
        }

        void SetCursorState(out bool canRotate)
        {
            if (Raylib.IsMouseButtonReleased(MouseButton.Right))
            {
                Vector2Int v = new Vector2Int(mousePosBeforeDisable);
                Raylib.EnableCursor();
                Raylib.SetMousePosition(v.X, v.Y);
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                mousePosBeforeDisable = Raylib.GetMousePosition();
                Raylib.DisableCursor();
                Console.WriteLine(mousePosBeforeDisable);
            }

            canRotate = Raylib.IsMouseButtonDown(MouseButton.Right);
        }

        Vector3 GetForward()
        {
            return Vector3.Normalize(new Vector3(
                MathF.Sin(yaw) * MathF.Cos(pitch),
                MathF.Sin(pitch),
                MathF.Cos(yaw) * MathF.Cos(pitch)
            ));
        }
    }
}