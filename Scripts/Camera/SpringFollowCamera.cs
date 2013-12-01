using UnityEngine;
using System.Collections;

namespace Junior
{
    [AddComponentMenu("Third Person Camera/Spring Follow Camera")]

    class SpringFollowCamera : MonoBehaviour
    {
        public Transform target;
        public float distance = 4.0f;
        public float height = 1.0f;
        public float smoothLag = 0.2f;
        public float maxSpeed = 10.0f;
        public float snapLag = 0.3f;
        public float clampHeadPositionScreenSpace = 0.75f;
        public LayerMask lineOfSightMask = 0;

        private bool isSnapping = false;
        private Vector3 headOffset = Vector3.zero;
        private Vector3 centerOffset = Vector3.zero;
        private ThirdPersonController controller;
        private Vector3 velocity = Vector3.zero;
        private float targetHeight = 100000.0f;

        public void Awake()
        {
            CharacterController characterController = target.collider as CharacterController;
            if (characterController)
            {
                centerOffset = characterController.bounds.center - target.position;
                headOffset = centerOffset;
                headOffset.y = characterController.bounds.max.y - target.position.y;
            }

            if (target)
                controller = target.GetComponent<ThirdPersonController>();

            if (!controller)
                Debug.Log("Please assign a target to the camera that has a Third Person Controller script component");
        }

        public void LateUpdate()
        {
            Vector3 targetCenter = target.position + centerOffset;
            Vector3 targetHead = target.position + headOffset;

            //when thrustinging wedon't move the camera up unless the player is moving out of the camera 
            if (controller.IsThrusting())
            {
                //check to see if character is high enough
                float newTargetHeight = targetCenter.y + height;
                if (newTargetHeight < targetHeight || newTargetHeight - targetHeight > 5)
                    targetHeight = targetCenter.y + height;
            }
            else
            {
                targetHeight = targetCenter.y + height;
            }

            //we snap to the character when player hit left mouse button
            if (Input.GetButton("Fire2") && !isSnapping)
            {
                velocity = Vector3.zero;
                isSnapping = true;
            }

            if (isSnapping)
                ApplySnapping(targetCenter);
            else
                ApplyPositionDamping(new Vector3(targetCenter.x, targetHeight, targetCenter.z));

            SetUpRotation(targetCenter, targetHead);
        }

        public void ApplySnapping(Vector3 targetCenter)
        {
            Vector3 position = transform.position;
            Vector3 offset = position - targetCenter;
            offset.y = 0;
            float currentDistance = offset.magnitude;

            float targetAngle = target.eulerAngles.y;
            float currentAngle = transform.eulerAngles.y;

            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref velocity.x, snapLag);
            currentDistance = Mathf.SmoothDamp(currentDistance, distance, ref velocity.z, snapLag);

            Vector3 newPosition = targetCenter;
            newPosition += Quaternion.Euler(0.0f, currentAngle, 0.0f) * Vector3.back * currentDistance;

            newPosition.y = Mathf.SmoothDamp(position.y, targetCenter.y + height, ref velocity.y, smoothLag, maxSpeed);

            newPosition = AdjustLineOfSight(newPosition, targetCenter);

            transform.position = newPosition;

            //we are close to the target so we can stop snapping
            if (AngleDistance(currentAngle, targetAngle) < 3.0f)
            {
                isSnapping = false;
                velocity = Vector3.zero;
            }
        }

        public Vector3 AdjustLineOfSight(Vector3 newPosition, Vector3 target)
        {
            RaycastHit hit;
            if (Physics.Linecast(target, newPosition, out hit, lineOfSightMask.value))
            {
                velocity = Vector3.zero;
                return hit.point;
            }
            return newPosition;
        }

        public void ApplyPositionDamping(Vector3 targetCenter)
        {
            //this is where we try to maintain a constant distiance with a spring
            Vector3 position = transform.position;
            Vector3 offset = position - targetCenter;
            offset.y = 0.0f;
            Vector3 newTargetPosition = offset.normalized * distance + targetCenter;

            Vector3 newPosition;
            newPosition.x = Mathf.SmoothDamp(position.x, newTargetPosition.x, ref velocity.x, smoothLag, maxSpeed);
            newPosition.z = Mathf.SmoothDamp(position.z, newTargetPosition.z, ref velocity.z, smoothLag, maxSpeed);
            newPosition.y = Mathf.SmoothDamp(position.y, targetCenter.y, ref velocity.y, smoothLag, maxSpeed);

            newPosition = AdjustLineOfSight(newPosition, targetCenter);

            transform.position = newPosition;
        }

        public void SetUpRotation(Vector3 centerPos, Vector3 headPos)
        {
            Vector3 cameraPos = transform.position;
            Vector3 offsetToCenter = centerPos - cameraPos;

            //generate the base rotation around only the y-axis
            Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0.0f, offsetToCenter.z));

            Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
            transform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);

            //Calculate the projected center position and top position in world space
            Ray centerRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1.0f));
            Ray topRay = camera.ViewportPointToRay(new Vector3(0.5f, clampHeadPositionScreenSpace, 1.0f));

            Vector3 centerRayPos = centerRay.GetPoint(distance);
            Vector3 topRayPos = topRay.GetPoint(distance);

            float centerToTopAngle = Vector3.Angle(centerRay.direction, topRay.direction);

            float heightToAngle = centerToTopAngle / (centerRayPos.y - topRayPos.y);

            float extraLookAngle = heightToAngle * (centerRayPos.y - centerPos.y);

            if (extraLookAngle < centerToTopAngle)
                extraLookAngle = 0.0f;
            else
            {
                extraLookAngle = extraLookAngle - centerToTopAngle;
                transform.rotation *= Quaternion.Euler(-extraLookAngle, 0.0f, 0.0f);
            }
        }

        public float AngleDistance(float a, float b)
        {
            a = Mathf.Repeat(a, 360);
            b = Mathf.Repeat(b, 360);

            return Mathf.Abs(b - a);
        }
    }
}