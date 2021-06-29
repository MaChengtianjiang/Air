using Define;
using UnityEngine;

namespace Common {
    public class IRotation : MonoBehaviour {
        public void RotateAxisOfSpeed(Axis selfAxis, float speed) {
            switch (selfAxis) {
                case Axis.X:
                    transform.Rotate(new Vector3(1 * Time.deltaTime * speed, 0, 0));
                    break;
                case Axis.Y:
                    transform.Rotate(new Vector3(0, 1 * Time.deltaTime * speed, 0));
                    break;
                case Axis.Z:
                    transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * speed));
                    break;
                default:
                    transform.Rotate(new Vector3(1 * Time.deltaTime * speed, 0, 0));
                    break;
            }
        }

        public void SetRotateOfAxis(Axis axis, float rotate) {
            switch (axis) {
                case Axis.X:
                    transform.Rotate(new Vector3(rotate, 0, 0));
                    break;
                case Axis.Y:
                    transform.Rotate(new Vector3(0, rotate, 0));
                    break;
                case Axis.Z:
                    transform.Rotate(new Vector3(0, 0, rotate));
                    break;
                default:
                    transform.Rotate(new Vector3(rotate, 0, 0));
                    break;
            }
        }
        
        // 获取平面向量间的夹角
        protected float getRotateAngle(Vector3 a, Vector2 b) {
            
            float x1 = a.x;
            float x2 = b.x;
            
            float y1 = a.y;
            float y2 = b.y;
            float epsilon = float.Epsilon;
            

            float dist, dot, degree, angle;
            
            // normalize
            dist = Mathf.Sqrt(x1 * x1 + y1 * y1);
            x1 /= dist;
            y1 /= dist;
            dist = Mathf.Sqrt(x2 * x2 + y2 * y2);
            x2 /= dist;
            y2 /= dist;
            // 点乘
            dot = x1 * x2 + y1 * y2;
            if (Mathf.Abs(dot - 1.0f) <= epsilon)
                angle = 0.0f;
            else if (Mathf.Abs(dot + 1.0f) <= epsilon)
                angle = Mathf.PI;
            else {
                
                float cross;

                angle = Mathf.Acos(dot);
                //cross product
                cross = x1 * y2 - x2 * y1;
                // vector p2 is clockwise from vector p1
                // with respect to the origin (0.0)
                if (cross < 0) {
                    angle = 2 * Mathf.PI - angle;
                }
            }

            degree = angle * 180.0f / Mathf.PI;
            return degree;
        }
    }
}