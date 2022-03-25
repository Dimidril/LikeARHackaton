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
            public string imagePath;
        }

        [Serializable]
        public class Place
        {
            public int id;
            public string name;
            public string description;
            public string audioPath;
            public string audioText;
            public ARObject[] aRObjects;
        }

        [Serializable]
        public class ARObject
        {
            public int id;
            public string name;
            public string modelPath;
            public string description;
        }
    }
}
