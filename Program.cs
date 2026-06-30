using GridTown.Engine.GameObjects.Movements;
using GridTown.Engine.Observers;
using Raylib_cs;

Raylib.InitWindow(1240, 720, "Grid Town");

Camera gameCamera = new Camera();

while (!Raylib.WindowShouldClose())
{
    float dt = Raylib.GetFrameTime();

    Observers.Update(dt);

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    Raylib.BeginMode3D(Camera.instance);

    Observers.Draw();

    Raylib.DrawGrid(36, 1f);

    Raylib.EndMode3D();

    Observers.DrawUI();

    Raylib.DrawText("Hold right click to rotate camera", 20, 20, 20, Color.RayWhite);
    Raylib.DrawFPS(Raylib.GetScreenWidth() - 150, 10);

    Raylib.EndDrawing();
}

Observers.Unload();

Raylib.CloseWindow();