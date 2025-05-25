using ARC.Tests.Common.Layers;

namespace ARC.API.Tests.Layers
{
    public class APILayerDependencyTests : ArchitectureTestsBase
    {
        [Theory]
        [InlineData(ApplicationAssemblyName)]
        [InlineData(PersistenceAssemblyName)]
        [InlineData(InfrastructureAssemblyName)]
        public void API_ShouldDependOn(string AssemblyName)
        {
            AssertHasDependency(APIAssemblyName, AssemblyName);
        }
    }
}


