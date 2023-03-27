using UnityEngine;

namespace Script
{
    public class KillingCam : MonoBehaviour
    {
        public GameObject particleEffect;
        private Vector2 _touchPos;
        private RaycastHit _hit;
        private Camera _cam;

        private void Start()
        {
            _cam = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touchPos = Input.GetTouch(0).position;
                Ray ray = _cam.ScreenPointToRay(_touchPos);
                if (Physics.Raycast(ray, out _hit))
                {
                    GameObject hitObject = _hit.collider.gameObject;
                    if (hitObject.CompareTag("Enemy"))
                    {
                        var clone = Instantiate(particleEffect, hitObject.transform.position, Quaternion.identity);
                        clone.transform.localScale = hitObject.transform.localScale;
                        Destroy(hitObject);
                    }
                }
            }
        }
    }
}
