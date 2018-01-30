using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    public class SeriallizationTests
    {
        [Test]
        public void save_and_load_school()
        {
            // Arrange
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            School s = CreateTestSchool();

            // Act
            f.Serialize( mem, s );
            mem.Position = 0;
            School s2 = (School)f.Deserialize( mem );

            // Assert
            Assert.That( s2.Name, Is.EqualTo( s.Name ) );
            Assert.That( s2.FindTeacher( "Paul" ), Is.Not.Null );
            Assert.That( s2.FindTeacher( "John" ), Is.Not.Null );
            Assert.That( s2.FindTeacher( "Albert" ), Is.Not.Null );

            Assert.That( s2.FindTeacher( "Albert" ).School, Is.SameAs( s2 ) );
            Assert.That( s2.FindTeacher( "Albert" ).Assignment, Is.SameAs( s2.FindClassRoom( "E01" ) ) );

        }

        private static School CreateTestSchool()
        {
            School s = new School( "First School" );
            Teacher albert = s.AddTeacher( "Albert" );
            Teacher john = s.AddTeacher( "John" );
            Teacher paul = s.AddTeacher( "Paul" );
            Classroom e01 = s.AddClassRoom( "E01" );
            Classroom e03 = s.AddClassRoom( "E03" );
            albert.AssignTo( e01 );
            john.AssignTo( e03 );
            e01.AddPupil( "Simon", "Jouatel" );
            e01.AddPupil( "Albert", "Einstein" );
            e03.AddPupil( "Olivier", "Spinelli" );
            return s;
        }
    }
}
