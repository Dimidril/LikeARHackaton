using System;

namespace API
{
    public class Wrappers
    {
        [Serializable]
        public class CoursesList
        {
            public Course[] courses;
        }
    
        [Serializable]
        public class PlacesList
        {
            public Place[] places;
        }

        [Serializable]
        public class Course
        {
            public int id;
            public string name;
            public string description;
            public string image;
        }

        [Serializable]
        public class Place
        {
            public int id;
            public string name;
            public string descruption;
            public string audio;
            public string audioText;
            public ARObject[] models;
        }

        [Serializable]
        public class ARObject
        {
            public int id;
            public string name;
            public string image;
            public string model;
        }
    }
}
