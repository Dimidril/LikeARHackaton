using System.IO;
using UnityEngine;

namespace Cache
{
    public class CacheDestination : MonoBehaviour
    {
        const string IMAGES_FOLDER_TITLE = "Images";
        const string VIDEO_FOLDER_TITLE = "Videos";
        const string AUDIO_FOLDER_TITLE = "Audios";
        const string MODEL_FOLDER_TITLE = "Models";

        public static string RootFolderPath => Application.persistentDataPath;

        public static string ImagesFolderPath
        {
            get
            {
                var path = Path.Combine(RootFolderPath, IMAGES_FOLDER_TITLE);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                return path;
            }
        }

        public static string AudioFolderPath
        {
            get
            {
                var path = Path.Combine(RootFolderPath, AUDIO_FOLDER_TITLE);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        public static string VideoFolderPath
        {
            get
            {
                var path = Path.Combine(RootFolderPath, VIDEO_FOLDER_TITLE);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }
        
        public static string ModelFolderPath
        {
            get
            {
                var path = Path.Combine(RootFolderPath, MODEL_FOLDER_TITLE);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }
    }
}