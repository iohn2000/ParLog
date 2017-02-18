using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.IO;
using ParLog;

namespace ParLogLib_UnitTest
{
    [TestFixture()]
    public class Test
    {

        [Test(), Category("simple tests")]
        public void KomplexCase()
        {
            var mock = new Mock<IFileManager>();
            mock.Setup(foo => foo.GetAllFilenamesWildcard()).Returns(new List<string>(){"eins"});
            mock.Setup(foo => foo.GetLinesOfFile("eins")).Returns(TestLines());

            CmdArguments cmd = new CmdArguments();
            cmd.StartOfLinePattern = @"^in";
            cmd.SearchPattern = "zeile";
            cmd.ShowPerformance = false;

            ParLogLib llib = new ParLogLib(mock.Object, cmd);

            // catch standard output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                llib.Parse();

                string expected = 
                    @"in erste zeile
in zweite zeile
  multi line 2.5 no key word
in dritte zeile
  mehr zu drei
  noch mehr zu drei
in vierte zeile
  mehr zu zeile 4
in zeile
out zeile
in zeile
out ohne dem wort
in wieder eine zeile
"; 
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
                Console.WriteLine(sw.ToString());
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        private static IEnumerable<string> TestLines()
        {
            string testLines=@"
in erste zeile
in zweite zeile
  multi line 2.5 no key word
in dritte zeile
  mehr zu drei
  noch mehr zu drei
in vierte zeile
  mehr zu zeile 4
in aber kein keyword
  mehr ohne keyword
  mehr mit keyword
out ohne passenden keyword
out mt zeile als search term
in zeile
out zeile
in zeile
out ohne dem wort
in but no keyword
 dont show this
 neither that
in wieder eine zeile";

            foreach (string x in testLines.Split(new string[]{ Environment.NewLine },StringSplitOptions.RemoveEmptyEntries))
                yield return x;

        }
    }
}

