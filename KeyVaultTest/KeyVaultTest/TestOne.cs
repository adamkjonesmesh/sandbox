using Xunit;

namespace KeyVaultTest
{
    public class TestOne : IClassFixture<GenericStartupFixture>
    {
        GenericStartupFixture _fixture;

        public TestOne(GenericStartupFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShowValueOne()
        {
            Assert.Equal("MeshDev", _fixture.EnvironmentContext);
        }
    }
}
