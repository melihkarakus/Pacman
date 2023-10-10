using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform connection; // Ba�lant� noktas� olarak kullan�lacak Transform bile�eni.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Bu fonksiyon, bir Collider2D bu nesnenin tetikleme b�lgesine girdi�inde �al���r.
        Vector3 position = other.transform.position;

        // Tetiklenen nesnenin pozisyonunu ba�lant� noktas�n�n x ve y koordinatlar�na e�itler.
        position.x = connection.position.x;
        position.y = connection.position.y;

        // Nesnenin pozisyonunu g�nceller.
        other.transform.position = position;
    }
}
