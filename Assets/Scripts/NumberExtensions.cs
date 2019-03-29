public static class NumberExtensions {
    public static int Mod(this int x, int m) {
        return (x % m + m) % m;
    }
}