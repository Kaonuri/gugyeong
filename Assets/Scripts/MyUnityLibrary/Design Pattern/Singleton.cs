// 상속 받은 클래스에서는 생성자를 반드시 protected로 선언을 해서 외부에서의 생성을 막도록 해야한다.
public abstract class Singleton<T> where T : class, new()
{
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}