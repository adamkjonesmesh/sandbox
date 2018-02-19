using Xunit;

namespace KeyVaultTest
{
    public class TestFive : IClassFixture<GenericStartupFixture>
    {
        GenericStartupFixture _fixture;

        public TestFive(GenericStartupFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShowValueFive()
        {
            Assert.Equal("MeshDev", _fixture.EnvironmentContext);
        }
    }
}
