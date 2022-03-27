using System.IO;
using UnityEngine;

namespace API
{
    public static class Constants
    {
        public static string FULL_VIDEO_DIR_SAVE_PATH = Application.persistentDataPath;

        public const string BASE_URL = "http://100.69.115.247:8080";

        public static string GET_PLACES_REQUEST_PATH => BASE_URL + "/api/places";

        public static string PLACES_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Plases.data");

        public static string GET_COUESES_REQUEST_PATH => BASE_URL + "/api/courses";

        public static string COUESES_SAVE_PATH => Path.Combine(Application.persistentDataPath, "Couses.data");
    }
}