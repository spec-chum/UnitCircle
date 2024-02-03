using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace UnitCircle;

internal static class Program
{
    const float ToDeg = 180f / MathF.PI;
    const int Width = 800;
    const int Height = 800;
    const float Radius = 300;
    const int AxisMargin = 80;

    static readonly Vector2 centre = new(Height / 2, Width / 2);

    static void Main()
    {
        InitWindow(Width, Height, "Unit Circle");
        SetTargetFPS(60);

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Black);

            // Draw the unit circle and axis
            DrawCircleLines((int)centre.X, (int)centre.Y, Radius, Color.Red);
            DrawLine((int)centre.X, AxisMargin, (int)centre.X, Height - AxisMargin, Color.Purple);
            DrawLine(AxisMargin, (int)centre.Y, Width - AxisMargin, (int)centre.Y, Color.Purple);

            // Calculate the hypotenuse and the x-axis end point
            Vector2 mouse = GetMousePosition();
            Vector2 dir = mouse - centre;
            Vector2 hyp = centre + (Vector2.Normalize(dir) * Radius);
            Vector2 xEndPoint = new(hyp.X, centre.Y);

            // Draw the triangle from centre to mouse coords
            DrawLineV(centre, hyp, Color.White);
            DrawLineV(centre, xEndPoint, Color.Blue);
            DrawLineV(hyp, xEndPoint, Color.Green);

            // Draw the (normalised) angle as an arc
            float angle = (MathF.Atan2(-dir.Y, dir.X) + (2 * MathF.PI)) % (2 * MathF.PI);
            DrawCircleSectorLines(centre, Radius / 4, 0, -angle * ToDeg, 32, Color.Purple);

            // Display coords and angles
            var (sin, cos) = MathF.SinCos(angle);
            DrawText($"({cos:F2}, {sin:F2})", (int)hyp.X, (int)hyp.Y, 16, Color.White);
            DrawText($"Rad: {angle:F2}\nDeg: {angle * ToDeg:F2}°", (int)centre.X, (int)centre.Y - 16, 16, Color.White);

            EndDrawing();
        }

        CloseWindow();
    }
}
