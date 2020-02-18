using UnityEngine;
using UnityEngine.UI;

public class FinishBlock : MonoBehaviour
{
    [SerializeField]
    private int _winnerCounter;
    [SerializeField]
    private Text __winnerText;
    private BoxCollider2D _selfCollider;


    private void Start()
    {
        _selfCollider = GetComponent<BoxCollider2D>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var v = collision.GetComponent<Ball>();

        if(v != null)
        {
            v.ReturnToPool();
            _winnerCounter--;

            if(_winnerCounter == 0)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        __winnerText.color = new Color(1, 0, 0, 1);
    }
}
