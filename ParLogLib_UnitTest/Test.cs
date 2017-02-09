using NUnit.Framework;
using System;
using Moq;
using ParLog;
using System.Collections.Generic;
using System.IO;

namespace ParLogLib_UnitTest
{
    [TestFixture()]
    public class Test
    {
        [Test(), Category("simple tests")]
        public void All_Lines_Match()
        {
            var mock = new Mock<IFileManager>();
            mock.Setup(foo => foo.GetAllFilenamesWildcard()).Returns(new List<string>(){"eins"});
            mock.Setup(foo => foo.GetLinesOfFile("eins")).Returns(Eins_Lines());

            ParLogLib llib = new ParLogLib(mock.Object, 
                //@"^[0-9]{2} [\w]{3} [0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2},[0-9]{3}", 
                @"^aa", 
                "zeile");

            // catch standard output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                llib.Parse();

                string expected = 
                    "aa erste zeile" + Environment.NewLine
                    + "aa zweite zeile" + Environment.NewLine
                    + "aa dritte zeile" + Environment.NewLine; 
                    //+ "aa dritte zeile" + Environment.NewLine;

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));

                Console.WriteLine(sw.ToString());

                Assert.AreEqual(expected, sw.ToString());
            }
        }

        private static IEnumerable<string> Eins_Lines()
        {
            List<string> xx = new List<string>(){ "aa erste zeile","aa zweite zeile","aa dritte zeile", "bb vierte zeile"};
            foreach (string x in xx)
                yield return x;

        }
    }
}

