using  System.IO;

namespace Lesson.Helpers
{
    public class Io
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
