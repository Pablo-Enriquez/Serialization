using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnriquezSerialization
{
    public class Course
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        //Constructor
        public Course(string number, string title, string description)
        {
            Number = number;
            Title = title;
            Description = description;
        }
    }

    //CourseWriter class to write course to screen and text file
    public class CourseWriter
    {
        //Write courses to screen
        public static void WriteToScreen(List<Course> courses)
        {
            foreach (var course in courses)
            {
                Console.WriteLine($"Number: {course.Number}");
                Console.WriteLine($"Name: {course.Title}");
                Console.WriteLine($"Description: {course.Description}\n");
            }
        }
        //Write courses to text file
        public static void WriteToTextFile(List<Course> courses, string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                foreach (var course in courses)
                {
                    file.WriteLine($"{course.Number}\t{course.Title}\t{course.Description}");
                }
            }
        }
    }
    //CourseReader class to read courses from a tab-delimted text file
    public class CourseReader
    {
        //Read courses from text file
        public static List<Course> ReadFromTextFile(string filename)
        {
            List<Course> courses = new List<Course>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('\t');
                    Course course = new Course(parts[0], parts[1], parts[2]);
                    courses.Add(course);
                }
            }

            return courses;

        }
    }
    //DataSerializer calss for serialization and deserialization
    public class DataSerializer
    {
        //Serialize data to text file
        public static void SerializeToBinary<T>(List<T> data, string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                foreach (var item in data)
                {
                    file.WriteLine(item.ToString());
                }
            }
        }

        //Deserialize data from text file
        public static List<T> DeserializeFromTextFile<T>(string filename)
        {
            List<T> data = new List<T>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    // Assuming T has a constructor that accepts string
                    data.Add((T)Activator.CreateInstance(typeof(T), line));
                }
            }
            return data;
        }
    }


            // Serialize data to XML format
            //public static void SerializeToXml<T>(List<T> data, string filename)
        //{
            //using (FileStream stream = new FileStream(filename, FileMode.Create))
            //{
               // XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                //serializer.Serialize(stream, data);
            //}
        //}

        // Deserialize data from XML format
        //public static List<T> DeserializeFromXml<T>(string filename)
       // {
           // using (FileStream stream = new FileStream(filename, FileMode.Open))
           // {
               // XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
               // return (List<T>)serializer.Deserialize(stream);
           // }
       // }

        // Serialize data to JSON format
       // public static void SerializeToJson<T>(List<T> data, string filename)
       // {
           // string json = JsonConvert.SerializeObject(data);
           // File.WriteAllText(filename, json);
       // }

        // Deserialize data from JSON format
       // public static List<T> DeserializeFromJson<T>(string filename)
        //{
           // string json = File.ReadAllText(filename);
            //return JsonConvert.DeserializeObject<List<T>>(json);
        //}
   // }
    class Program
    {
        static void Main(string[] arg)
        {
            //Print banner and welcome message
            Console.WriteLine("****************************************************************************************************");
            Console.WriteLine("                                       COURSE MANAGEMENT TOOL");
            Console.WriteLine("****************************************************************************************************\n");

            // Ask user for first name
            Console.Write("What is your first name? ");
            string firstName = Console.ReadLine();

            // Ask user for number of courses
            Console.Write("How many courses will you enter? ");
            int numCourses = int.Parse(Console.ReadLine());

            List<Course> courses = new List<Course>();

            //Ask user to enter course details for each course
            for (int i = 0; i < numCourses; i++)
            {
                Console.WriteLine($"\nEnter course number {i + 1}:");
                Console.Write("Enter course number: ");
                string number = Console.ReadLine();
                Console.Write("Enter course title: ");
                string title = Console.ReadLine();
                Console.Write("Enter course description: ");
                string description = Console.ReadLine();

                Course course = new Course(number, title, description);
                courses.Add(course);
            }

            //Print the course entered by the user
            Console.WriteLine("\nThese are the courses you entered:");
            CourseWriter.WriteToScreen(courses);

            //Ask user to enter directory to save the courses
            Console.Write("Enter directory to save the courses to: ");
            string directory = Console.ReadLine();

            //Save courses to files
            //DataSerializer.SerializeToTextFile(courses, $"{directory}\\{firstName}.txt");

            //Read courses from text file and print. 
            //I Could not get this section of the 
            Console.WriteLine($"\nI will now read these courses back from {firstName}.txt file:");
            //List<Course> readCourses = DataSerializer.DeserializeFromTextFile<Course>($"{directory}\\{firstName}.txt");
            //CourseWriter.WriteToScreen(readCourses);


            //Prints the closing message
            Console.WriteLine("****************************************************************************************************");
            Console.WriteLine("                                  Thank you for using this program!");
            Console.WriteLine("****************************************************************************************************");
        }

    }
}
