using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider uiSlider; // Unity Editor'da bağlantı yapılacak Slider
    public Transform characterTransform; // Karakterin Transform bileşeni

    void Update()
    {
        if (characterTransform != null && uiSlider != null)
        {
            // Karakterin dünya konumunu al
            Vector3 characterPosition = characterTransform.position;

            // Karakterin dünya konumunu kullanarak Slider'ın dünya konumunu güncelle
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(characterPosition);
            uiSlider.transform.position = screenPosition;
        }
    }
}
