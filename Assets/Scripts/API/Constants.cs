using System.IO;
using UnityEngine;

namespace API
{
    public static class Constants
    {
        public static string FULL_VIDEO_DIR_SAVE_PATH = Application.persistentDataPath;
        
        public const string BASE_URL = "https://barsova.4app.pro";

        public static string GET_EXCURSIONS_REQUEST_PATH => BASE_URL + "/api/excursion";

        public static string EXCURSIONS_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Excursions.data");
        
        public static string GET_SCENES360_REQUEST_PATH => BASE_URL + "/api/video";
        
        public static string SCENES360_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Scenes360.data");
        
        public static string GET_QUIZES_REQUEST_PATH => BASE_URL + "/api/quiz";
        
        public static string QUIZES_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Quizes.data");

        public static string GET_OBJECTS_REQUEST_PATH => BASE_URL + "/api/object";
        
        public static string OBJECTS_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Objects.data");
    }
}