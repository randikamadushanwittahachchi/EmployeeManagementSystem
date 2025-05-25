namespace Client.Helper.Constracts;

public interface IGenericModal<T> where T : class
{
    void ShowDialog(Func<T, Task> save, string titel, string button);
    void ShowDialog(Func<T, Task> edit, T entity, string titel, string button);
}
