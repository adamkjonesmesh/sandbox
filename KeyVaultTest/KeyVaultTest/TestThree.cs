using Xunit;

namespace KeyVaultTest
{
    public class TestThree : IClassFixture<GenericStartupFixture>
    {
        GenericStartupFixture _fixture;

        public TestThree(GenericStartupFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShowValueThree()
        {
            Assert.Equal("MeshDev", _fixture.EnvironmentContext);
        }
    }
}
