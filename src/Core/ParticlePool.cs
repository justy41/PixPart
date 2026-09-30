public class ParticlePool {
    private Particle[] _particles;
    public int ActiveCount {get; private set;}
    public int Capacity => _particles.Length;
    
    public ParticlePool(int maxParticles) {
        _particles = new Particle[maxParticles];
        ActiveCount = 0;
    }
    
    public void Spawn(Particle p) {
        if(ActiveCount >= _particles.Length) {
            return;
        }
        
        _particles[ActiveCount] = p;
        ActiveCount++;
    }
    
    public void KillAt(int index) {
        if(index < 0 || index >= ActiveCount) {
            return;
        }
        
        int lastActiveIndex = ActiveCount-1;
        _particles[index] = _particles[lastActiveIndex];
        ActiveCount--;
    }
    
    public Span<Particle> Active => _particles.AsSpan(0, ActiveCount);
    
    public void Clear() {
        ActiveCount = 0;
    }
}