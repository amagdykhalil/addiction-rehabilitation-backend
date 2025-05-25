using ARC.Tests.Common.Layers;

namespace ARC.Application.Tests.Layers
{
    public class ApplicationLayerDependencyTests : ArchitectureTestsBase
    {
        [Theory]
        [InlineData(DomainAssemblyName)]
        public void Application_ShouldDependOn(string AssemblyName)
        {
            AssertHasDependency(ApplicationAssemblyName, AssemblyName);
        }

        [Theory]
        [InlineData(APIAssemblyName)]
        [InlineData(InfrastructureAssemblyName)]
        [InlineData(PersistenceAssemblyName)]
        public void Application_ShouldNotDependOn(string AssemblyName)
        {
            AssertNoDependency(ApplicationAssemblyName, AssemblyName);
        }

    }
}


