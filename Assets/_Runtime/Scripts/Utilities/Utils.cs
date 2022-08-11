using UnityEngine;

public class Utils
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        if (position == Vector3.zero) return Vector3.zero;

        float m_DistanceZ = 6f;
        Vector3 distanceFromCamera = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z + m_DistanceZ);
        Plane plane = new Plane(camera.transform.forward, distanceFromCamera);

        Ray ray = camera.ScreenPointToRay(position);
        Vector3 hitPoint = Vector3.zero;
        float enter = 0f;

        if (plane.Raycast(ray, out enter))
        {
            hitPoint = ray.GetPoint(enter);
        }

        return hitPoint;
    }
}
