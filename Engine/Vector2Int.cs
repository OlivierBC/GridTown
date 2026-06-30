using System.Numerics;

namespace GridTown.Engine
{
    internal struct Vector2Int
    {
        public int X, Y;

        public Vector2Int(int x, int y)
        {
            X = x; Y = y;
        }

        public Vector2Int() : this(0, 0) { }

        public Vector2Int(Vector2 vector) : this((int)vector.X, (int)vector.Y) { }

        public static int DistanceSquared(Vector2Int v1, Vector2Int v2)
        {
            int dx = v2.X - v1.X;
            int dy = v2.Y - v1.Y;

            return dx * dx + dy * dy;
        }

        public override string ToString()
        {
            return "<" + X + " , " + Y + ">";
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2Int vInt &&
                   X == vInt.X &&
                   Y == vInt.Y;
        }

        public static bool operator ==(Vector2Int left, Vector2Int right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
