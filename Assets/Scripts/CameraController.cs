
public class CameraController : MonoBehaviour

{
    
    public float TrackSpeed = 2.0f;
    public float yOffset = -1;
    public Transform target;
    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, TrackSpeed * Time.deltaTime);
        }
    }
}
