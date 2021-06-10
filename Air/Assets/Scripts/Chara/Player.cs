using System.Collections;

using UnityEngine;
using UnityEngine.UI;
//继承MovingObject类
public class Player : MovingObject {


    
    //继承MovingObject的Start方法　用base调用
    protected override void Start ( ) {

        //调用MovingObject的Start
        base.Start ( );
    }
    //Player脚本无效之前、体力保存至GameManager
    //Unity的生命周期API方法
    private void OnDisable ( ) {
    }

    void Update ( ) {
        
        int horizontal = 0;
        int vertical = 0;

        //********** 修改开始 **********//

        //用于判断平台
        //键盘操作
        horizontal = (int) Input.GetAxisRaw ("Horizontal");
        vertical = (int) Input.GetAxisRaw ("Vertical");

        if (horizontal != 0) {
            vertical = 0;
        }
        //触摸操作


        //********* 修改结束 **********//
        //无论进行上下左右移动之时
        if (horizontal != 0 || vertical != 0) {
            //Wall: 指定传入范型<T>类
            //Player的场合只需要判定Wall
            AttemptMove<StageCell> (horizontal, vertical);
        }
    }

    protected override void AttemptMove<T> (int xDir, int yDir) {


        //调用MovingObject的AttemptMove
        base.AttemptMove<T> (xDir, yDir);

        RaycastHit2D hit;

        //能够移动时moveSound1或moveSound2随机播放
        if (Move (xDir, yDir)) {
            // SoundManager.Instance.RandomizeSfx (moveSound1, moveSound2);
        }

        CheckIfGameOver ( );
        //Player回合结束
        // GameManager.instance.playersTurn = false;
    }

    //必须复写MovingObject的抽象方法
    protected override void OnCantMove<T> (T component) {

    }

    private void OnTriggerEnter2D (Collider2D other) {
       
    }

   
  

    private void CheckIfGameOver ( ) {
    }
}