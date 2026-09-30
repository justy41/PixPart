public struct Particle {
    public float X, Y, VX, VY;
    public float Life, MaxLife;
    public float Size, Rotation;
    public int TextureID;
    public int R0, G0, B0, A0, R1, G1, B1, A1;
    
    public readonly float AgeFraction() {
        if(MaxLife <= 0) return 1f;
        float fraction = 1f-(Life/MaxLife);
        
        if(fraction < 0) return 0;
        if(fraction > 1) return 1;
        return fraction;
    }
}