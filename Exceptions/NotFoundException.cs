namespace Lesson.Exceptions
{
    public class NotFoundException : Exception 
    {
       public NotFoundException(string message):base(message) {
        }
    }
}
