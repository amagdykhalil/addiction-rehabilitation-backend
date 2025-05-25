using ARC.Tests.Common.Layers;

namespace ARC.Domain.Tests.Layers
{
    public class DomainLayerDependencyTests : ArchitectureTestsBase
    {
        [Theory]
        [InlineData(APIAssemblyName)]
        [InlineData(InfrastructureAssemblyName)]
        [InlineData(PersistenceAssemblyName)]
        [InlineData(ApplicationAssemblyName)]
        public void Domain_ShouldNotDependOn(string AssemblyName)
        {
            AssertNoDependency(DomainAssemblyName, AssemblyName);
        }

    }
}


