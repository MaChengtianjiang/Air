using System.Collections;

using UnityEngine;

// abstract 关键字提供你创建用于继承的的类和成员，但需要通过实现派生才能完整实现这个类实体.
public abstract class MovingObject : MonoBehaviour {
    public float moveTime = 0.1f; //对象移动的时间, 单位秒.
    public LayerMask blockingLayer; //用来确认碰撞的图层

    [SerializeField]
    private BoxCollider2D boxCollider; //对象的碰撞器
    
    [SerializeField]
    private Rigidbody2D rb2D; //对象的刚体
    
    private float inverseMoveTime; //时间倒数（速度）.

    //Protected, virtual 关键字定义的方法 可以通过override复写以及继承.
    protected virtual void Start ( ) {
        

        //存储移动时间的倒数.
        inverseMoveTime = 1f / moveTime;
    }

    //如果能够移动返回True,不能移动返回False
    //移动需要x方向，y方向和RayCastHit2D的参数来检查碰撞。
    protected bool Move (int xDir, int yDir) {
        //存储要移动的起始位置，基于对象当前位置。
        Vector2 start = transform.position;

        //根据调用移动时传入的方向参数计算结束位置。
        Vector2 end = start + new Vector2 (xDir, -yDir);



        // 判断是否越界
        bool isRowMax = end.x < 0 ||  Mathf.RoundToInt(end.x) > SceneManager.Instance.stageWidth;
        bool isColMax = end.x < 0 ||  Mathf.RoundToInt(Mathf.Abs(end.y)) > SceneManager.Instance.stageHeight;
        
        if ( isRowMax || isColMax ) {
            return false;
        }
        
        
        StageCell cell = SceneManager.Instance.stageMap[Mathf.RoundToInt(end.x), Mathf.RoundToInt(Mathf.Abs(end.y))];
        

        //检查没有有没有物体
        if (cell != null) {
            //如果捕获到单位，启动SmoothMovement协程传递移动目的地
            StartCoroutine (SmoothMovement (end));

            //可以移动 返回true
            return true;
        }

        //有东西阻挡 返回false
        return false;
    }

    //用于将单位从一个位置移动到下一个位置的协程，定移动到哪里由end参数决定。
    protected IEnumerator SmoothMovement (Vector3 end) {

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
            Vector3 newPostion = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //调用刚体的移动方法去移动计算出来的新坐标
            rb2D.MovePosition (newPostion);

            //再次计算距离.直到距离接近0
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //下一帧继续循环 直到
            yield return null;
        }
    }

    // 尝试移动
    // virtual keyword 定义 AttemptMove ,实现类可以通过 override关键字进行复写.
    // 使用一个通用的参数t来指定组件的类型，我们期望我们的单位与被blocked标记的（玩家和敌人，墙和玩家）进行交互.
    protected virtual void AttemptMove<T> (int xDir, int yDir)
    where T : Component {


        // 移动且返回判断是否移动成功
        bool canMove = Move (xDir, yDir);
        

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