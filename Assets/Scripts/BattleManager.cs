using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct BattleMove {
    public string name, description;
    public int power, cost;
    public Color color;
    public BattleMove(string name, string description, int power, int cost) {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.color = Random.ColorHSV();
    }
}

public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject MoveListItemPrefab, UICanvas;
    BattleState currentBattleState;
    enum BattleState { Start, ChooseMove, AttackAnimation, EnemyAttackAnimation, End };
    
    List<BattleMove> DrawMoves() {
        List<BattleMove> generatedMoves = new List<BattleMove>();

        //add a couple random placeholder moves for now
        for (int i = 0; i <3; i++) {
            generatedMoves.Add(new BattleMove("Random Move"+i, "random description "+i, Random.Range(10,50), Random.Range(4,100)));
        }

        return generatedMoves;
    }

    void PresentMoves() {
        List<BattleMove> availableMoves = DrawMoves();

        //put a button on screen for each move
        for (int i = 0; i<availableMoves.Count; i++) {
            GameObject thisMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(0, 1500+300*i, 0), Quaternion.identity);
            thisMove.transform.parent = UICanvas.transform;
            //img.tintColor = availableMoves[i].color;
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentBattleState = BattleState.Start;
        PresentMoves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
