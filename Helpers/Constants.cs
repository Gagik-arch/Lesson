namespace Lesson.Helpers
{
    public class Constants
    {
        public static readonly string Base = AppContext.BaseDirectory;

        public static readonly string TodoUploadPath = Path.Combine(Base, "uploads", "todos");
    }
}
