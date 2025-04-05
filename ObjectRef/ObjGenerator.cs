using System;

namespace ObjectRef;

public class ObjGenerator
{
    public Address Address()
    {
        Address addr = new Address
        {
            Street = "Road-27",
            City = "Chittagong",
            Country = "Bangladesh"
        };

        return addr;
    }
    public AdmissionTest AdmissionTest()
    {
        AdmissionTest test = new AdmissionTest
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(3),
            TestFees = 1000.00
        };

        return test;
    }
    public Course Course()
    {
        Course course = new Course
        {
            Title = "ASP .NET",
            Teacher = Instructor(),
            Topics = new List<Topic>
            {
                Topic(),
                Topic(),
                Topic()
            },
            Fees = 30000,
            Tests = new List<AdmissionTest>
            {
                AdmissionTest(),
                AdmissionTest(),
                AdmissionTest()
            }
        };

        return course;
    }
    public Instructor Instructor()
    {
        Instructor instructor = new Instructor
        {
            Name = "Instructor Name",
            Email = "instructor@domain.com",
            PresentAddress = Address(),
            PermanentAddress = Address(),
            PhoneNumbers = new List<Phone>
            {
                Phone(),
                Phone(),
                Phone()
            }
        };

        return instructor;
    }
    public Phone Phone()
    {
        Phone phone = new Phone
        {
            Number = "1111111111",
            Extension = "0",
            CountryCode = "+88"
        };

        return phone;
    }
    public Session Session()
    {
        Session session = new Session
        {
            DurationInHour = 3,
            LearningObjective = "NULL"
        };

        return session;
    }
    public Topic Topic()
    {
        Topic topic = new Topic
        {
            Title = "Topic Title",
            Sessions = new List<Session>
            {
                Session(),
                Session(),
                Session()
            },
            Description = "Topic Description"
        };

        return topic;
    }
}
