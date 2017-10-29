public class EventsController : Element
{
    public delegate void Notification(Element p_source, params object[] p_data);
    public static event Notification Collision;
    public static event Notification GFinished;

    public void OnCollision(Element p_source, params object[] p_data)
    {
        if (Collision != null)
            Collision(p_source, p_data);
    }

    public void OnGFinished(Element p_source, params object[] p_data)
    {
        if (GFinished != null)
            GFinished(p_source, p_data);
    }

}