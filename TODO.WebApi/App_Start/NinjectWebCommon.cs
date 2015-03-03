using System.Reflection;
using Ninject;
using Ninject.Modules;
using TODO.Data.Assignments;
using TODO.Domain.Services.Assignments;

namespace TODO.WebApi
{
    public class NinjectWebCommon
    {
        public static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }

    public class ServiceMododule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssignmentService>().To<AssignmentService>();
            Bind<IAssignmentRepository>().To<AssignmentRepository>();
        }
    }
}