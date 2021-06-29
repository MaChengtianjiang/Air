using System.Collections;
using Define;
using UnityEngine;
using UnityEngine.UI;

//继承MovingObject类
public class Player : MovingObject {


    private int _step = 0;
    
    [SerializeField]
    private Animator anim;						// 动画控制


    
    //继承MovingObject的Start方法　用base调用
    protected override void Start() {
        //调用MovingObject的Start
        base.Start();
        StageManager.Instance.SetPlayer(this);


        // 测试无限走
        // StartCoroutine(Run(99999));
    }


    //Player脚本无效之前、体力保存至GameManager
    //Unity的生命周期API方法
    private void OnDisable() {
    }

    void Update() {

        anim.SetBool("Run", isMoving);
    }


    public IEnumerator Run(int step) {

        this.setStep(step);
        
        while (_step > 0) {
            
            this.AttemptMove<StageCell>();
            yield return 0;
        }
        
    
        
        
    }
    

    protected override void AttemptMove<T>() {
        //调用MovingObject的AttemptMove
        base.AttemptMove<T>();

        // RaycastHit2D hit;

        //能够移动时moveSound1或moveSound2随机播放
        base.Move(() => { _step--; });
        // CheckIfGameOver();
        //Player回合结束

    }

    //必须复写MovingObject的抽象方法
    protected override void OnCantMove<T>(T component) {
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }


    private void CheckIfGameOver() {
    }


    public void setStep(int step) {
        _step = step;
    }
    
    public bool isStop() {
        return _step < 1;
    }
    
    //アニメーションEvents側につける表情切り替え用イベントコール
    public void OnCallChangeFace (string str)
    {   
        // int ichecked = 0;
        // foreach (var animation in animations) {
        //     if (str == animation.name) {
        //         ChangeFace (str);
        //         break;
        //     } else if (ichecked <= animations.Length) {
        //         ichecked++;
        //     } else {
        //         //str指定が間違っている時にはデフォルトで
        //         str = "default@unitychan";
        //         ChangeFace (str);
        //     }
        // } 
    }
}