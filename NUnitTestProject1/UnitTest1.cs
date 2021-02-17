using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var people = MySelect.GetNames();

            var expected = new List<string>();
            expected.Add("James");
            expected.Add("CC");
            expected.Add("Frank");

            Assert.AreEqual(expected, people);
        }

        [Test]
        public void Test2()
        {
            var people = MySelect.GetPeople();

            var expected = new List<int>();
            expected.Add(17);
            expected.Add(19);
            expected.Add(20);

            Assert.AreEqual(expected, people.HiSelect(person => person.Age));
        }

        [Test]
        public void Test3()
        {
            var people = MySelect.GetPeople();

            var expected = new List<int>();
            expected.Add(34);
            expected.Add(38);
            expected.Add(40);

            Assert.AreEqual(expected, people.HiSelect(person => person.Age * 2));
        }

        [Test]
        public void Test4()
        {
            var people = MySelect.GetPeople();

            var expected = new List<Person>();
            var Person1 = new Person();
            Person1.Age = 17;
            Person1.Name = "James";
            expected.Add(Person1);

            var Person2 = new Person();
            Person2.Age = 19;
            Person2.Name = "CC";
            expected.Add(Person2);

            var Person3 = new Person();
            Person3.Age = 20;
            Person3.Name = "Frank";
            expected.Add(Person3);

            var people1 = people.HiSelect(person =>
            new
            {
                Age = person.Age,
                Name = person.Name
            }
  );
            List<Person> people2 = new List<Person>();
            foreach (var item in people1)
            {
                var Person4 = new Person();
                Person4.Age = item.Age;
                Person4.Name = item.Name;
                people2.Add(Person4);
            }
            Assert.AreEqual(expected, people2);
        }
    }

    public struct Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public static class MySelect
    {
        public static List<Person> GetPeople()
        {
            return new List<Person>
        {
             new Person {Name = "James", Age = 17,City = "UnitedStates"},
             new Person {Name = "CC", Age = 19,City = "Taiwan"},
             new Person {Name = "Frank", Age = 20,City = "Italy"}
        };
        }

        public static IEnumerable<string> GetNames()
        {
            var people = GetPeople();

            foreach (var person in people)
            {
                yield return person.Name;
            }
        }

        public static IEnumerable<TResult> HiSelect<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return InternalSelect(source, selector);
        }

        public static IEnumerable<TResult> InternalSelect<TSource, TResult>(
            IEnumerable<TSource> source, Func<TSource, TResult> condition)
        {
            foreach (var item in source)
            {
                yield return condition(item);
            }
        }

    }

}