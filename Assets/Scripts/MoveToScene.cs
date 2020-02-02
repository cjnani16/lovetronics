using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToScene : MonoBehaviour
{
    [SerializeField] string NextSceneName;

    public void Go () {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(()=>{SceneManager.LoadScene(NextSceneName);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
