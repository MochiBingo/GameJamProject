using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class CollectibleScript : MonoBehaviour
{

    public TextMeshProUGUI countText;
    private int count;
    private int totalCount;
    public TextMeshProUGUI levelEndMessage;
void Start()
{
    count = 0;
    totalCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
    SetCountText();
    levelEndMessage.gameObject.SetActive(false);
}
void SetCountText()
{
    countText.text = $"{count}/{totalCount}";
    if (count >= totalCount)
    {
        countText.color = Color.green;
        levelEndMessage.text = "You've gotten all ingredients in this stage, you can head back to the store now!"; 
        levelEndMessage.gameObject.SetActive(true);
        StartCoroutine(HideLevelEndMessage());
    }
}
IEnumerator HideLevelEndMessage()
{
    yield return new WaitForSeconds(4f);
    levelEndMessage.gameObject.SetActive(false);
}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}
