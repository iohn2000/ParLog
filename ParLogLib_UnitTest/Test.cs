using NUnit.Framework;
using System;
using Moq;
using ParLog;
using System.Collections.Generic;

namespace ParLogLib_UnitTest
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            var mock = new Mock<IFileManager>();
            mock.Setup(foo => foo.GetAllFilenamesWildcard()).Returns(new List<string>(){"eins"});
            mock.Setup(foo => foo.GetLinesOfFile("eins")).Returns(Eins_Lines());



            ParLogLib llib = new ParLogLib(mock.Object, 
                @"^[0-9]{2} [\w]{3} [0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3}", 
                "zeile");

            llib.Parse();

        }

        private static IEnumerable<string> Eins_Lines()
        {
            List<string> xx = new List<string>(){ "erste zeile","zweite zeile","dritte zeile"};
            foreach (string x in xx)
                yield return x;

        }
    }
}

