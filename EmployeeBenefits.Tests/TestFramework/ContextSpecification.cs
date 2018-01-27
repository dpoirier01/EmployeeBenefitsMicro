using NUnit.Framework;

namespace EmployeeBenefits.Tests.TestFramework
{
    public abstract class ContextSpecification

    {

        private TestContext testContextInstance;

        public TestContext TestContext

        {

            get { return this.testContextInstance; }

            set { this.testContextInstance = value; }

        }

        [SetUp]

        public void TestInitialize()

        {

            this.Context();

            this.BecauseOf();

        }

        [TearDown]

        public void TestCleanup()

        {

            this.Cleanup();

        }

        protected virtual void Context()

        {

        }

        protected virtual void BecauseOf()

        {

        }

        protected virtual void Cleanup()

        {

        }
    }
}
