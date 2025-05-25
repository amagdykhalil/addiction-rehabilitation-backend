using ARC.Tests.Common.Layers;

namespace ARC.Infrastructure.Tests.Layers
{
    public class InfrastructureLayerDependencyTests : ArchitectureTestsBase
    {
        [Theory]
        [InlineData(ApplicationAssemblyName)]
        [InlineData(DomainAssemblyName)]
        public void Infrastructure_ShouldDependOn(string AssemblyName)
        {
            AssertHasDependency(InfrastructureAssemblyName, AssemblyName);
        }

        [Theory]
        [InlineData(APIAssemblyName)]
        [InlineData(PersistenceAssemblyName)]
        public void Infrastructure_ShouldNotDependOn(string AssemblyName)
        {
            AssertNoDependency(InfrastructureAssemblyName, AssemblyName);
        }
    }
}


