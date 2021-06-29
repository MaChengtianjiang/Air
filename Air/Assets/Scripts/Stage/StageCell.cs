using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CellDefine cellType = CellDefine.Event;



    public CellDefine getType() {
        return cellType;
    }
    
}
