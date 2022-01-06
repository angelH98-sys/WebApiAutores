namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        Guid ObtenerTranscient();
        void realizarTarea();
    }

    public class IServicioA : IServicio
    {
        private readonly ILogger<IServicioA> logger;
        private readonly ServicioTranscient servicioTrancient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;

        public IServicioA(ILogger<IServicioA> logger, ServicioTranscient servicioTrancient,
            ServicioScoped servicioScoped, ServicioSingleton servicioSingleton)
        {
            this.logger = logger;
            this.servicioTrancient = servicioTrancient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }

        public Guid ObtenerTranscient() { return servicioTrancient.Guid; } 
        public Guid ObtenerScoped() { return servicioScoped.Guid; } 
        public Guid ObtenerSingleton() { return servicioSingleton.Guid; } 

        public void realizarTarea()
        {
            throw new NotImplementedException();
        }
    }
    public class IServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTranscient()
        {
            throw new NotImplementedException();
        }

        public void realizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioTranscient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}
