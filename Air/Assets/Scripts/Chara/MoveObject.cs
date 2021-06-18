using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract 关键字提供你创建用于继承的的类和成员，但需要通过实现派生才能完整实现这个类实体.
public abstract class MovingObject : MonoBehaviour {
    public float moveSpeed = 2; //对象移动的时间, 单位秒.
    public LayerMask blockingLayer; //用来确认碰撞的图层

    [SerializeField]
    private BoxCollider2D boxCollider; //对象的碰撞器
    
    [SerializeField]
    private Rigidbody2D rb2D; //对象的刚体
    

    [SerializeField]
    private Transform body;
    
    protected bool isMoving = false;


    // 方向
    private List<Vector2> dirs = new List<Vector2>() {
        new Vector2(0,1),
        new Vector2(0,-1),
        new Vector2(-1,0),
        new Vector2(1,0)
    }; 
    private Vector2 beforPos = Vector2.zero;

    //Protected, virtual 关键字定义的方法 可以通过override复写以及继承.
    protected virtual void Start ( ) {
        
    }

    //如果能够移动返回True,不能移动返回False
    //移动需要x方向，y方向和RayCastHit2D的参数来检查碰撞。
    protected void Move (Action callBack = null) {

        if (isMoving) {
            return;
        }

        //存储要移动的起始位置，基于对象当前位置。
        Vector2 start = transform.position;
        
        Vector2 end = Vector2.zero;

        //根据调用移动时传入的方向参数计算结束位置。

        dirs = ListUtils.RandomSortList(dirs);
        
        foreach (var dir in dirs) {
            
            
            end = start + new Vector2 (dir.x, dir.y);

            // 如果与之前的坐标相同，相当于回头了禁止
            // Debug.Log(String.Format("beforPos-x:{0},y:{1}", beforPos.x, beforPos.y));
            // Debug.Log(String.Format("endPos-x:{0},y:{1}", end.x, end.y));
            if (Math.Abs(end.x - beforPos.x) < 1 && Math.Abs(end.y - beforPos.y) < 1) {
                
                // Debug.Log("end == beforPos");
                continue;
            }



            // 判断是否越界
            bool isRowMax = end.x < 0 ||  Mathf.RoundToInt(end.x ) >= SceneManager.Instance.stageWidth;
            bool isColMax = end.y < 0 ||  Mathf.RoundToInt(Mathf.Abs(end.y)) >= SceneManager.Instance.stageHeight;


        
            if ( isRowMax || isColMax ) {
                continue;
            }
        
            StageCell cell = SceneManager.Instance.stageMap[Mathf.RoundToInt(end.x), Mathf.RoundToInt(Mathf.Abs(end.y))];
        

            //检查没有有没有物体
            if (cell != null) {
                
                isMoving = true;
                //可以移动
                beforPos = transform.position;
                break;
            }
        }
        
        //如果捕获到网络，启动SmoothMovement协程传递移动目的地
        StartCoroutine (SmoothMovement (end, callBack));
        
        
    }

    //用于将单位从一个位置移动到下一个位置的协程，定移动到哪里由end参数决定。
    protected IEnumerator SmoothMovement (Vector3 end, Action callBack) {
        

        // 粗略计算距离的大小，根据当前位置和结束的坐标之间的差值常量的的粗略值 sqrMagnitude不开平方 /magnitude 开平方
        // d=√[(x1-x2)^2+(y1-y2)^2+(z1-z2)^2]
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //如果这个距离大于一个float的接近0的常量 (float.Epsilon, 接近0):
        while (sqrRemainingDistance > float.Epsilon) {
            // 计算当前移动的坐标位置
            // Vector3.MoveTowards作用是将当前值current移向目标end。（对Vector3是沿两点间直线）
            // inverseMoveTime (速度) Time.deltaTime 
            // 返回值是当rb2D.position值加上end的值，如果这个值超过了target，返回的就是target的值。
            // 有点类似Vector3.Lerp(rb2D.position,rb2D.position+end, Time.deltaTime/moveTime)
            Vector3 newPostion = Vector3.MoveTowards (rb2D.position, end, moveSpeed * Time.deltaTime);

            //调用刚体的移动方法去移动计算出来的新坐标
            rb2D.MovePosition (newPostion);
            
            //转头
            Vector3 rotateVector = end - transform.position;
            Quaternion newRotation = Quaternion.LookRotation (rotateVector);
            body.rotation = Quaternion.RotateTowards (body.rotation, newRotation, 1 * Time.deltaTime);



            //再次计算距离.直到距离接近0
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //下一帧继续循环 直到
            yield return null;
        }

        isMoving = false;

        if (callBack != null) {
            callBack();
        }

    }

    // 尝试移动 (假如移动前需要进行某些交互)
    protected virtual void AttemptMove<T> () where T : Component {


        // 移动且返回判断是否移动成功
        // bool canMove = Move (xDir, yDir);
        

        // // 从捕获到的对象中获取组件(T 例如玩家的时候是Player,敌人的时候是Enemy)
        // T hitComponent = hit.transform.GetComponent<T> ( );
        //
        // // 如果不能移动且 hitComponent 不为 null,即而且捕获了能与之交互的东西.执行我们的用于继承的OnCantMove方法
        // if (!canMove && hitComponent != null) {
        //
        //     //执行OnCantMove方法并且利用碰撞目标的参数.
        //     OnCantMove(hitComponent);
        // }
    }

    //abstract修饰符关键字表示一个没有被实现或者需要修改的方法
    //OnCantMove我们在继承他的类中去写复写实现
    protected abstract void OnCantMove<T> (T component)
    where T : Component;
}