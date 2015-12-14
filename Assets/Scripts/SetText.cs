using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public void SetLabel(Slider slider)
    {
        gameObject.GetComponent<Text>().text = slider.value.ToString();
    }
}
