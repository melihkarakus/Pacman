using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform connection; // Baðlantý noktasý olarak kullanýlacak Transform bileþeni.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Bu fonksiyon, bir Collider2D bu nesnenin tetikleme bölgesine girdiðinde çalýþýr.
        Vector3 position = other.transform.position;

        // Tetiklenen nesnenin pozisyonunu baðlantý noktasýnýn x ve y koordinatlarýna eþitler.
        position.x = connection.position.x;
        position.y = connection.position.y;

        // Nesnenin pozisyonunu günceller.
        other.transform.position = position;
    }
}
