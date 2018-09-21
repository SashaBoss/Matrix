namespace Matrix.App_Start
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Matrix.Interfaces;
    using Matrix.Services;
    using System.Web.Mvc;

    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<MatrixService>().As<IMatrixService>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}