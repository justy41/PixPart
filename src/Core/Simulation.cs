public class Simulation {
    public ParticlePool Pool {get;}
    public EmitterConfig Config {get; set;}
    public float _spawnAccumulator;
    private Random _rng;
    
    public Simulation(EmitterConfig config, int maxParticles, int? seed = null) {
        Config = config;
        Pool = new ParticlePool(maxParticles);
        _rng = seed.HasValue ? new Random(seed.Value) : new Random();
        _spawnAccumulator = 0;
    }
    
    public void Update(float deltaTime) {
        SpawnNewParticles(deltaTime);
        UpdateExistingParticles(deltaTime);
    }
    
    private void SpawnNewParticles(float deltaTime) {
        _spawnAccumulator += Config.EmissionRate * deltaTime;
        
        while(_spawnAccumulator >= 1) {
            SpawnOne();
            _spawnAccumulator -= 1;
        }
    }
    
    private void SpawnOne() {
        float angleDegrees = Lerp(Config.AngleMin, Config.AngleMax, (float)_rng.NextDouble());
        float speed = Lerp(Config.SpeedMin, Config.SpeedMax, (float)_rng.NextDouble());
        
        float angleRadians = angleDegrees*MathF.PI / 180f;
        float vx = speed * MathF.Cos(angleRadians);
        float vy = speed * MathF.Sin(angleRadians);
        
        float maxLife = Lerp(Config.LifetimeMin, Config.LifetimeMax, (float)_rng.NextDouble());
        float size = Lerp(Config.SizeMin, Config.SizeMax, (float)_rng.NextDouble());
        
        var particle = new Particle {
            X = 426, Y = 360,
            VX = vx, VY = vy,
            Life = maxLife,
            MaxLife = maxLife,
            Size = size,
            Rotation = 0,
            TextureID = Config.TextureID,
            R0 = Config.StartR, G0 = Config.StartG, B0 = Config.StartB, A0 = Config.StartA,
            R1 = Config.EndR, G1 = Config.EndG, B1 = Config.EndB, A1 = Config.EndA,
        };
        
        Pool.Spawn(particle);
    }
    
    private void UpdateExistingParticles(float deltaTime) {
        var active = Pool.Active;
        
        for(int i = active.Length-1; i >= 0; i--) {
            ref Particle p = ref active[i];
            
            p.VY += Config.Gravity * deltaTime;
            p.X += p.VX * deltaTime;
            p.Y += p.VY * deltaTime;
            p.Life -= deltaTime;
            
            if(p.Life <= 0 || ((p.X > Utils.WindowWidth || p.X < 0) || (p.Y > Utils.WindowHeight || p.Y < 0))) {
                Pool.KillAt(i);
            }
        }
    }
    
    public void Reset() {
        Pool.Clear();
        _spawnAccumulator = 0;
    }
    
    private static float Lerp(float a, float b, float t) => a+(b-a)*t;
}