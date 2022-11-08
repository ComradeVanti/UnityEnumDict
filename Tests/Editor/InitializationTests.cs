using System.Collections.Generic;
using NUnit.Framework;

namespace Dev.ComradeVanti.EnumDict
{

    [TestFixture]
    public class InitializationTests
    {

        private enum ColorName
        {

            Red,
            Green,
            Blue,
            Yellow

        }


        private static readonly ColorName[] colors =
        {
            ColorName.Red,
            ColorName.Green,
            ColorName.Blue,
            ColorName.Yellow
        };


        [Test]
        public void DefaultConstructorMakesEachValueDefault()
        {
            var enumDict = new EnumDict<ColorName, int>();
            const int expected = 0;

            foreach (var color in colors)
            {
                var actual = enumDict[color];
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void DictionaryConstructorAppliesGivenValues()
        {
            var dict = new Dictionary<ColorName, int>
            {
                { ColorName.Red, 1 },
                { ColorName.Blue, 3 }
            };
            var enumDict = new EnumDict<ColorName, int>(dict);

            Assert.AreEqual(1, enumDict[ColorName.Red]);
            Assert.AreEqual(3, enumDict[ColorName.Blue]);
        }
        
        [Test]
        public void DictionaryConstructorLeavesNonGivenValuesAtDefault()
        {
            var dict = new Dictionary<ColorName, int>
            {
                { ColorName.Red, 1 },
                { ColorName.Blue, 3 }
            };
            var enumDict = new EnumDict<ColorName, int>(dict);

            Assert.AreEqual(0, enumDict[ColorName.Green]);
            Assert.AreEqual(0, enumDict[ColorName.Yellow]);
        }

    }

}