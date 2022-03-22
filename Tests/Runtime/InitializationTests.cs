using NUnit.Framework;

namespace ComradeVanti.EnumDict
{

    [TestFixture]
    public class InitializationTests
    {

        [Test]
        public void Default_Constructor_Adds_Entry_For_Each_Enum_Value()
        {
            var dict = new EnumDict<Colors, int>();
            foreach (var color in EnumUtil.GetEnumValues<Colors>())
                Assert.AreEqual(0, dict.Get(color));
        }

    }

}